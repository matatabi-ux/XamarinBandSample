#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

extern alias ios;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using Native = ios::Microsoft.Band;
using UIKit;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 BandClientManager
    /// </summary>
    public class NativeBandClientManager : IBandClientManager
    {
        /// <summary>
        /// 登録 Band デバイス情報を取得する
        /// </summary>
        /// <returns>登録 Band デバイス情報</returns>
        public Task<IBandInfo[]> GetBandsAsync()
        {
            return Task.FromResult<IBandInfo[]>((
                from c in new List<Native.BandClient>(Native.BandClientManager.Instance.AttachedClients)
                select new NativeBandInfo(c) as IBandInfo).ToArray());
        }

        /// <summary>
        /// Band デバイスに接続する
        /// </summary>
        /// <param name="device">Band デバイス情報</param>
        /// <returns>Band 接続サービス</returns>
        public async Task<IBandClient> ConnectAsync(IBandInfo bandInfo)
        {
            var client = Native.BandClientManager.Instance.AttachedClients.FirstOrDefault(c => c.Name.Equals(bandInfo.Name));

            if (client != null && !client.IsDeviceConnected)
            {
                await Native.BandClientManagerExtensions.ConnectTaskAsync(Native.BandClientManager.Instance, client);
            }

            return new NativeBandClient(client);
        }
    }
}