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
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjGyl/VkV+XU9AclRHQmJPYVF2R2VJfl56d1ZMYFtBJAtUQF1hT35Xd0NhW3xac3RVR2RYWkd1");

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new StartProject());
        }
    }
}