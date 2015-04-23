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
    ///  Android �p�p���x�f�[�^
    /// </summary>
    class NativeBandGyroscopeReading : IBandGyroscopeReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandGyroscopeReading(Native.Sensors.IBandGyroscopeEvent data)
        {
            this.Timestamp = DateTime.Now;
            this.AngularVelocityX = data.AngularVelocityX;
            this.AngularVelocityY = data.AngularVelocityY;
            this.AngularVelocityZ = data.AngularVelocityZ;
            this.AccelerationX = data.AccelerationX;
            this.AccelerationY = data.AccelerationY;
            this.AccelerationZ = data.AccelerationZ;
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
        public double AccelerationX { get; private set; }

        /// <summary>
        /// Y ���p�����x
        /// </summary>
        public double AccelerationY { get; private set; }

        /// <summary>
        /// Z ���p�����x
        /// </summary>
        public double AccelerationZ { get; private set; }
    }
}