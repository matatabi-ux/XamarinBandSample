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
    /// Android 用肌温度センサーデータ
    /// </summary>
    public class NativeBandSkinTemperatureReading : IBandSkinTemperatureReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandSkinTemperatureReading(Native.Sensors.IBandSkinTemperatureEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
            this.Temperature = data.Temperature;
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// 肌温度
        /// </summary>
        public double Temperature { get; private set; }
    }
}