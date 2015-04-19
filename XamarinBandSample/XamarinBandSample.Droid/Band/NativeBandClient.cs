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
using Native = android::Microsoft.Band;

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
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            return this.GetHardwareVersionAsync();
        }

        //TODO:後で実装予定

        public IBandNotificationManager NotificationManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandPersonalizationManager PersonalizationManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandSensorManager SensorManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandTileManager TileManager
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
        }
    }
}