#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Band.Tiles;
using Xamarin.Forms;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.Phone.Band.Tiles
{
    /// <summary>
    /// Windows 用アプリタイル管理クラス
    /// </summary>
    public class NativeBandTileManager : IBandTileImageManager
    {
        /// <summary>
        /// アプリタイル管理クラス
        /// </summary>
        private IBandTileManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NativeBandTileManager()
        {
        }

        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public void SetClient(IBandClient client)
        {
            this.manager = client.TileManager;
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
        /// <remarks>テーマカラーが上書きされないかも</remarks>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public Task<bool> AddTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return Task.FromResult(false);
            }
            return this.manager.AddTileAsync(native.Tile);
        }

        /// <summary>
        /// 残りのアプリ枠数を取得する
        /// </summary>
        /// <returns>残りアプリ枠数</returns>
        public Task<int> GetRemainingTileCapacityAsync()
        {
            return this.manager.GetRemainingTileCapacityAsync();
        }

        /// <summary>
        /// 登録されているアプリタイルを取得する
        /// </summary>
        /// <remarks>小さいアイコンとテーマカラーが取得できないかも</remarks>
        /// <returns>アプリタイルのコレクション</returns>
        public async Task<IEnumerable<IBandTile>> GetTilesAsync()
        {
            var tiles = new List<IBandTile>();
            var nativeTiles = await this.manager.GetTilesAsync();
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
        public Task<bool> RemoveTileAsync(Guid tileId)
        {
            return this.manager.RemoveTileAsync(tileId);
        }

        /// <summary>
        /// アプリタイルの登録を削除する
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public Task<bool> RemoveTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return Task.FromResult(false);
            }
            return this.manager.RemoveTileAsync(native.Tile);
        }
    }
}