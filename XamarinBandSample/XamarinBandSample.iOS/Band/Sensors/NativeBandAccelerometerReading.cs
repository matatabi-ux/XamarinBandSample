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
    /// iOS �p�����x�f�[�^
    /// </summary>
    public class NativeBandAccelerometerReading : IBandAccelerometerReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandAccelerometerReading(Native.Sensors.BandSensorAccelerometerData data)
        {
            this.Timestamp = DateTime.Now;
            this.AccelerationX = data.AccelerationX;
            this.AccelerationY = data.AccelerationY;
            this.AccelerationZ = data.AccelerationZ;
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// X �������x
        /// </summary>
        public double AccelerationX { get; private set; }

        /// <summary>
        /// Y �������x
        /// </summary>
        public double AccelerationY { get; private set; }

        /// <summary>
        /// Z �������x
        /// </summary>
        public double AccelerationZ { get; private set; }
    }
}