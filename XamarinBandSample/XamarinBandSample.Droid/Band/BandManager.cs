using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用 Microsoft Band デバイス管理クラス
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
                from i in BandClientManager.Instance.GetPairedBands()
                select new BandDevice(i) as IBandDevice).ToList());
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
                throw new InvalidOperationException("Parameter 'device' is not BandDevice type.");
            }
            var client = BandClientManager.Instance.Create(Application.Context, info.DeviceInfo);

            if (client != null && !client.IsConnected)
            {
               var result = await client.ConnectTaskAsync();
            }

            return new BandService(client);
        }
    }
}