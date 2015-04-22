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
    /// Android �p�����x�Z���T�[
    /// </summary>
    public class NativeBandAcceleromerter : IBandSensor<IBandAccelerometerReading>
    {
        /// <summary>
        /// �����x�Z���T�[
        /// </summary>
        private Native.Sensors.AccelerometerSensor sensor = null;

        /// <summary>
        /// �Ή��Z���T�[���o�C���^�[�o������
        /// </summary>
        private static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>
        {
            TimeSpan.FromMilliseconds(128),
            TimeSpan.FromMilliseconds(32),
            TimeSpan.FromMilliseconds(16),
        };

        /// <summary>
        /// �Ή��ۃt���O
        /// </summary>
        public bool IsSupported
        {
            get { return true; }
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public event EventHandler<BandSensorReadingEventArgs<IBandAccelerometerReading>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandAcceleromerter(Native.Sensors.IBandSensorManager manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateAccelerometerSensor(manager);
            this.ReportingInterval = NativeSupportedReportingIntervals.First();
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g�n���h��
        /// </summary>
        /// <param name="sender">�C�x���g���s��</param>
        /// <param name="e">�C�x���g����</param>
        private void OnReadingChanged(object sender, Native.Sensors.IBandSensorEventEventArgs<Native.Sensors.IBandAccelerometerEvent> e)
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
        /// <remarks>���f�͔�Ή�</remarks>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task StartReadingsAsync(System.Threading.CancellationToken token)
        {
            return this.StartReadingsAsync();
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <returns>Task</returns>
        public Task StartReadingsAsync()
        {
            var sampleRate = Native.Sensors.SampleRate.Ms128;
            switch (this.ReportingInterval.Milliseconds)
            {
                case 16:
                    sampleRate = Native.Sensors.SampleRate.Ms16;
                    break;

                case 32:
                    sampleRate = Native.Sensors.SampleRate.Ms32;
                    break;
            }
            return this.sensor.StartReadingsTaskAsync(sampleRate);
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <remarks>��Ή�</remarks>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task StopReadingsAsync(CancellationToken token)
        {
            return this.StopReadingsAsync();
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <returns>Task</returns>
        public Task StopReadingsAsync()
        {
            return this.sensor.StopReadingsTaskAsync();
        }

        /// <summary>
        /// �Ή��Z���T�[���o�C���^�[�o������
        /// </summary>
        public IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// �Z���T�[���o�C���^�[�o������
        /// </summary>
        /// <remarks>��Ή�</remarks>
        public TimeSpan ReportingInterval { get; set; }
    }
}