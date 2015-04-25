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
    /// Android �p���p���
    /// </summary>
    public class NativeBandContactReading : IBandContactReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandContactReading(Native.Sensors.IBandContactEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
            this.State = BandContactState.NotWorn;
            if (data.ContactStatus == Native.Sensors.BandContactStatus.Worn)
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