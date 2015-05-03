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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Tiles;
using Native = ios::Microsoft.Band;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.iOS.Band.Tiles
{
    /// <summary>
    /// iOS 用アプリタイル管理クラス
    /// </summary>
    public class NativeBandTileManager : IBandTileImageManager
    {
        /// <summary>
        /// アプリタイル管理クラス
        /// </summary>
        private Native.Tiles.IBandTileManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public NativeBandTileManager(Native.BandClient client)
        {
            this.manager = client.TileManager;
        }

        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public void SetClient(IBandClient client)
        {
            // Dummy
        }

        /// <summary>
        /// アプリタイルを生成する
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="icon">アイコンの画像ソース（46 x 46 px）</param>
        /// <param name="smallIcon">小さいアイコンの画像ソース（24 x 24 px）</param>
        /// <param name="theme">テーマカラー</param>
        /// <returns>アプリタイル</returns>
        public async Task<IBandTile> CreateTile(Guid id, string name, StreamImageSource icon, StreamImageSource smallIcon, BandTheme theme = null)
        {
            var nativeIcon = await NativeBandImageConvert.ToNativeIcon(icon);
            var nativeSmallIcon = await NativeBandImageConvert.ToNativeIcon(smallIcon);
            var tile = new NativeBandTile(id, name, nativeIcon, nativeSmallIcon);
            tile.Theme = theme;

            return tile;
        }

        /// <summary>
        /// アプリタイルを追加登録する
        /// </summary>
        /// <remarks>テーマカラーがなぜか上書きされない</remarks>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public async Task<bool> AddTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return false;
            }
            await Native.Tiles.BandTileManagerExtensions.AddTileTaskAsync(this.manager, native.Tile);
            return true;
        }

        /// <summary>
        /// 残りのアプリ枠数を取得する
        /// </summary>
        /// <returns>残りアプリ枠数</returns>
        public async Task<int> GetRemainingTileCapacityAsync()
        {
            return (int)await Native.Tiles.BandTileManagerExtensions.RemainingTileCapacityTaskAsync(this.manager);
        }

        /// <summary>
        /// 登録されているアプリタイルを取得する
        /// </summary>
        /// <remarks>小さいアイコンとテーマカラーがなぜか取得できない</remarks>
        /// <returns>アプリタイルのコレクション</returns>
        public async Task<IEnumerable<IBandTile>> GetTilesAsync()
        {
            var tiles = new List<IBandTile>();
            var nativeTiles = await Native.Tiles.BandTileManagerExtensions.GetTilesTaskAsync(this.manager);

            foreach (var tile in nativeTiles)
            {
                tiles.Add(new NativeBandTile(tile));
            }
            return tiles;
        }

        /// <summary>
        /// アプリタイルの登録を削除する
        /// </summary>
        /// <param name="tileId">ID</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public async Task<bool> RemoveTileAsync(Guid tileId)
        {
            await Native.Tiles.BandTileManagerExtensions.RemoveTileTaskAsync(
                this.manager,
                new NSUuid(tileId.ToByteArray()));
            return true;
        }

        /// <summary>
        /// アプリタイルの登録を削除する
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public async Task<bool> RemoveTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return false;
            }
            await Native.Tiles.BandTileManagerExtensions.RemoveTileTaskAsync(
                this.manager,
                native.Tile);
            return true;
        }
    }
}