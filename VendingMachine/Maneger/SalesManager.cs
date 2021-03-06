﻿using System;
using System.Collections.Generic;

using VendingMachine.Data;
using VendingMachine.Base;

namespace VendingMachine.Maneger {

    /// <summary>販売管理</summary>
    internal class SalesManager {

        /// <summary>商品管理クラス</summary>
        private ProductManager m_productManager = null;

        /// <summary>お金管理クラス</summary>
        private MoneyManager m_moneyManger = null;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// コンストラクタ
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal SalesManager(SalesParameter salesParamter) {

            m_productManager = new ProductManager(salesParamter.ProductInfo);
            m_moneyManger = new MoneyManager(salesParamter.MoneyInfo);
        }

        #region function

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 管理を開始する
        /// </summary>
        /// <returns>開始結果</returns>
        ////////////////////////////////////////////////////////////
        internal bool Start() {

            bool isSuccess = false;

            try {

                isSuccess = m_productManager.Start();
                if (!isSuccess) {

                    return false;
                }

                isSuccess = m_moneyManger.Start();
                if (!isSuccess) {

                    return false;
                }
            }
            catch (Exception) {

                return false;
            }

            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 投入金額を更新する
        /// </summary>
        /// <param name="moneyInfoList"></param>
        ////////////////////////////////////////////////////////////
        internal void UpdateInputMoney(MoneyInfoList moneyInfoList) {

            m_moneyManger.UpdateInputMoney(moneyInfoList);

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 釣り銭をだす
        /// </summary>
        /// <param name="moneyInfo"></param>
        ////////////////////////////////////////////////////////////
        internal void GiveChange(ref MoneyInfoList moneyInfo) {

            m_moneyManger.GiveChange(ref moneyInfo);

            return;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入可否を取得する
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal bool GetEnablePurchase(SalesInfoBase info) {

            bool enablePurchase = false;

            enablePurchase = m_moneyManger.GetEnablePurchase(info);
            if (!enablePurchase) {

                return false;
            }

            enablePurchase = m_productManager.GetEnablePurchase(info);
            if (!enablePurchase) {

                return false;
            }

            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 商品を購入する
        /// </summary>
        /// <param name="info">購入情報</param>
        /// <returns>購入結果</returns>
        ////////////////////////////////////////////////////////////
        internal bool Purchase(SalesInfoBase info) {

            m_moneyManger.Purchase(info);

            m_productManager.Purchase(info);

            return true;
        }

        #endregion 
    }
}
