using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using VendingMachine.Base;

namespace VendingMachine.Data {

    internal class SalesParameter {

        #region private variable

        /// <summary>
        /// お金情報
        /// </summary>
        private MoneyInfoList m_moneyInfo = null;

        /// <summary>
        /// 商品情報
        /// </summary>
        private ProductInfoList m_productInfo = null;

        #endregion

        #region property

        /// <summary>お金情報</summary>
        internal MoneyInfoList MoneyInfo {

            get { return m_moneyInfo; }
        }

        /// <summary>商品情報</summary>
        internal ProductInfoList ProductInfo {

            get { return m_productInfo; }
        }

        #endregion

        #region internal function

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// コンストラクタ
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal SalesParameter() {

            m_moneyInfo = new MoneyInfoList();
            m_productInfo = new ProductInfoList();
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// パラメータファイルを読み込む
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal bool ReadParmeterFile() {

            const string PRODUCT_INFO_FILE_NAME = "product.json";
            const string MONEY_INFO_FILE_NAME = "money.json";

            string jsonString = string.Empty;
            string filePath = string.Empty;

            SalesInfoListBase infoList = null;

            try {


                // パラメータを読み込む
                infoList = new SalesInfoListBase();
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Parameter/", PRODUCT_INFO_FILE_NAME);
                m_productInfo.m_salesInfoList = this.ReadParameter(filePath, infoList);

                // パラメータを読み込む
                infoList = new SalesInfoListBase();
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Parameter/", MONEY_INFO_FILE_NAME);
                m_moneyInfo.m_salesInfoList = this.ReadParameter(filePath, infoList);
            }
            catch (Exception) {

                return false;
            }
            finally {

            }

            return true;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// パラメータファイルを書き込む
        /// </summary>
        ////////////////////////////////////////////////////////////
        internal bool WriteParamterFile() {

            const string PRODUCT_INFO_FILE_NAME = "product.json";
            const string MONEY_INFO_FILE_NAME = "money.json";

            string jsonString = string.Empty;
            string filePath = string.Empty;

            SalesInfoListBase infoList = null;

            try {


                // パラメータを読み込む
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Parameter/", PRODUCT_INFO_FILE_NAME);
                this.WriteParamter(filePath, m_productInfo);

                // パラメータを読み込む
                infoList = new SalesInfoListBase();
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../Parameter/", MONEY_INFO_FILE_NAME);
                this.WriteParamter(filePath, m_moneyInfo);
            }
            catch (Exception) {

                return false;
            }
            finally {

            }

            return true;
        }

    #endregion

    #region private function

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// パラメータを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="salesDataList"></param>
        ////////////////////////////////////////////////////////////
        private List<SalesInfoBase> ReadParameter(string filePath, SalesInfoListBase salesInfoListBases) {

                string jsonString = string.Empty;

                // jsonファイルの読み込み
                jsonString = File.ReadAllText(filePath);

                // デシリアライズ
                salesInfoListBases = JsonSerializer.Deserialize<SalesInfoListBase>(jsonString);

                return salesInfoListBases.m_salesInfoList;
        }

        ////////////////////////////////////////////////////////////
        /// <summary>
        /// パラメータを読み込む
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="salesDataList"></param>
        ////////////////////////////////////////////////////////////
        private bool WriteParamter(string filePath, SalesInfoListBase info) {

            string jsonString = string.Empty;

            // jsonファイルの読み込み
            jsonString = File.ReadAllText(filePath);

            // デシリアライズ
            jsonString = JsonSerializer.Serialize<SalesInfoListBase>(info);

            File.WriteAllText(filePath, jsonString);

            return true;
        }

        #endregion
    }
}
