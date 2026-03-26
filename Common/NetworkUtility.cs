using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Common {
    public static class NetworkUtility {
        /// <summary>
        /// IPv4 アドレスを取得
        /// </summary>
        public static string GetIpAddress() {
            var ip = Dns.GetHostAddresses(Dns.GetHostName())
                        .FirstOrDefault(addr => addr.AddressFamily == AddressFamily.InterNetwork);
            return ip?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// デフォルトゲートウェイアドレスを取得
        /// </summary>
        public static string GetDefaultGatewayAddress() {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic =>
                    nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(nic => nic.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address?.ToString())
                .FirstOrDefault(addr => !string.IsNullOrWhiteSpace(addr))
                ?? string.Empty;
        }

        /// <summary>
        /// 接続場所を取得
        /// </summary>
        public static string GetConnectLocation() {
            //string[] parts = GetIpAddress().Split('.');
            //if (parts.Length < 3)
            //    return string.Empty;

            //string ipAddress = string.Join(".", parts[..3]);

            //return ipAddress switch {
            //    "192.168.1" => "本社",
            //    "192.168.10" => "三郷車庫",
            //    _ => string.Empty
            //};
            // デフォルトゲートウェイ取得
            string? gateway = NetworkInterface.GetAllNetworkInterfaces()
                .Where(nic =>
                    nic.OperationalStatus == OperationalStatus.Up &&
                    nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(nic => nic.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address?.ToString())
                .FirstOrDefault(addr => !string.IsNullOrWhiteSpace(addr));

            if (string.IsNullOrWhiteSpace(gateway))
                return string.Empty;

            // 完全一致で判定（第四オクテットまで）
            return gateway switch {
                "192.168.1.5" => "本社",
                "192.168.10.1" => "三郷車庫",
                "192.168.11.1" => "リサイクルセンター",
                _ => string.Empty
            };
        }
    }
}
