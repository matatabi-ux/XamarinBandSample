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
using System.Net;
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
    /// iOS 用紫外線データ
    /// </summary>
    public class NativeBandUltravioletLightReading : IBandUltravioletLightReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandUltravioletLightReading(Native.Sensors.BandSensorUVData data)
        {
            this.Timestamp = DateTime.Now;
            this.ExposureLevel = UltravioletExposureLevel.None;
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.High)
            {
                this.ExposureLevel = UltravioletExposureLevel.High;
            }
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.Medium)
            {
                this.ExposureLevel = UltravioletExposureLevel.Medium;
            }
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.Low)
            {
                this.ExposureLevel = UltravioletExposureLevel.Low;
            }
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// 紫外線レベル
        /// </summary>
        public UltravioletExposureLevel ExposureLevel { get; private set; }
    }
}