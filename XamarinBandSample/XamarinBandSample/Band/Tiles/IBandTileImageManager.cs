#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Band.Tiles;
using Xamarin.Forms;

namespace XamarinBandSample.Band.Tiles
{
    /// <summary>
    /// Xamarin.Forms からタイルを管理できるようにした BandTileManager のインタフェース
    /// </summary>
    public interface IBandTileImageManager
    {
        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        void SetClient(IBandClient client);

        /// <summary>
        /// アプリタイルを生成する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="icon">アイコンの画像ソース（46 x 46 px）</param>
        /// <param name="smallIcon">小さいアイコンの画像ソース（24 x 24 px）</param>
        /// <param name="theme">テーマカラー</param>
        /// <returns>アプリタイル</returns>
        Task<IBandTile> CreateTile(Guid id, string name, StreamImageSource icon = null, StreamImageSource smallIcon = null, BandTheme theme = null);

        /// <summary>
        /// アプリタイルを追加登録する
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        Task<bool> AddTileAsync(IBandTile tile);

        /// <summary>
        /// 残りのアプリ枠数を取得する
        /// </summary>
        /// <returns>残りアプリ枠数</returns>
        Task<int> GetRemainingTileCapacityAsync();

        /// <summary>
        /// 登録されているアプリタイルを取得する
        /// </summary>
        /// <returns>アプリタイルのコレクション</returns>
        Task<IEnumerable<IBandTile>> GetTilesAsync();

        /// <summary>
        /// アプリタイルの登録を削除する
        /// </summary>
        /// <param name="tileId">ID</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        Task<bool> RemoveTileAsync(Guid tileId);

        /// <summary>
        /// アプリタイルの登録を削除する
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        Task<bool> RemoveTileAsync(IBandTile tile);
    }
}
