/*
 * 2025-05-24
 */
using System.Diagnostics;

namespace Common {
    public class MapUtility {
        /// <summary>
        /// MapOpen
        /// URLエンコーディング
        /// string urlEnc = System.Net.WebUtility.UrlEncode(address);
        /// Process.Start("https://www.google.com/maps?q=[" + urlEnc + "]&t=[h]&hl=[ja]");
        /// 参考にしたHP　https://qiita.com/yasukotelin/items/605b9d4260a8c9ebcbeb
        /// </summary>
        /// <param name="selectedAddress"></param>
        public void MapOpen(string selectedAddress) {
            var addressSplit = new AddressSplitUtility(selectedAddress);
            var address = string.Concat(addressSplit.PrefecturesAddress, addressSplit.CityAddress, addressSplit.OtherAddress);

            ProcessStartInfo processStartInfo = new();
            processStartInfo.FileName = "https://www.google.co.jp/maps/search/" + address;
            processStartInfo.UseShellExecute = true;
            try {
                Process.Start(processStartInfo);
            } catch (Exception exception) {
                Console.WriteLine("MapOpen" + ":" + exception.Message);
            }
        }

        /// <summary>
        /// RouteMapOpen
        /// https://www.google.co.jp/maps/dir/ 
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="endAddress"></param>
        public void RouteMapOpen(string startAddress, string endAddress) {
            AddressSplitUtility startAddressSplit = new(startAddress);
            AddressSplitUtility endAddressSplit = new(endAddress);
            String address1 = string.Concat(startAddressSplit.PrefecturesAddress, startAddressSplit.CityAddress, startAddressSplit.OtherAddress);
            String address2 = string.Concat(endAddressSplit.PrefecturesAddress, endAddressSplit.CityAddress, endAddressSplit.OtherAddress);

            ProcessStartInfo processStartInfo = new();
            processStartInfo.FileName = "https://www.google.co.jp/maps/dir/" + address1 + "/" + address2 + "/";
            processStartInfo.UseShellExecute = true;

            try {
                Process.Start(processStartInfo);
            } catch (Exception exception) {
                Console.WriteLine("RouteMapOpen" + ":" + exception.Message);
            }
        }
    }
}
