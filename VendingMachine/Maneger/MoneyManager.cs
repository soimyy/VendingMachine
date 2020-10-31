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

        /// <summary>
        /// 在庫
        /// </summary>
        private MoneyInfoList m_stockInfo = null;

        /// <summary>
        /// 投入
        /// </summary>
        private MoneyInfoList m_inputInfo = null;

        ///// <summary>
        ///// お金価値リスト
        ///// </summary>
        //private List<int> m_valueList = null;

        ///// <summary>
        ///// お金名リスト
        ///// </summary>
        //private List<string> m_nameList = null;

        //#endregion

        //#region property

        ///// <summary>
        ///// 価値リスト
        ///// </summary>
        //internal List<int> ValueList {

        //    get { return m_valueList; }
        //}

        ///// <summary>
        ///// 名前リスト
        ///// </summary>
        //internal List<string> NameList {

        //    get { return m_nameList; }
        //}

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal MoneyManager(MoneyInfoList infoList) : base(infoList) {

            m_stockInfo = infoList;
        }

        #region function

        /// <summary>
        /// 投入金額を更新する
        /// </summary>
        /// <param name="moneyInfoList"></param>
        internal void UpdateInputMoney(MoneyInfoList moneyInfoList) {

            m_inputInfo = moneyInfoList;

            return;
        }

        /// <summary>
        /// 釣り銭を出す
        /// </summary>
        /// <param name="moneyInfo"></param>
        internal void GiveChange(ref MoneyInfoList moneyInfo) {

            // 釣り銭を設定する
            this.SetChange(ref moneyInfo);

            return;
        }

        /// <summary>
        /// 商品を購入する
        /// </summary>
        /// <param name="infoList">購入情報</param>
        /// <returns>購入結果可否</returns>
        internal bool BuyProduct(List<Dictionary<string, uint>> infoList) {

            foreach (Dictionary<string, uint> info in infoList) {


            }

            return true;
        }

        /// <summary>
        /// 釣り銭を計算する
        /// </summary>
        /// <param name="moneyInfo"></param>
        private void SetChange(ref MoneyInfoList moneyInfo) {

            uint sumInputMoney = 0;

            // 合計を計算する
            sumInputMoney = this.CalcSum(m_inputInfo);

            // 釣り銭情報を計算する
            this.ClacChange(ref moneyInfo, sumInputMoney);

            return;
        }

        /// <summary>
        /// 合計を計算する
        /// </summary>
        /// <param name="moneyInfo"></param>
        /// <returns></returns>
        private uint CalcSum(MoneyInfoList infoList) {

            uint sum = 0;

            foreach (SalesInfoBase info in infoList.m_salesInfoList) {

                sum += info.Price * info.Stock;
            }

            return sum;
        }

        /// <summary>
        /// 釣り銭を設定する
        /// </summary>
        /// <param name="moneyInfo"></param>
        /// <param name="sumDiff"></param>
        private void ClacChange(ref MoneyInfoList moneyInfo, uint sum) {

            // 在庫数と投入金額の情報マージする
            for (int index = 0; index < m_stockInfo.m_salesInfoList.Count; index++) {

                m_stockInfo.m_salesInfoList[index].Stock += m_inputInfo.m_salesInfoList[index].Stock;
            }

            // 情報から最小数の釣り銭を計算する
            for (int index = 0; index < m_stockInfo.m_salesInfoList.Count; index++) {

                SalesInfoBase salesInfoBase = new SalesInfoBase(); 

                uint num = 0;
                string name = m_stockInfo.m_salesInfoList[index].Name;
                uint stock = m_stockInfo.m_salesInfoList[index].Stock;
                uint price = m_stockInfo.m_salesInfoList[index].Price;


                salesInfoBase.Price = price;

                // 合計金額 / 紙幣価値
                num = sum / salesInfoBase.Price;
                if (num == 0) {

                    continue;
                }


                //for (int min = 0; min < num; min++) {



                //    sum -= 
                //}

                //if (num > stock) {

                //}
                //salesInfoBase.Stock = m_stockInfo.m_salesInfoList[index].Stock - num;

                //moneyInfo.m_salesInfoList.Add();

            }

            return;
        }

        /// <summary>
        /// 釣り銭を出す
        /// </summary>
        /// <returns>釣り銭</returns>
        //internal List<Dictionary<string, uint>> GiveChange() {

        //    string change = string.Empty;

        //    return change;
        //}

        #endregion 

    }
}
