using System.Net;
using System.Net.Sockets;

namespace Common {
    public class Network {
        /// <summary>
        /// IPアドレスを取得
        /// </summary>
        /// <returns>IPアドレス</returns>
        public string GetIpAddress() {
            //IPアドレス用変数
            string ip = string.Empty;

            //自身のIPアドレスの一覧を取得する
            string hostname = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(hostname);

            //一覧からIPv4アドレスのみ抽出する
            foreach (IPAddress iPAddress in ips) {
                //IPv4を対象とする
                if (iPAddress.AddressFamily.Equals(AddressFamily.InterNetwork)) {
                    ip = iPAddress.ToString();
                    break;
                }
            }
            return ip;
        }

        /// <summary>
        /// 接続場所を取得
        /// </summary>
        /// <returns></returns>
        public string GetConnectLocation() {
            string[] arrayIpAddress = GetIpAddress().Split('.');
            string ipAddress = string.Concat(arrayIpAddress[0], ".", arrayIpAddress[1], ".", arrayIpAddress[2]);
            switch (ipAddress) {
                case "192.168.1":
                    return "本社";
                default:
                    return string.Empty;
            }
        }
    }
}
