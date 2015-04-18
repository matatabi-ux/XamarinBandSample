using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Phone.Band
{
    /// <summary>
    /// Windows Phone 用 BandClientManager
    /// </summary>
    public class BandManager : IBandManager
    {
        /// <summary>
        /// 登録 Band デバイス情報を取得する
        /// </summary>
        /// <returns>登録 Band デバイス情報</returns>
        public async Task<IList<IBandDevice>> GetBandsAsync()
        {
            return (from i in await BandClientManager.Instance.GetBandsAsync()
                    select new BandDevice(i) as IBandDevice).ToList();
        }

        /// <summary>
        /// Band デバイスに接続する
        /// </summary>
        /// <param name="device">Band デバイス情報</param>
        /// <returns>Band 接続サービス</returns>
        public async Task<IBandService> ConnectAsync(IBandDevice device)
        {
            var info = device as BandDevice;
            if (info == null)
            {
                throw  new InvalidOperationException("Parameter 'device' is not BandDevice type.");
            }

            var client = await BandClientManager.Instance.ConnectAsync(info.DeviceInfo);

            return new BandService(client);
        }
    }
}