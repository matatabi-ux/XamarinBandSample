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
    /// Androrid 用紫外線データ
    /// </summary>
    class NativeBandUltravioletLightReading : IBandUltravioletLightReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandUltravioletLightReading(Native.Sensors.IBandUVEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
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