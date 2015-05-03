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
using global::Microsoft.Band.Personalization;
using Java.Util;
using Native = android::Microsoft.Band;
using Xamarin.Forms;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.Droid.Band.Tiles
{
    /// <summary>
    /// Android 用アプリタイル
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
        public NativeBandTile(Guid id, string name, Native.Tiles.BandIcon icon, Native.Tiles.BandIcon smallIcon)
        {
            this.tileId = id;

            this.Build(UUID.FromString(id.ToString("D")), name, icon, smallIcon);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        public NativeBandTile(Native.Tiles.BandTile tile)
        {
            this.tile = tile;

            this.tileId = Guid.Parse(tile.TileId.ToString());
            this.tileIconSource = NativeBandImageConvert.FromNative(tile.TileIcon);

            if (tile.TileSmallIcon != null)
            {
                this.smallIconSource = NativeBandImageConvert.FromNative(tile.TileSmallIcon);
            }
        }

        /// <summary>
        /// アプリタイル
        /// </summary>
        private Native.Tiles.BandTile tile = null;

        /// <summary>
        /// アプリタイル
        /// </summary>
        public Native.Tiles.BandTile Tile
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
            get { return this.tile.TileName; }
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
        /// アプリタイルを生成しなおす
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">名称</param>
        /// <param name="icon">アイコン</param>
        /// <param name="smallIcon">小さいアイコン</param>
        /// <param name="theme">テーマカラー</param>
        private void Build(UUID id, string name, Native.Tiles.BandIcon icon, Native.Tiles.BandIcon smallIcon = null,
            Native.Tiles.BandTheme theme = null)
        {
            var builder = new Native.Tiles.BandTile.Builder(id, name, icon);
            if (smallIcon != null)
            {
                builder.SetTileSmallIcon(smallIcon);
            }
            if (theme != null)
            {
                builder.SetTheme(theme);
            }
            this.tile = builder.Build();
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
            var icon = await NativeBandImageConvert.ToNativeIcon(source);
            this.Build(this.tile.TileId, this.tile.TileName, icon, this.tile.TileSmallIcon, this.tile.Theme);

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

            Native.Tiles.BandIcon icon = null;
            if (source != null)
            {
                icon = await NativeBandImageConvert.ToNativeIcon(source);
            }
            this.Build(this.tile.TileId, this.tile.TileName, this.tile.TileIcon, icon, this.tile.Theme);

            return true;
        }

        /// <summary>
        /// テーマカラー
        /// </summary>
        public BandTheme Theme
        {
            get
            {
                if (this.tile.Theme == null)
                {
                    return null;
                }
                return NativeBandThemeConvert.FromNative(this.tile.Theme);
            }

            set
            {
                Native.Tiles.BandTheme theme = null;
                if (value != null)
                {
                    theme = NativeBandThemeConvert.ToNative(value);
                }
                this.Build(this.tile.TileId, this.tile.TileName, this.tile.TileIcon, this.tile.TileSmallIcon, theme);
            }
        }
    }
}