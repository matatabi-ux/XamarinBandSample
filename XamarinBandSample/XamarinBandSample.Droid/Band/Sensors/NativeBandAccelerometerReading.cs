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
    /// Android 用加速度データ
    /// </summary>
    public class NativeBandAccelerometerReading : IBandAccelerometerReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandAccelerometerReading(Native.Sensors.IBandAccelerometerEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
            this.AccelerationX = data.AccelerationX;
            this.AccelerationY = data.AccelerationY;
            this.AccelerationZ = data.AccelerationZ;
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// X 軸加速度
        /// </summary>
        public double AccelerationX { get; private set; }

        /// <summary>
        /// Y 軸加速度
        /// </summary>
        public double AccelerationY { get; private set; }

        /// <summary>
        /// Z 軸加速度
        /// </summary>
        public double AccelerationZ { get; private set; }
    }
}