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
    public class NativeBandAcceleromerter : IBandSensor<IBandAccelerometerReading>
    {
        /// <summary>
        /// 加速度センサー
        /// </summary>
        private Native.Sensors.AccelerometerSensor sensor = null;

        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        private static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>();

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
        /// <remarks>中断は非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
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
            return Task.Run(() => this.sensor.StartReadings());
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
            return Task.Run(() => this.sensor.StopReadings());
        }

        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// センサー検出インターバル時間
        /// </summary>
        /// <remarks>非対応</remarks>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public TimeSpan ReportingInterval
        {
            get { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
            set { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
        }
    }
}