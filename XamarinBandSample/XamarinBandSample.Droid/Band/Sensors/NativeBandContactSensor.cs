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
    /// Android �p���p��ԃZ���T�[
    /// </summary>
    public class NativeBandContactSensor : NativeBandSensorBase<IBandContactReading>, IBandContactSensor
    {
        /// <summary>
        /// ���p��ԃZ���T�[
        /// </summary>
        private Native.Sensors.ContactSensor sensor = null;

        /// <summary>
        /// ���݂̒��p���
        /// </summary>
        private IBandContactReading contactReading = null;

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandContactReading>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandContactSensor(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateContactSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        protected void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandContactEvent> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            if (this.ReadingChanged == null)
            {
                return;
            }
            var args = new BandSensorReadingEventArgs<IBandContactReading>(new NativeBandContactReading(e.SensorReading));
            this.contactReading = args.SensorReading;
            this.ReadingChanged.Invoke(
                this, args);
            this.ReadingChanged.Invoke(
                this, args);
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <returns>Task</returns>
        public override Task StartReadingsAsync()
        {
            return this.sensor.StartReadingsTaskAsync();
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return this.sensor.StopReadingsTaskAsync();
        }

        /// <summary>
        /// ���݂̏�Ԃ��擾����
        /// </summary>
        /// <returns>���݂̏��</returns>
        public Task<IBandContactReading> GetCurrentStateAsync()
        {
            return Task.FromResult<IBandContactReading>(this.contactReading);
        }
    }
}