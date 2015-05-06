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
    /// iOS �p�Z���T�[���N���X
    /// </summary>
    /// <typeparam name="T">�Z���T�[���N���X</typeparam>
    public abstract class NativeBandSensorBase<T> : IBandSensor<T> where T : IBandSensorReading
    {
        /// <summary>
        /// �Ή��Z���T�[���o�C���^�[�o������
        /// </summary>
        protected static readonly IEnumerable<TimeSpan> NativeSupportedReportingIntervals = new List<TimeSpan>();

        /// <summary>
        /// �Ή��ۃt���O
        /// </summary>
        public virtual bool IsSupported 
        {
            get { return true; }
        }

        /// <summary>
        /// �Z���T�[�l�ύX�C�x���g
        /// </summary>
        public abstract event EventHandler<BandSensorReadingEventArgs<T>> ReadingChanged;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manager">Band �Z���T�[�Ǘ��N���X</param>
        public NativeBandSensorBase(Native.Sensors.IBandSensorManager manager)
        {
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <remarks>���f�͔�Ή�</remarks>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public virtual Task<bool> StartReadingsAsync(CancellationToken token)
        {
            return this.StartReadingsAsync();
        }

        /// <summary>
        /// �Z���T�[���m���J�n����
        /// </summary>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        public abstract Task<bool> StartReadingsAsync();

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <remarks>��Ή�</remarks>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public virtual Task StopReadingsAsync(CancellationToken token)
        {
            return this.StopReadingsAsync();
        }

        /// <summary>
        /// �Z���T�[���m���~����
        /// </summary>
        /// <returns>Task</returns>
        public abstract Task StopReadingsAsync();

        /// <summary>
        /// �Ή��Z���T�[���o�C���^�[�o������
        /// </summary>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual IEnumerable<TimeSpan> SupportedReportingIntervals
        {
            get { return NativeSupportedReportingIntervals; }
        }

        /// <summary>
        /// �Z���T�[���o�C���^�[�o������
        /// </summary>
        /// <remarks>��Ή�</remarks>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual TimeSpan ReportingInterval
        {
            get { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
            set { throw new NotSupportedException("Microsoft Band SDK for iOS not supported get or set 'ReportingInterval' property."); }
        }

        /// <summary>
        /// ���݂̃��[�U�[������Ԃ��擾����
        /// </summary>
        /// <returns>���[�U�[�������</returns>
        public virtual UserConsent GetCurrentUserConsent()
        {
            return UserConsent.Granted;
        }

        /// <summary>
        /// �Z���T�[���p�̃��[�U�[������v������
        /// </summary>
        /// <param name="token">���f�g�[�N��</param>
        /// <remarks>��Ή�</remarks>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        [Obsolete("SupportedReportingIntervals is not supported for iOS.")]
        public virtual Task<bool> RequestUserConsentAsync(CancellationToken token)
        {
            return this.RequestUserConsentAsync();
        }

        /// <summary>
        /// �Z���T�[���p�̃��[�U�[������v������
        /// </summary>
        /// <returns>���������ꍇ��<code>true</code>�A����ȊO��<code>false</code></returns>
        public virtual Task<bool> RequestUserConsentAsync()
        {
            return Task.FromResult(true);
        }
    }
}