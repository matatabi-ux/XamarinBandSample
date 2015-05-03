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
using Native = ios::Microsoft.Band;
using UIKit;

namespace XamarinBandSample.Band
{
    /// <summary>
    /// iOS 用テーマカラーコンバーター
    /// </summary>
    public static class NativeBandThemeConvert
    {
        /// <summary>
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">ネイティブ配色情報</param>
        /// <returns>共通配色情報</returns>
        public static BandColor FromNative(Native.Tiles.BandColor color)
        {
            nfloat red, green, blue, alpha;
            color.UIColor.GetRGBA(out red, out green, out blue, out alpha);
            return new BandColor
            {
                R = (byte)(red * 255f),
                G = (byte)(green * 255f),
                B = (byte)(blue * 255f),
            };
        }

        /// <summary>
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">共通配色情報</param>
        /// <returns>ネイティブ配色情報</returns>
        public static Native.Tiles.BandColor ToNative(BandColor color)
        {
            return Native.Tiles.BandColor.ColorWithRed(color.R, color.G, color.B);
        }
        
        /// <summary>
        /// テーマカラーの変換
        /// </summary>
        /// <param name="nativeColor">ネイティブテーマカラー情報</param>
        /// <returns>共通テーマカラー情報</returns>
        public static BandTheme FromNative(Native.Tiles.BandTheme theme)
        {
            return new BandTheme
            {
                Base = FromNative(theme.BaseColor),
                HighContrast = FromNative(theme.HighContrastColor),
                Highlight = FromNative(theme.HighLightColor),
                Lowlight = FromNative(theme.LowLightColor),
                Muted = FromNative(theme.MutedColor),
                SecondaryText = FromNative(theme.SecondaryTextColor),
            };
        }

        /// <summary>
        /// テーマカラーの変換
        /// </summary>
        /// <param name="color">共通テーマカラー情報</param>
        /// <returns>ネイティブテーマカラー情報</returns>
        public static Native.Tiles.BandTheme ToNative(BandTheme theme)
        {
            return new Native.Tiles.BandTheme{
                BaseColor = ToNative(theme.Base),
                HighContrastColor = ToNative(theme.HighContrast),
                HighLightColor = ToNative(theme.Highlight),
                LowLightColor = ToNative(theme.Lowlight),
                MutedColor = ToNative(theme.Muted),
                SecondaryTextColor = ToNative(theme.SecondaryText),
            };
        }
    }
}