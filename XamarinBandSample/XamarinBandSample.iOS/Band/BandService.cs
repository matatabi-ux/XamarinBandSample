using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using XamarinBandSample.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 Microsoft Band デバイス情報のインターフェース
    /// </summary>
    public class BandService : IBandService
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private BandClient client = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public BandService(BandClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <returns>ファームウェアバージョン</returns>
        public async Task<string> GetFirmwareVersionAsync()
        {
            NSString version = await this.client.GetFirmwareVersionAsyncAsync();
            return version != null ? version.ToString() : string.Empty;
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
        public async Task<string> GetHardwareVersionAsync()
        {
            NSString version = await this.client.GetHardwareVersionAsycAsync();
            return version != null ? version.ToString() : string.Empty;
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