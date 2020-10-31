using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VendingMachine.Base {

    public class SalesInfoListBase {

        /// <summary>
        /// 販売情報リスト
        /// </summary>
        [JsonPropertyName("data")]
        public List<SalesInfoBase> m_salesInfoList { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SalesInfoListBase() {

            m_salesInfoList = new List<SalesInfoBase>();
        }
    }
}
