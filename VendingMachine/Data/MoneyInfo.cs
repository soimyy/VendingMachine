using System;
using System.Collections.Generic;

using VendingMachine.Base;

namespace VendingMachine.Data {

    /// <summary>
    /// お金情報クラス
    /// </summary>
    internal class MoneyInfo : SalesInfoBase {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal MoneyInfo(string name, uint price, uint stock) : base(name, price, stock) {

        }
    }
}
