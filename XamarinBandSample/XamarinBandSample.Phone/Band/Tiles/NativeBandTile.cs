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
    /// Windows 用アプリタイル
    /// </summary>
    public class NativeBandTile : IBandTile
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="icon">アイコン（46 x 46 px）</param>
        /// <param name="smallIcon">小さいアイコン（24 x 24 px）</param>
        public NativeBandTile(Guid id, string name, BandIcon icon, BandIcon smallIcon)
        {
            this.tileId = id;

            this.tile = new BandTile(id)
            {
                IsBadgingEnabled = true,
                Name = name,
                TileIcon = icon,
                SmallIcon = smallIcon,
            };
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        public NativeBandTile(BandTile tile)
        {
            this.tile = tile;

            this.tileId = tile.TileId;
            this.tileIconSource = NativeBandImageConvert.FromNative(tile.TileIcon);

            if (tile.SmallIcon != null)
            {
                this.smallIconSource = NativeBandImageConvert.FromNative(tile.SmallIcon);
            }
        }

        /// <summary>
        /// アプリタイル
        /// </summary>
        private BandTile tile = null;

        /// <summary>
        /// アプリタイル
        /// </summary>
        public BandTile Tile
        {
            get { return this.tile; }
        }

        /// <summary>
        /// ID
        /// </summary>
        private Guid tileId = Guid.Empty;

        /// <summary>
        /// ID
        /// </summary>
        public Guid TileId
        {
            get { return this.tileId; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this.tile.Name; }
        }

        /// <summary>
        /// アイコン画像ソース
        /// </summary>
        private StreamImageSource tileIconSource = null;

        /// <summary>
        /// アイコン画像ソース
        /// </summary>
        public StreamImageSource TileIconSource
        {
            get { return this.tileIconSource; }
        }

        /// <summary>
        /// アイコンを設定する
        /// </summary>
        /// <param name="source">アイコン画像ソース（46 x 46 px）</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public async Task<bool> SetTileIconSource(StreamImageSource source)
        {
            this.tileIconSource = source;

            if (source == null)
            {
                return false;
            }

            this.tile.TileIcon = await NativeBandImageConvert.ToNativeIcon(source);
            return true;
        }

        /// <summary>
        /// 小さいアイコン画像ソース
        /// </summary>
        private StreamImageSource smallIconSource = null;

        /// <summary>
        /// 小さいアイコン画像ソース
        /// </summary>
        public StreamImageSource SmallIconSource
        {
            get { return this.smallIconSource; }
        }

        /// <summary>
        /// 小さいアイコンを設定する
        /// </summary>
        /// <param name="source">小さいアイコン画像ソース（24 x 24 px）</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        public async Task<bool> SetSmallIconSource(StreamImageSource source)
        {
            this.smallIconSource = source;

            if (source == null)
            {
                this.tile.SmallIcon = null;
                return true;
            }

            this.tile.SmallIcon = await NativeBandImageConvert.ToNativeIcon(source);
            return true;
        }

        /// <summary>
        /// テーマカラー
        /// </summary>
        public BandTheme Theme
        {
            get { return this.tile.Theme; }
            set { this.tile.Theme = value; }
        }
    }
}