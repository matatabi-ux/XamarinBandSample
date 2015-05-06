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
    /// Androrid �p���O���f�[�^
    /// </summary>
    class NativeBandUltravioletLightReading : IBandUVReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandUltravioletLightReading(Native.Sensors.IBandUVEvent data)
        {
            this.Timestamp = DateTimeOffset.FromFileTime(data.Timestamp);
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