using System;
using System.Collections.Generic;

using VendingMachine.Data;
using VendingMachine.Form;
using VendingMachine.Maneger;

namespace VendingMachine.Controller {

    internal class ApplicationController {

        #region event

        /// <summary>
        /// お金投入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moneyInfoList"></param>
        //private void InputMoneyEvent(object sender, List<MoneyInfo> moneyInfoList);

        #endregion

        #region variable

        /// <summary>
        /// 販売管理
        /// </summary>
        private SalesManager m_salesManager = null;

        /// <summary>
        /// メイン画面
        /// </summary>
        private MainFrom m_mainForm = null;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal ApplicationController(SalesParameter parameter) {

            m_salesManager = new SalesManager(parameter);
            m_mainForm = new MainFrom(parameter);
        }

        #region function

        /// <summary>
        /// 開始する
        /// </summary>
        /// <returns></returns>
        internal bool Start() {

            bool isSuccess = false;

            // イベントを登録する
            this.AddEventHandler();

            // 開始する
            isSuccess = m_salesManager.Start();
            if (!isSuccess) {

                return false;
            }

            // 開始する
            isSuccess = m_mainForm.Start();
            if (!isSuccess) {

                return false;
            }

            return true;
        }

        #endregion

        #region

        /// <summary>
        /// イベントを登録する
        /// </summary>
        private void AddEventHandler() {

            m_mainForm.AcceptMoneyEvent += new MainFrom.AcceptMoneyEventHandler(this.AcceptMoneyEvent);
            m_mainForm.InputMoneyEvent += new MainFrom.InputMoneyEventHandler(this.InputMoneyEvent);
            m_mainForm.GiveChangeEvent += new MainFrom.GiveChangeEventHandler(this.GiveChangeEvent);

            return;
        }

        /// <summary>
        /// お金を受け付ける
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptMoneyEvent(object sender, EventArgs e) {

            try {

                // お金を受け付ける
                m_mainForm.AcceptMoney();
            }
            catch (Exception) {

            }

            return;
        }

        /// <summary>
        /// お金投入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moneyInfoList"></param>
        private void InputMoneyEvent(object sender, MoneyInfoList moneyInfoList) {

            try {

                // 投入金額を更新する
                m_salesManager.UpdateInputMoney(moneyInfoList);

                // 購入を受け付ける
                m_mainForm.AcceptPurchace();
            }
            catch (Exception) {

            }

            return;
        }

        /// <summary>
        /// お金投入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moneyInfoList"></param>
        private void GiveChangeEvent(object sender, EventArgs e) {

            MoneyInfoList moneyInfo = null;

            try {

                moneyInfo = new MoneyInfoList();

                // 釣り銭情報を取得する
                m_salesManager.GiveChange(ref moneyInfo);

                // 釣り銭を出す
                m_mainForm.GiveChange(moneyInfo);
            }
            catch (Exception) {

            }

            return;
        }

        #endregion
    }
}
