#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

extern alias android;

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

using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Sensors;
using global::Microsoft.Band.Tiles;
using Microsoft.Practices.Unity;
using Native = android::Microsoft.Band;
using XamarinBandSample.Droid.Band.Sensors;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用 Microsoft Band デバイス情報のインターフェース
    /// </summary>
    public class NativeBandClient : IBandClient
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private Native.IBandClient client = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public NativeBandClient(Native.IBandClient client)
        {
            this.client = client;

            App.Container.RegisterInstance<IBandSensorManager>(new NativeBandSensorManager(this.client), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <returns>ファームウェアバージョン</returns>
        public Task<string> GetFirmwareVersionAsync()
        {
            return Native.BandClientExtensions.GetFirmwareVersionTaskAsync(this.client);
        }

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <returns>ファームウェアバージョン</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return Native.BandClientExtensions.GetHardwareVersionTaskAsync(this.client);
        }

        /// <summary>
        /// ハードウェアバージョンを取得する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <returns>ハードウェアバージョン</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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