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
using XamarinBandSample.Band;
using XamarinBandSample.Band.Tiles;
using BandIcon = ios::Microsoft.Band.Tiles.BandIcon;

namespace XamarinBandSample.iOS.Band.Tiles
{
    /// <summary>
    /// iOS 用アプリタイル
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
            this.tileIconSource = NativeBandImageConvert.FromNative(icon);
            this.smallIconSource = NativeBandImageConvert.FromNative(smallIcon);

            var error = new NSError();
            this.tile = Native.Tiles.BandTile.Create(new NSUuid(id.ToByteArray()), name, icon, smallIcon, out error);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tile">アプリタイル</param>
        public NativeBandTile(Native.Tiles.BandTile tile)
        {
            this.tile = tile;

            this.tileId = new Guid(tile.TileId.GetBytes());
            this.tileIconSource = NativeBandImageConvert.FromNative(tile.TileIcon);

            // Band からタイル情報を取得した場合小さいアイコンがなぜか取れない
            if (tile.SmallIcon != null)
            {
                this.smallIconSource = NativeBandImageConvert.FromNative(tile.SmallIcon);                
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

            var error = new NSError();
            if (source == null)
            {
                return this.tile.SetTileIcon(null, out error);
            }

            return this.tile.SetTileIcon(await NativeBandImageConvert.ToNativeIcon(source), out error);
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

            var error = new NSError();
            if (source == null)
            {
                this.tile.SetSmallIcon(null, out error);
                return true;
            }

            return this.tile.SetSmallIcon(await NativeBandImageConvert.ToNativeIcon(source), out error);
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
                if (value == null)
                {
                    this.tile.Theme = null;
                    return;
                }
                this.tile.Theme = NativeBandThemeConvert.ToNative(value);
            }
        }
    }
}