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
    /// iOS 用加速度センサー
    /// </summary>
    public class NativeBandAcceleromerter : NativeBandSensorBase<IBandAccelerometerReading>
    {
        /// <summary>
        /// 加速度センサー
        /// </summary>
        private Native.Sensors.AccelerometerSensor sensor = null;

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandAccelerometerReading>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandAcceleromerter(Native.Sensors.IBandSensorManager manager) : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateAccelerometerSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// センサー値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnReadingChanged(object sender, Native.Sensors.BandSensorDataEventArgs<Native.Sensors.BandSensorAccelerometerData> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandAccelerometerReading>(new NativeBandAccelerometerReading(e.SensorReading)));
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        public override Task<bool> StartReadingsAsync()
        {
            this.sensor.StartReadings();
            return Task.FromResult(true);
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