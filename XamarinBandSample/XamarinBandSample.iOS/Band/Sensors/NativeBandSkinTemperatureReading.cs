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
    /// iOS �p�����x�Z���T�[�f�[�^
    /// </summary>
    public class NativeBandSkinTemperatureReading : IBandSkinTemperatureReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandSkinTemperatureReading(Native.Sensors.BandSensorSkinTemperatureData data)
        {
            this.Timestamp = DateTime.Now;
            this.Temperature = data.Temperature;
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// �����x
        /// </summary>
        public double Temperature { get; private set; }
    }
}