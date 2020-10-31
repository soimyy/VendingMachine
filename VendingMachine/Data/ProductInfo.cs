using System;
using VendingMachine.Base;

namespace VendingMachine.Data {

    /// <summary>
    /// 商品情報クラス
    /// </summary>
    internal class ProductInfo : SalesInfoBase {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ProductInfo(string name, uint price, uint stock) : base(name, price, stock) {

        }
    }
}
