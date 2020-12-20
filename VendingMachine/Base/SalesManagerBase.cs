using System;
using System.Collections.Generic;

namespace VendingMachine.Base {

    /// <summary>
    /// 販売管理クラスの基底クラス
    /// </summary>
    internal abstract class SalesManagerBase {

        #region メンバ変数

        /// <summary>販売情報のリスト</summary>
        private SalesInfoListBase m_salesInfoList = null;

        #endregion

        #region コンストラクタ

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// コンストラクタ
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal SalesManagerBase(SalesInfoListBase infoList) {

            m_salesInfoList = infoList;
        }

        #endregion

        #region function

        #region

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 管理を開始する
        /// </summary>
        /// <returns>開始結果</returns>
        ////////////////////////////////////////////////////////////
        internal virtual bool Start() {

            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入可否を取得する
        /// </summary>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////
        internal abstract bool GetEnablePurchase(SalesInfoBase info);

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// 購入する
        /// </summary>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////
        internal virtual bool Purchase(SalesInfoBase purchaseInfo) {

            return true;
        }

        #endregion

        #endregion

    }
}