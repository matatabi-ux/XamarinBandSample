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
using Microsoft.Band.Sensors;
using Microsoft.Practices.Unity;
using Native = ios::Microsoft.Band;
using UIKit;

namespace XamarinBandSample.iOS.Band.Sensors
{
    /// <summary>
    /// iOS 用着用状態センサー
    /// </summary>
    public class NativeBandContactSensor : NativeBandSensorBase<IBandContactReading>
    {
        /// <summary>
        /// 着用状態センサー
        /// </summary>
        private Native.Sensors.ContactSensor sensor = null;

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
        protected void OnReadingChanged(object sender, Native.Sensors.BandSensorDataEventArgs<Native.Sensors.BandSensorContactData> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandContactReading>(new NativeBandContactReading(e.SensorReading)));
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>Task</returns>
        public override Task StartReadingsAsync()
        {
            return Task.Run(() => this.sensor.StartReadings());
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return Task.Run(() => this.sensor.StopReadings());
        }
    }
}