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
    /// iOS �p�S�����f�[�^
    /// </summary>
    public class NativeBandHeartRateReading : IBandHeartRateReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
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
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// �S����
        /// </summary>
        public int HeartRate { get; private set; }

        /// <summary>
        /// �v����
        /// </summary>
        public HeartRateQuality Quality { get; private set; }
    }
}