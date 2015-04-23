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
    ///  iOS �p�p���x�f�[�^
    /// </summary>
    class NativeBandGyroscopeReading : IBandGyroscopeReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandGyroscopeReading(Native.Sensors.BandSensorGyroscopeData data)
        {
            this.Timestamp = DateTime.Now;
            this.AngularVelocityX = data.X;
            this.AngularVelocityY = data.Y;
            this.AngularVelocityZ = data.Z;
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// X ���p���x
        /// </summary>
        public double AngularVelocityX { get; private set; }

        /// <summary>
        /// Y ���p���x
        /// </summary>
        public double AngularVelocityY { get; private set; }

        /// <summary>
        /// Z ���p���x
        /// </summary>
        public double AngularVelocityZ { get; private set; }

        /// <summary>
        /// X ���p�����x
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationX { get; private set; }

        /// <summary>
        /// Y ���p�����x
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationY { get; private set; }

        /// <summary>
        /// Z ���p�����x
        /// </summary>
        [Obsolete("Acceleration by Gyroscope is not supported for iOS.")]
        public double AccelerationZ { get; private set; }
    }
}