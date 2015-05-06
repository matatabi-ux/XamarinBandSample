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
    /// iOS �p���O���f�[�^
    /// </summary>
    public class NativeBandUltravioletLightReading : IBandUVReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandUltravioletLightReading(Native.Sensors.BandSensorUVData data)
        {
            this.Timestamp = DateTime.Now;
            this.IndexLevel = UVIndexLevel.None;
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.High)
            {
                this.IndexLevel = UVIndexLevel.High;
            }
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.Medium)
            {
                this.IndexLevel = UVIndexLevel.Medium;
            }
            if (data.UVIndexLevel == Native.Sensors.UVIndexLevel.Low)
            {
                this.IndexLevel = UVIndexLevel.Low;
            }
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// ���O�����x��
        /// </summary>
        public UVIndexLevel IndexLevel { get; private set; }
    }
}