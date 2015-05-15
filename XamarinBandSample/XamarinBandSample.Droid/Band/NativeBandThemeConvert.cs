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
using System.Linq;
using System.Text;

using global::Microsoft.Band;
using Microsoft.Band.Personalization;
using Native = android::Microsoft.Band;
using Android.Graphics;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用テーマカラーコンバーター
    /// </summary>
    public static class NativeBandThemeConvert
    {
        /// <summary>
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">ネイティブ配色情報</param>
        /// <returns>共通配色情報</returns>
        public static BandColor FromNative(Color color)
        {
            return new BandColor(color.R, color.G, color.B);
        }

        /// <summary>
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">共通配色情報</param>
        /// <returns>ネイティブ配色情報</returns>
        public static Color ToNative(BandColor color)
        {
            return new Color(color.R, color.G, color.B);
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
                Highlight = FromNative(theme.HighContrastColor),
                Lowlight = FromNative(theme.LowlightColor),
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
            return new Native.Tiles.BandTheme
            {
                BaseColor = ToNative(theme.Base),
                HighContrastColor = ToNative(theme.HighContrast),
                HighlightColor = ToNative(theme.Highlight),
                LowlightColor = ToNative(theme.Lowlight),
                MutedColor = ToNative(theme.Muted),
                SecondaryTextColor = ToNative(theme.SecondaryText),
            };
        }
    }
}