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
    /// Android 用通知機能管理クラス
    /// </summary>
    public class NativeBandNotificationManager : IBandNotificationManager
    {
        /// <summary>
        /// 通知機能管理クラス
        /// </summary>
        private Native.Notifications.IBandNotificationManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public NativeBandNotificationManager(Native.IBandClient client)
        {
            this.manager = client.NotificationManager;
        }

        /// <summary>
        /// メッセージを通知する
        /// </summary>
        /// <param name="tileId">アプリタイルのID</param>
        /// <param name="title">タイトル</param>
        /// <param name="body">本文</param>
        /// <param name="timestamp">日時</param>
        /// <param name="flags">フラグ</param>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task SendMessageAsync(Guid tileId, string title, string body, DateTimeOffset timestamp,
            MessageFlags flags, CancellationToken token)
        {
            return this.SendMessageAsync(tileId, title, body, timestamp, flags);
        }

        /// <summary>
        /// メッセージを通知する
        /// </summary>
        /// <param name="tileId">アプリタイルのID</param>
        /// <param name="title">タイトル</param>
        /// <param name="body">本文</param>
        /// <param name="timestamp">日時</param>
        /// <param name="flags">フラグ</param>
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
        /// ダイアログを表示する
        /// </summary>
        /// <param name="tileId">アプリタイルのID</param>
        /// <param name="title">タイトル</param>
        /// <param name="body">本文</param>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task ShowDialogAsync(Guid tileId, string title, string body, CancellationToken token)
        {
            return this.ShowDialogAsync(tileId, title, body);
        }

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        /// <param name="tileId">アプリタイルのID</param>
        /// <param name="title">タイトル</param>
        /// <param name="body">本文</param>
        /// <returns>Task</returns>
        public Task ShowDialogAsync(Guid tileId, string title, string body)
        {
            return Native.Notifications.BandNotificationManagerExtensions.ShowDialogTaskAsync(
                this.manager, 
                UUID.FromString(tileId.ToString("D")), title, body);
        }

        /// <summary>
        /// 振動させる
        /// </summary>
        /// <param name="vibrationType">振動タイプ</param>
        /// <param name="token">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task VibrateAsync(VibrationType vibrationType, CancellationToken token)
        {
            return this.VibrateAsync(vibrationType);
        }

        /// <summary>
        /// 振動させる
        /// </summary>
        /// <param name="vibrationType">振動タイプ</param>
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