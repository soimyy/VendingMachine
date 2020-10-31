using System;
using System.Text.Json.Serialization;

namespace VendingMachine.Base {

    /// <summary>
    /// 販売情報の基底クラス
    /// </summary>
    public class SalesInfoBase {

        #region メンバ変数

        /// <summary>
        /// 名前
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// 値段
        /// </summary>
        [JsonPropertyName("price")]
        public uint Price { get; set; }

        /// <summary>
        /// 在庫
        /// </summary>
        [JsonPropertyName("stock")]
        public uint Stock { get; set; }

        #endregion

        #region コンストラクタ

        public SalesInfoBase() {

            Name = string.Empty;
            Price = 0;
            Stock = 0;
        }

        public SalesInfoBase(string _name, uint _price, uint _stock) {

            Name = _name;
            Price = _price;
            Stock = _stock;
        }

        #endregion
    }
}
