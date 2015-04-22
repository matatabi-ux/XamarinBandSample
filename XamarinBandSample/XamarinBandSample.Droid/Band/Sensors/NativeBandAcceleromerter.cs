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
    /// Android 用加速度センサー
    /// </summary>
    public class NativeBandAcceleromerter : IBandSensor<IBandAccelerometerReading>
    {
        /// <summary>
        /// 加速度センサー
        /// </summary>
        private Native.Sensors.AccelerometerSensor sensor = null;

        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        private static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>
        {
            TimeSpan.FromMilliseconds(128),
            TimeSpan.FromMilliseconds(32),
            TimeSpan.FromMilliseconds(16),
        };

        /// <summary>
        /// 対応可否フラグ
        /// </summary>
        public bool IsSupported
        {
            get { return true; }
        }

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public event EventHandler<BandSensorReadingEventArgs<IBandAccelerometerReading>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandAcceleromerter(Native.Sensors.IBandSensorManager manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateAccelerometerSensor(manager);
            this.ReportingInterval = NativeSupportedReportingIntervals.First();
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// センサー値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandAccelerometerEvent> e)
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
        /// <remarks>中断は非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task StartReadingsAsync(System.Threading.CancellationToken token)
        {
            return this.StartReadingsAsync();
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>Task</returns>
        public Task StartReadingsAsync()
        {
            var sampleRate = Native.Sensors.SampleRate.Ms128;
            switch (this.ReportingInterval.Milliseconds)
            {
                case 16:
                    sampleRate = Native.Sensors.SampleRate.Ms16;
                    break;

                case 32:
                    sampleRate = Native.Sensors.SampleRate.Ms32;
                    break;
            }
            return this.sensor.StartReadingsTaskAsync(sampleRate);
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <remarks>非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task StopReadingsAsync(CancellationToken token)
        {
            return this.StopReadingsAsync();
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <returns>Task</returns>
        public Task StopReadingsAsync()
        {
            return this.sensor.StopReadingsTaskAsync();
        }

        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        public IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// センサー検出インターバル時間
        /// </summary>
        /// <remarks>非対応</remarks>
        public TimeSpan ReportingInterval { get; set; }
    }
}