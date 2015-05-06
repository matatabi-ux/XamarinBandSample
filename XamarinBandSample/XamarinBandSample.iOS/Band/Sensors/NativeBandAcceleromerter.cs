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
    /// iOS �p�����x�Z���T�[
    /// </summary>
    public class NativeBandAcceleromerter : NativeBandSensorBase<IBandAccelerometerReading>
    {
        /// <summary>
        /// �����x�Z���T�[
        /// </summary>
        private Native.Sensors.AccelerometerSensor sensor = null;

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandAccelerometerReading>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandAcceleromerter(Native.Sensors.IBandSensorManager manager) : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateAccelerometerSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        private void OnReadingChanged(object sender, Native.Sensors.BandSensorDataEventArgs<Native.Sensors.BandSensorAccelerometerData> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandAccelerometerReading>(new NativeBandAccelerometerReading(e.SensorReading)));
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        public override Task<bool> StartReadingsAsync()
        {
            this.sensor.StartReadings();
            return Task.FromResult(true);
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return Task.Run(() => this.sensor.StopReadings());
        }
    }
}