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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using Java.Util;
using Native = android::Microsoft.Band;
using Xamarin.Forms;

namespace XamarinBandSample.Droid.Band.Notifications
{
    /// <summary>
    /// Android �p�ʒm�@�\�Ǘ��N���X
    /// </summary>
    public class NativeBandNotificationManager : IBandNotificationManager
    {
        /// <summary>
        /// �ʒm�@�\�Ǘ��N���X
        /// </summary>
        private Native.Notifications.IBandNotificationManager manager = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public NativeBandNotificationManager(Native.IBandClient client)
        {
            this.manager = client.NotificationManager;
        }

        /// <summary>
        /// ���b�Z�[�W��ʒm����
        /// </summary>
        /// <param name="tileId">�A�v���^�C����ID</param>
        /// <param name="title">�^�C�g��</param>
        /// <param name="body">�{��</param>
        /// <param name="timestamp">����</param>
        /// <param name="flags">�t���O</param>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task SendMessageAsync(Guid tileId, string title, string body, DateTimeOffset timestamp,
            MessageFlags flags, CancellationToken token)
        {
            return this.SendMessageAsync(tileId, title, body, timestamp, flags);
        }

        /// <summary>
        /// ���b�Z�[�W��ʒm����
        /// </summary>
        /// <param name="tileId">�A�v���^�C����ID</param>
        /// <param name="title">�^�C�g��</param>
        /// <param name="body">�{��</param>
        /// <param name="timestamp">����</param>
        /// <param name="flags">�t���O</param>
        /// <returns>Task</returns>
        public Task SendMessageAsync(Guid tileId, string title, string body, DateTimeOffset timestamp,
            MessageFlags flags = MessageFlags.None)
        {
            var nativeFlag = flags == MessageFlags.None ? Native.Notifications.MessageFlags.None : Native.Notifications.MessageFlags.ShowDialog;
            var timespan = timestamp.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;

            return Native.Notifications.BandNotificationManagerExtensions.SendMessageTaskAsync(
                this.manager, 
                UUID.FromString(tileId.ToString("D")), title, body, new Date((long)timespan), nativeFlag);
        }

        /// <summary>
        /// �_�C�A���O��\������
        /// </summary>
        /// <param name="tileId">�A�v���^�C����ID</param>
        /// <param name="title">�^�C�g��</param>
        /// <param name="body">�{��</param>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task ShowDialogAsync(Guid tileId, string title, string body, CancellationToken token)
        {
            return this.ShowDialogAsync(tileId, title, body);
        }

        /// <summary>
        /// �_�C�A���O��\������
        /// </summary>
        /// <param name="tileId">�A�v���^�C����ID</param>
        /// <param name="title">�^�C�g��</param>
        /// <param name="body">�{��</param>
        /// <returns>Task</returns>
        public Task ShowDialogAsync(Guid tileId, string title, string body)
        {
            return Native.Notifications.BandNotificationManagerExtensions.ShowDialogTaskAsync(
                this.manager, 
                UUID.FromString(tileId.ToString("D")), title, body);
        }

        /// <summary>
        /// �U��������
        /// </summary>
        /// <param name="vibrationType">�U���^�C�v</param>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task VibrateAsync(VibrationType vibrationType, CancellationToken token)
        {
            return this.VibrateAsync(vibrationType);
        }

        /// <summary>
        /// �U��������
        /// </summary>
        /// <param name="vibrationType">�U���^�C�v</param>
        /// <returns>Task</returns>
        public Task VibrateAsync(VibrationType vibrationType)
        {
            var nativeType = Native.Notifications.VibrationType.RampDown;
            switch (vibrationType)
            {
                case VibrationType.RampDown:
                    nativeType = Native.Notifications.VibrationType.RampDown;
                    break;

                case VibrationType.RampUp:
                    nativeType = Native.Notifications.VibrationType.RampUp;
                    break;

                case VibrationType.NotificationOneTone:
                    nativeType = Native.Notifications.VibrationType.NotificationOneTone;
                    break;

                case VibrationType.NotificationTwoTone:
                    nativeType = Native.Notifications.VibrationType.NotificationTwoTone;
                    break;

                case VibrationType.NotificationAlarm:
                    nativeType = Native.Notifications.VibrationType.NotificationAlarm;
                    break;

                case VibrationType.NotificationTimer:
                    nativeType = Native.Notifications.VibrationType.NotificationTimer;
                    break;

                case VibrationType.OneToneHigh:
                    nativeType = Native.Notifications.VibrationType.OneToneHigh;
                    break;

                case VibrationType.TwoToneHigh:
                    nativeType = Native.Notifications.VibrationType.TwoToneHigh;
                    break;

                case VibrationType.ThreeToneHigh:
                    nativeType = Native.Notifications.VibrationType.ThreeToneHigh;
                    break;

            }
            return Native.Notifications.BandNotificationManagerExtensions.VibrateTaskAsync(this.manager, nativeType);
        }
    }
}