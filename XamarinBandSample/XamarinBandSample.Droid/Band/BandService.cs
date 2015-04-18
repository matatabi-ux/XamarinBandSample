using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用 Microsoft Band デバイス情報のインターフェース
    /// </summary>
    public class BandService : IBandService
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private IBandClient client = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public BandService(IBandClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <returns>ファームウェアバージョン</returns>
        public Task<string> GetFirmwareVersionAsync()
        {
            return this.client.GetFirmwareVersionTaskAsync();
        }

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <returns>ファームウェアバージョン</returns>
        public Task<string> GetFirmwareVersionAsync(CancellationToken token)
        {
            return this.GetFirmwareVersionAsync();
        }

        /// <summary>
        /// ハードウェアバージョンを取得する
        /// </summary>
        /// <returns>ハードウェアバージョン</returns>
        public Task<string> GetHardwareVersionAsync()
        {
            return this.client.GetHardwareVersionTaskAsync();
        }

        /// <summary>
        /// ハードウェアバージョンを取得する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <returns>ハードウェアバージョン</returns>
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            return this.GetHardwareVersionAsync();
        }
    }
}