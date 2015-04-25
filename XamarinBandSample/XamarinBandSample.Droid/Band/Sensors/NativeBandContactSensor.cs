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

namespace XamarinBandSample.Droid.Band.Sensors
{
    /// <summary>
    /// Android 用着用状態センサー
    /// </summary>
    public class NativeBandContactSensor : NativeBandSensorBase<IBandContactReading>, IBandContactSensor
    {
        /// <summary>
        /// 着用状態センサー
        /// </summary>
        private Native.Sensors.ContactSensor sensor = null;

        /// <summary>
        /// 現在の着用状態
        /// </summary>
        private IBandContactReading contactReading = null;

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandContactReading>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandContactSensor(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateContactSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// センサー値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        protected void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandContactEvent> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            if (this.ReadingChanged == null)
            {
                return;
            }
            var args = new BandSensorReadingEventArgs<IBandContactReading>(new NativeBandContactReading(e.SensorReading));
            this.contactReading = args.SensorReading;
            this.ReadingChanged.Invoke(
                this, args);
            this.ReadingChanged.Invoke(
                this, args);
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>Task</returns>
        public override Task StartReadingsAsync()
        {
            return this.sensor.StartReadingsTaskAsync();
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return this.sensor.StopReadingsTaskAsync();
        }

        /// <summary>
        /// 現在の状態を取得する
        /// </summary>
        /// <returns>現在の状態</returns>
        public Task<IBandContactReading> GetCurrentStateAsync()
        {
            return Task.FromResult<IBandContactReading>(this.contactReading);
        }
    }
}