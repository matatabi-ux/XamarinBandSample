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
    /// iOS 用センサー基底クラス
    /// </summary>
    /// <typeparam name="T">センサー情報クラス</typeparam>
    public abstract class NativeBandSensorBase<T> : IBandSensor<T> where T : IBandSensorReading
    {
        /// <summary>
        /// 対応センサー検出インターバル時間
        /// </summary>
        protected static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>();

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
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <remarks>中断は非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public virtual Task<bool> StartReadingsAsync(CancellationToken token)
        {
            return this.StartReadingsAsync();
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        public abstract Task<bool> StartReadingsAsync();

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <remarks>非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
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
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// センサー検出インターバル時間
        /// </summary>
        /// <remarks>非対応</remarks>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual TimeSpan ReportingInterval
        {
            get { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
            set { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
        }

        /// <summary>
        /// 現在のユーザー承諾状態を取得する
        /// </summary>
        /// <returns>ユーザー承諾状態</returns>
        public virtual UserConsent GetCurrentUserConsent()
        {
            return UserConsent.Granted;
        }

        /// <summary>
        /// センサー利用のユーザー承諾を要求する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <remarks>非対応</remarks>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual Task<bool> RequestUserConsentAsync(CancellationToken token)
        {
            return this.RequestUserConsentAsync();
        }

        /// <summary>
        /// センサー利用のユーザー承諾を要求する
        /// </summary>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        public virtual Task<bool> RequestUserConsentAsync()
        {
            return Task.FromResult(true);
        }
    }
}