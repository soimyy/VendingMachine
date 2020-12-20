using System;
using System.Collections.Generic;

using VendingMachine.Base;
using VendingMachine.Data;

namespace VendingMachine.Maneger {

    /// <summary>
    /// 商品管理クラス
    /// </summary>
    internal class ProductManager : SalesManagerBase {

        /// <summary>在庫</summary>
        private ProductInfoList m_stockInfo = null;

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// コンストラクタ
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal ProductManager(ProductInfoList infoList) : base(infoList) {

            m_stockInfo = infoList;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入可否を取得する
        /// </summary>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////
        internal override bool GetEnablePurchase(SalesInfoBase purchaseInfo) {

            bool enablePurchase = false;

            foreach (var info in m_stockInfo.m_salesInfoList) {

                if (info.Name != purchaseInfo.Name) {

                    continue;
                }
                if (info.Stock == 0) {

                    return false;
                }

                enablePurchase = true;
                break;
            }
            if (!enablePurchase) {

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

            foreach (var info in m_stockInfo.m_salesInfoList) {

                if (info.Name != purchaseInfo.Name) {

                    continue;
                }

                info.Stock--;
            }

            return true;
        }
    }
}