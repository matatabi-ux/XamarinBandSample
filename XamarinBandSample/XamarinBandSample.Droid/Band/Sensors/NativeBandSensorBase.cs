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
{   /// <summary>
    /// センサー基底クラス
    /// </summary>
    /// <typeparam name="T">センサー情報クラス</typeparam>
    public abstract class NativeBandSensorBase<T> : IBandSensor<T> where T : IBandSensorReading
    {
        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        protected static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>
        {
            TimeSpan.FromMilliseconds(128),
            TimeSpan.FromMilliseconds(32),
            TimeSpan.FromMilliseconds(16),
        };

        /// <summary>
        /// 対応可否フラグ
        /// </summary>
        public virtual bool IsSupported
        {
            get { return true; }
        }

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public abstract event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandSensorBase(Native.Sensors.IBandSensorManager manager)
        {
            this.ReportingInterval = NativeSupportedReportingIntervals.First();
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <remarks>中断は非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public virtual Task StartReadingsAsync(CancellationToken token)
        {
            return this.StartReadingsAsync();
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>Task</returns>
        public abstract Task StartReadingsAsync();

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <remarks>非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public virtual Task StopReadingsAsync(CancellationToken token)
        {
            return this.StopReadingsAsync();
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <returns>Task</returns>
        public abstract Task StopReadingsAsync();

        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        public virtual IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// センサー検出インターバル時間
        /// </summary>
        /// <remarks>非対応</remarks>
        public virtual TimeSpan ReportingInterval { get; set; }

        /// <summary>
        /// サンプリング設定の取得
        /// </summary>
        /// <returns>サンプリング設定</returns>
        public virtual Native.Sensors.SampleRate GetSampleRate()
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
            return sampleRate;
        }
    }
}