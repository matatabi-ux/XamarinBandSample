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
    /// Androrid �p�ړ������f�[�^
    /// </summary>
    public class NativeBandDistanceReading : IBandDistanceReading
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="data">�Z���T�[�f�[�^</param>
        public NativeBandDistanceReading(Native.Sensors.IBandDistanceEvent data)
        {
            this.Timestamp = DateTime.Now;

            this.CurrentMotion = MotionType.Unknown;
            if (data.PedometerMode == Native.Sensors.PedometerMode.Idle)
            {
                this.CurrentMotion = MotionType.Idle;
            }
            if (data.PedometerMode == Native.Sensors.PedometerMode.Walking)
            {
                this.CurrentMotion = MotionType.Walking;
            }
            if (data.PedometerMode == Native.Sensors.PedometerMode.Jogging)
            {
                this.CurrentMotion = MotionType.Jogging;
            }
            if (data.PedometerMode == Native.Sensors.PedometerMode.Running)
            {
                this.CurrentMotion = MotionType.Running;
            }
            this.Pace = (double)data.Pace;
            this.Speed = (double)data.Speed;
            this.TotalDistance = (long)data.TotalDistance;
        }

        /// <summary>
        /// ���o����
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// �ړ����
        /// </summary>
        public MotionType CurrentMotion { get; private set; }

        /// <summary>
        /// �ړ��y�[�X
        /// </summary>
        public double Pace { get; private set; }

        /// <summary>
        /// �ړ����x
        /// </summary>
        public double Speed { get; private set; }

        /// <summary>
        /// �ړ��������v
        /// </summary>
        public long TotalDistance { get; private set; }
    }
}