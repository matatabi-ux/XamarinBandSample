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

using global::Microsoft.Band.Sensors;
using Native = android::Microsoft.Band;

namespace XamarinBandSample.Droid.Band.Sensors
{
    /// <summary>
    /// Android 用歩数データ
    /// </summary>
    public class NativeBandPedometer : NativeBandSensorBase<IBandPedometerReading>
    {
        /// <summary>
        /// 歩数センサー
        /// </summary>
        private Native.Sensors.PedometerSensor sensor = null;

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandPedometerReading>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandPedometer(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreatePedometerSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// センサー値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        protected void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandPedometerEvent> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandPedometerReading>(new NativeBandPedometerReading(e.SensorReading)));
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
    }
}