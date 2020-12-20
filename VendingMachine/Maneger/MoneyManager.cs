using System;
using System.Collections.Generic;

using VendingMachine.Base;
using VendingMachine.Data;

namespace VendingMachine.Maneger {

    /// <summary>
    /// お金管理クラス
    /// </summary>
    internal class MoneyManager : SalesManagerBase {

        #region variable

        /// <summary>在庫</summary>
        private MoneyInfoList m_stockInfo = null;

        /// <summary>投入</summary>
        private MoneyInfoList m_inputInfo = null;

        /// <summary>投入</summary>
        private uint m_inputPrice;

        #endregion

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// コンストラクタ
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal MoneyManager(MoneyInfoList infoList) : base(infoList) {

            m_inputPrice = 0;
            m_stockInfo = infoList;
        }

        #region function

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 投入金額を更新する
        /// </summary>
        /// <param name="moneyInfoList"></param>
        ////////////////////////////////////////////////////////////
        internal void UpdateInputMoney(MoneyInfoList moneyInfoList) {

            m_inputInfo = moneyInfoList;

            m_inputPrice = m_inputInfo.GetSumPrice();

            // 在庫数と投入金額の情報マージする
            for (int index = 0; index < m_stockInfo.m_salesInfoList.Count; index++) {

                m_stockInfo.m_salesInfoList[index].Stock += m_inputInfo.m_salesInfoList[index].Stock;
            }

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 釣り銭を出す
        /// </summary>
        /// <param name="moneyInfo"></param>
        ////////////////////////////////////////////////////////////
        internal void GiveChange(ref MoneyInfoList moneyInfo) {

            // 釣り銭を設定する
            this.SetChange(ref moneyInfo);

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 釣り銭を計算する
        /// </summary>
        /// <param name="moneyInfo"></param>
        ////////////////////////////////////////////////////////////
        private void SetChange(ref MoneyInfoList moneyInfo) {

            // 釣り銭情報を計算する
            this.ClacChange(ref moneyInfo);

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 釣り銭を設定する
        /// </summary>
        /// <param name="moneyInfo"></param>
        /// <param name="sumDiff"></param>
        ////////////////////////////////////////////////////////////
        private void ClacChange(ref MoneyInfoList moneyInfo) {

            // 情報から最小数の釣り銭を計算する
            for (int index = 0; index < m_stockInfo.m_salesInfoList.Count; index++) {

                SalesInfoBase changeData = new SalesInfoBase();

                uint chargeCount = 0;
                uint tmpChargeCount = 0;
                SalesInfoBase stockData = null;

                stockData = m_stockInfo.m_salesInfoList[index];

                string name = stockData.Name;
                uint stock = stockData.Stock;
                uint price = stockData.Price;

                // 紙幣枚数 = 合計金額 / 紙幣価値
                tmpChargeCount = m_inputPrice / stockData.Price;
                if (tmpChargeCount > stockData.Stock) {

                    chargeCount = stockData.Stock;
                }
                else {

                    chargeCount = tmpChargeCount;
                }

                // 在庫の紙幣枚数を減らす
                m_stockInfo.m_salesInfoList[index].Stock -= chargeCount;

                // (合計金額 - 返金額)
                m_inputPrice -= chargeCount * price;

                // 釣り銭情報を設定する
                changeData.Name = name;
                changeData.Price = price;
                changeData.Stock = chargeCount;

                // 情報を追加する
                moneyInfo.m_salesInfoList.Add(changeData);
            }

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入可否を取得する
        /// </summary>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////
        internal override bool GetEnablePurchase(SalesInfoBase purchaseInfo) {

            //uint inputSumPrice = 0;
            uint returnMoney = 0;

            //inputSumPrice = m_inputInfo.GetSumPrice();

            if (m_inputPrice < purchaseInfo.Price) {

                // 投入金額 < 購入金額の場合
                return false;
            }

            // 
            returnMoney = m_inputPrice - purchaseInfo.Price;
            //returnMoney = inputSumPrice - purchaseInfo.Price;

            // 情報から最小数の釣り銭を計算する
            for (int index = 0; index < m_stockInfo.m_salesInfoList.Count; index++) {

                SalesInfoBase stockInfo = null;
                uint tmpChargeCount = 0;
                uint chargeCount = 0;

                stockInfo = m_stockInfo.m_salesInfoList[index];

                if (returnMoney < stockInfo.Price) {

                    // 返金額 < 対象の紙幣の価値
                    continue;
                }

                // 
                tmpChargeCount = returnMoney / stockInfo.Price;
                if (tmpChargeCount > stockInfo.Stock) {

                    chargeCount = stockInfo.Stock;
                }
                else {

                    chargeCount = tmpChargeCount;
                }

                returnMoney -= stockInfo.Price * chargeCount;
            }
            if (returnMoney != 0) {

                return false;
            }

            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入する
        /// </summary>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////
        internal override bool Purchase(SalesInfoBase purchaseInfo) {

            m_inputPrice -= purchaseInfo.Price;

            return true;
        }

        #endregion 

    }
}
