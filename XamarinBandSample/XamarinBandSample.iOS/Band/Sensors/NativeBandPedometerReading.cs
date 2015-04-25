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
    /// iOS �p�����f�[�^
    /// </summary>
    public class NativeBandPedometerReading : IBandPedometerReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandPedometerReading(Native.Sensors.BandSensorPedometerData data)
        {
            this.Timestamp = DateTime.Now;
            this.TotalSteps = (long) data.TotalSteps;
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// �������v
        /// </summary>
        public long TotalSteps { get; private set; }
    }
}