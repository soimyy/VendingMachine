using System;
using System.Collections.Generic;

using VendingMachine.Base;
using VendingMachine.Data;

namespace VendingMachine.Maneger {

    /// <summary>
    /// 商品管理クラス
    /// </summary>
    internal class ProductManager : SalesManagerBase {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ProductManager(ProductInfoList infoList) : base(infoList) {

        }
    }
}