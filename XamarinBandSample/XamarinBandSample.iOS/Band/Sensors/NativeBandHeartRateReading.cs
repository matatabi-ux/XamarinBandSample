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
    /// iOS 用心拍数データ
    /// </summary>
    public class NativeBandHeartRateReading : IBandHeartRateReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandHeartRateReading(Native.Sensors.BandSensorHeartRateData data)
        {
            this.Timestamp = DateTime.Now;
			this.HeartRate = (int)data.HeartRate;

            if (data.Quality == Native.Sensors.HeartRateQuality.Locked)
            {
                this.Quality = HeartRateQuality.Locked;
            }
            if (data.Quality == Native.Sensors.HeartRateQuality.Acquiring)
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