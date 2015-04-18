using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Band.Tiles;

namespace XamarinBandSample.Band
{
    /// <summary>
    /// Microsoft Band デバイス接続サービスのインターフェース
    /// </summary>
    public interface IBandService
    {
        /* TODO:後で実装
        IBandNotificationManager NotificationManager { get; }

        IBandPersonalizationManager PersonalizationManager { get; }

        IBandTileManager TileManager { get; }

        IBandSensorManager SensorManager { get; }
        /* */

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <returns>ファームウェアバージョン</returns>
        Task<string> GetFirmwareVersionAsync();

        /// <summary>
        /// ファームウェアバージョンを取得する
        /// </summary>
        /// <remarks>Android, iOS では中断非対応</remarks>
        /// <param name="token">中断トークン</param>
        /// <returns>ファームウェアバージョン</returns>
        Task<string> GetFirmwareVersionAsync(CancellationToken token);

        /// <summary>
        /// ハードウェアバージョンを取得する
        /// </summary>
        /// <returns>ハードウェアバージョン</returns>
        Task<string> GetHardwareVersionAsync();

        /// <summary>
        /// ハードウェアバージョンを取得する
        /// </summary>
        /// <param name="token">中断トークン</param>
        /// <returns>ハードウェアバージョン</returns>
        Task<string> GetHardwareVersionAsync(CancellationToken token);
    }
}
