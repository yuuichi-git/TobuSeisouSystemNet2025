/*
 * 参考資料
 * https://docs.microsoft.com/ja-jp/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-6.0
 */
using System.Diagnostics;

namespace Common {
    public class FilesUtility {
        /// <summary>
        /// MicrosoftAccess
        /// </summary>
        /// <param name="filePath"></param>
        public void MicrosoftAccess(string filePath) {
            ProcessStartInfo processStartInfo = new();
            processStartInfo.UseShellExecute = true;
            processStartInfo.FileName = filePath;
            Process.Start(processStartInfo);
        }

        /// <summary>
        /// OpenFolder
        /// </summary>
        /// <param name="filePath"></param>
        public void OpenFolder(string filePath) {
            ProcessStartInfo processStartInfo = new();
            processStartInfo.UseShellExecute = true;
            processStartInfo.FileName = filePath;
            Process.Start(processStartInfo);
        }
    }
}
