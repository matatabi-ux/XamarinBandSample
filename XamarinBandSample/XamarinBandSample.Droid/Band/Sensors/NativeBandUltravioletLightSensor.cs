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
    /// Android �p���O���Z���T�[
    /// </summary>
    public class NativeBandUltravioletLightSensor : NativeBandSensorBase<IBandUVReading>
    {
        /// <summary>
        /// ���O���Z���T�[
        /// </summary>
        private Native.Sensors.UVSensor sensor = null;

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandUVReading>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandUltravioletLightSensor(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateUVSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        protected void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandUVEvent> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandUVReading>(new NativeBandUltravioletLightReading(e.SensorReading)));
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        public override async Task<bool> StartReadingsAsync()
        {
            await this.sensor.StartReadingsTaskAsync();
            return true;
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return this.sensor.StopReadingsTaskAsync();
        }
    }
}