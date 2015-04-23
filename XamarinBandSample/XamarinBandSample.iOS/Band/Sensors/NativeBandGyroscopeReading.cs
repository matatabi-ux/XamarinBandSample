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
    ///  iOS 用角速度データ
    /// </summary>
    class NativeBandGyroscopeReading : IBandGyroscopeReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandGyroscopeReading(Native.Sensors.BandSensorGyroscopeData data)
        {
            this.Timestamp = DateTime.Now;
            this.AngularVelocityX = data.X;
            this.AngularVelocityY = data.Y;
            this.AngularVelocityZ = data.Z;
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// X 軸角速度
        /// </summary>
        public double AngularVelocityX { get; private set; }

        /// <summary>
        /// Y 軸角速度
        /// </summary>
        public double AngularVelocityY { get; private set; }

        /// <summary>
        /// Z 軸角速度
        /// </summary>
        public double AngularVelocityZ { get; private set; }

        /// <summary>
        /// X 軸角加速度
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationX { get; private set; }

        /// <summary>
        /// Y 軸角加速度
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationY { get; private set; }

        /// <summary>
        /// Z 軸角加速度
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationZ { get; private set; }
    }
}