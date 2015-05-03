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
using Microsoft.Band.Personalization;
using Xamarin.Forms;

namespace XamarinBandSample.Band.Tiles
{
    /// <summary>
    /// アプリタイルのインターフェース
    /// </summary>
    public interface IBandTile
    {
        /// <summary>
        /// ID
        /// </summary>
        Guid TileId { get; }

        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// アイコン
        /// </summary>
        StreamImageSource TileIconSource { get; }

        /// <summary>
        /// アイコンを設定する
        /// </summary>
        /// <param name="source">アイコン画像ソース（46 x 46 px）</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        Task<bool> SetTileIconSource(StreamImageSource source);

        /// <summary>
        /// 小さいアイコン
        /// </summary>
        StreamImageSource SmallIconSource { get; }

        /// <summary>
        /// 小さいアイコンを設定する
        /// </summary>
        /// <param name="source">小さいアイコン画像ソース（24 x 24 px）</param>
        /// <returns>成功した場合 <code>true</code>、それ以外は <code>false</code></returns>
        Task<bool> SetSmallIconSource(StreamImageSource source);

        /// <summary>
        /// テーマカラー
        /// </summary>
        BandTheme Theme { get; set; } 
    }
}
