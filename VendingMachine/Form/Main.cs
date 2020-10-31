using System;
using System.Collections.Generic;

using VendingMachine.Base;
using VendingMachine.Data;

namespace VendingMachine.Form {

    internal class MainFrom {

        #region delegate

        /// <summary>
        /// お金受付イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moneyInfo"></param>
        public delegate void AcceptMoneyEventHandler(object sender, EventArgs e);

        /// <summary>
        /// お金投入イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="moneyInfo"></param>
        public delegate void InputMoneyEventHandler(object sender, MoneyInfoList moneyInfoList);

        /// <summary>
        /// 釣り銭イベント
        /// </summary>
        /// <param name="sender"></param>
        public delegate void GiveChangeEventHandler(object sender, EventArgs e);

        #endregion

        #region event

        /// <summary>
        /// お金受付イベント
        /// </summary>
        public event AcceptMoneyEventHandler AcceptMoneyEvent;

        /// <summary>
        /// お金投入イベント
        /// </summary>
        public event InputMoneyEventHandler InputMoneyEvent;

        /// <summary>
        /// 釣り銭イベント
        /// </summary>
        public event GiveChangeEventHandler GiveChangeEvent;

        #endregion

        #region enum

        /// <summary>
        /// 操作ID
        /// </summary>
        private enum Operation: int {

            InputMoney = 1,
            Aggregate = 2,
            Purchase = 3,
            GiveChange = 4,
            End = 100,
        };

        #endregion

        #region variable

        /// <summary>
        /// 販売情報
        /// </summary>
        private readonly SalesParameter m_salesParameter = null;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal MainFrom(SalesParameter salesParamter) {

            m_salesParameter = salesParamter;
        }

        #region function

        /// <summary>
        /// 開始する
        /// </summary>
        /// <returns>開始結果</returns>
        internal bool Start() {

            Operation operation = default(Operation);

            try {

                // 初期操作を受け付ける
                operation = this.InitOperation();
                if (operation == Operation.End) {

                    // 不正な入力
                    return false;
                }
                if (operation == Operation.Aggregate) {

                    // 集計
                    this.Aggregate();

                    return true;
                }

                // お金の投入を受け付ける
                if (this.AcceptMoneyEvent != null) {

                    this.AcceptMoneyEvent(this, EventArgs.Empty);

                    return true;
                }
            }
            catch (Exception) {

                return false;
            }
            

            return true;
        }

        /// <summary>
        /// お金を受け付ける
        /// </summary>
        internal void AcceptMoney() {

            // お金を投入する
            this.InputMoney();

            return;
        }

        /// <summary>
        /// 購入を受け付ける
        /// </summary>
        internal void AcceptPurchace() {

            Operation operation = default(Operation);

            // 購入操作を受け付ける
            operation = this.AcceptPurchaseOperation();
            if (operation == Operation.GiveChange) {

                if (this.GiveChangeEvent != null) {

                    this.GiveChangeEvent(this, EventArgs.Empty);

                    return;
                }

                return;
            }

            // 購入を受け付ける



            return;
        }

        /// <summary>
        /// 釣り銭をだす
        /// </summary>
        internal void GiveChange(MoneyInfoList infoList) {

            Console.WriteLine($"返金します");

            this.Aggregate(infoList);

            return;
        }


        /// <summary>
        /// 初期操作を受け付ける
        /// </summary>
        /// <returns>操作ID</returns>
        private Operation InitOperation() {

            string input = string.Empty;

            Console.WriteLine("操作を入力してください");
            Console.WriteLine("1：お金を入れる");
            Console.WriteLine("2：集計");

            input = Console.ReadLine();

            switch (Convert.ToInt32(input)) {

                case (int)Operation.InputMoney:
                    return Operation.InputMoney;

                case (int)Operation.Aggregate:
                    return Operation.Aggregate;
            }

            return Operation.End;
        }

        /// <summary>
        /// 集計する
        /// </summary>
        private void Aggregate() {

            this.Aggregate(m_salesParameter.MoneyInfo);         // お金情報
            this.Aggregate(m_salesParameter.ProductInfo);       // 商品情報

            return;
        }

        /// <summary>
        /// 集計する
        /// </summary>
        /// <param name="info"></param>
        private void Aggregate(SalesInfoListBase infoList) {

            foreach (SalesInfoBase info in infoList.m_salesInfoList) {

                Console.WriteLine($"名前: {info.Name.PadRight(10)}  数: {info.Stock}");
            }

            return;
        }

        /// <summary>
        /// お金を投入する
        /// </summary>
        private void InputMoney() {

            uint number = 0;
            string name = string.Empty;
            string input = string.Empty;

            Console.WriteLine("投入金額を入力してください");

            MoneyInfoList moneyInfoList = new MoneyInfoList();

            foreach (SalesInfoBase info in m_salesParameter.MoneyInfo.m_salesInfoList) {

                Console.Write($"{info.Name} :");
                input = Console.ReadLine();

                // 入力値を数値に変換する
                number = Convert.ToUInt32(input);

                // お金情報を生成する
                MoneyInfo moneyInfo = new MoneyInfo(info.Name, info.Price, number);

                moneyInfoList.m_salesInfoList.Add(moneyInfo);
            }

            // 投入金額を更新する
            if (InputMoneyEvent != null) {

                // 通知する
                this.InputMoneyEvent(this, moneyInfoList);
            }

            return;
        }

        /// <summary>
        /// 商品を購入する
        /// </summary>
        private void BuyProduct() {



            return;
        }

        /// <summary>
        /// 購入操作を受け付ける
        /// </summary>
        /// <returns>操作ID</returns>
        private Operation AcceptPurchaseOperation() {

            Console.WriteLine("操作を入力してください");
            Console.WriteLine("3：購入する");
            Console.WriteLine("4：釣り銭を出す");

            switch (Convert.ToInt32(Console.ReadLine())) {

                case (int)Operation.Purchase:
                    return Operation.Purchase;

                case (int)Operation.GiveChange:
                    return Operation.GiveChange;
            }

            return Operation.End;
        }

        #endregion
    }
}
