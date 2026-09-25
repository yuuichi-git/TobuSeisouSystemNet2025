using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Common {
    public static class NetworkUtility {
        /// <summary>
        /// コンピューター名を取得
        /// </summary>
        /// <returns></returns>
        public static string GetPcName() {
            return SystemInformation.ComputerName;
        }

        /// <summary>
        /// IPで指定したコンピューター名を取得
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetPcNameFromIp(string ip) {
            try {
                IPHostEntry entry = Dns.GetHostEntry(ip);
                return entry.HostName;                                  // 例: PC01.tobu-seisou.local
            } catch {
                return null;                                            // 逆引きできない場合
            }
        }

        /// <summary>
        /// IPv4 アドレスを取得
        /// </summary>
        public static string GetIpAddress() {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return iPHostEntry.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// デフォルトゲートウェイアドレスを取得
        /// </summary>
        public static string GetDefaultGatewayAddress() {
            return NetworkInterface.GetAllNetworkInterfaces().Where(nic => nic.OperationalStatus == OperationalStatus.Up &&
                                                                           nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                                             .SelectMany(nic => nic.GetIPProperties()?.GatewayAddresses)
                                                             .Select(g => g?.Address?.ToString())
                                                             .FirstOrDefault(addr => !string.IsNullOrWhiteSpace(addr)) ?? string.Empty;
        }

        /// <summary>
        /// 接続場所を取得
        /// </summary>
        /// <param name="gateway">DefaultGatewayアドレス</param>
        /// <returns></returns>
        public static string GetConnectLocation(string gateway) {
            switch(gateway) {
                case "192.168.1.5":
                    return "本社";
                case "192.168.10.1":
                    return "三郷車庫";
                case "192.168.11.1":
                    return "リサイクルセンター";
                default:
                    return string.Empty;
            }
        }
    }
}
