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
using System.Threading;
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
    /// iOS �p���p��ԃZ���T�[
    /// </summary>
    public class NativeBandContactReading : IBandContactReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandContactReading(Native.Sensors.BandSensorContactData data)
        {
            this.Timestamp = DateTime.Now;
            this.State = BandContactState.NotWorn;
            if (data.WornState == Native.Sensors.BandContactStatus.Worn)
            {
                this.State = BandContactState.Worn;
            }
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// ���p���
        /// </summary>
        public BandContactState State { get; private set; }
    }

}