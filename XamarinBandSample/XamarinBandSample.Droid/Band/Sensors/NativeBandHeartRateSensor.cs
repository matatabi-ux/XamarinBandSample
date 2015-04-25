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

using global::Microsoft.Band.Sensors;
using Native = android::Microsoft.Band;

namespace XamarinBandSample.Droid.Band.Sensors
{
    /// <summary>
    /// Android �p�S�����Z���T�[
    /// </summary>
    public class NativeBandHeartRateSensor : NativeBandSensorBase<IBandHeartRateReading>
    {
        /// <summary>
        /// �S�����Z���T�[
        /// </summary>
        private Native.Sensors.HeartRateSensor sensor = null;

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandHeartRateReading>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandHeartRateSensor(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateHeartRateSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        private void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandHeartRateEvent> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandHeartRateReading>(new NativeBandHeartRateReading(e.SensorReading)));
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
    }
}