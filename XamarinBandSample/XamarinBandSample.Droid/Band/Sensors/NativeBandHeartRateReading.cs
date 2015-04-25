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
    /// Android 用心拍数データ
    /// </summary>
    public class NativeBandHeartRateReading : IBandHeartRateReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandHeartRateReading(Native.Sensors.IBandHeartRateEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
            this.HeartRate = data.HeartRate;

            if (data.Quality == Native.Sensors.HeartRateQuality.Locked)
            {
                this.Quality = HeartRateQuality.Locked;
            }
            if (data.Quality == Native.Sensors.HeartRateQuality.Aquiring)
            {
                this.Quality = HeartRateQuality.Acquiring;
            }
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }
        
        /// <summary>
        /// 心拍数
        /// </summary>
        public int HeartRate { get; private set; }

        /// <summary>
        /// 計測状況
        /// </summary>
        public HeartRateQuality Quality { get; private set; }
    }
}