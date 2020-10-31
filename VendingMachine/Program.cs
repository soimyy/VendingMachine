using System;

using VendingMachine.Data;
using VendingMachine.Maneger;
using VendingMachine.Controller;

namespace VendingMachine
{
    class Program
    {

        static private SalesParameter m_salesParameter = null;
        static private ApplicationController m_applicationController = null;

        static private bool isEnd = false;

        static void Main(string[] args)
        {

            bool isSuccess = false;

            try {

                m_salesParameter = new SalesParameter();

                // パラメータを読み込み
                isSuccess = m_salesParameter.ReadParmeterFile();
                if (!isSuccess) {

                    return;
                }
                m_applicationController = new ApplicationController(m_salesParameter);

                // アプリケーション開始
                m_applicationController.Start();

                while(!isEnd) {
                }

            }
            catch (Exception) {

                Console.WriteLine("[Error] StartUp");
                return;
            }

            return;
        }
    }
}
