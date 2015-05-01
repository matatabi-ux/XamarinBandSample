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
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Sensors;
using global::Microsoft.Band.Tiles;
using Microsoft.Practices.Unity;
using XamarinBandSample.iOS.Band.Personalizations;
using XamarinBandSample.iOS.Band.Sensors;
using Native = ios::Microsoft.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 Microsoft Band デバイス情報のインターフェース
    /// </summary>
    public class NativeBandClient : IBandClient
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private Native.BandClient client = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public NativeBandClient(Native.BandClient client)
        {
            this.client = client;

            App.Container.RegisterInstance<IBandSensorManager>(new NativeBandSensorManager(this.client), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandPersonalizationManager>(new NativeBandPersonalizationManager(this.client), new ContainerControlledLifetimeManager());
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
        [Obsolete("CancellationToken is not supported for iOS.")]
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
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            return this.GetHardwareVersionAsync();
        }

        /// <summary>
        /// プッシュ通知管理クラス
        /// </summary>
        public IBandNotificationManager NotificationManager
        {
            get { return App.Container.Resolve<IBandNotificationManager>(); }
        }

        /// <summary>
        /// カスタマイズ設定管理クラス
        /// </summary>
        public IBandPersonalizationManager PersonalizationManager
        {
            get { return App.Container.Resolve<IBandPersonalizationManager>(); }
        }

        /// <summary>
        /// センサー管理クラス
        /// </summary>
        public IBandSensorManager SensorManager
        {
            get { return App.Container.Resolve<IBandSensorManager>(); }
        }

        /// <summary>
        /// タイル管理クラス
        /// </summary>
        public IBandTileManager TileManager
        {
            get { return App.Container.Resolve<IBandTileManager>(); }
        }

        /// <summary>
        /// 破棄処理
        /// </summary>
        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}