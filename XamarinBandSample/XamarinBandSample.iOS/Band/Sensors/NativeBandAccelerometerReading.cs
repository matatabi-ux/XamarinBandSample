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
    /// iOS 用加速度データ
    /// </summary>
    public class NativeBandAccelerometerReading : IBandAccelerometerReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandAccelerometerReading(Native.Sensors.BandSensorAccelerometerData data)
        {
            this.Timestamp = DateTime.Now;
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