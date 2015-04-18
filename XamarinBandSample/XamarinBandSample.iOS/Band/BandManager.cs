using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Band;
using UIKit;
using XamarinBandSample.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 BandClientManager
    /// </summary>
    public class BandManager : IBandManager
    {
        /// <summary>
        /// 登録 Band デバイス情報を取得する
        /// </summary>
        /// <returns>登録 Band デバイス情報</returns>
        public Task<IList<IBandDevice>> GetBandsAsync()
        {
            return Task.FromResult<IList<IBandDevice>>((
                from c in BandClientManager.Instance.AttachedClients
                select new BandDevice(c) as IBandDevice).ToList());
        }

        /// <summary>
        /// Band デバイスに接続する
        /// </summary>
        /// <param name="device">Band デバイス情報</param>
        /// <returns>Band 接続サービス</returns>
        public async Task<IBandService> ConnectAsync(IBandDevice device)
        {
            var client = BandClientManager.Instance.AttachedClients.FirstOrDefault(c => c.Name.Equals(device.Name));

            if (client != null && !client.IsDeviceConnected)
            {
                await BandClientManager.Instance.ConnectTaskAsync(client);
            }

            return new BandService(client);
        }
    }
}