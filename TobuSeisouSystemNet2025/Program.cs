using Syncfusion.Licensing;

namespace TobuSeisouSystemNet2025 {
    internal static class Program {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            /*
             * 2026-04-10 - Syncfusionのライセンス登録(2026年4月10日現在の最新バージョンは2024.3.0)。
             */
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JHaF5cWWdCekx3Q3xbf1x2ZFRHal5XTnJZUj0eQnxTdENjXX9XcndXQGRaV0JyXEleYA==");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new StartProject());
        }
    }
}