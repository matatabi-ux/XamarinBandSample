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
        public static BandColor FromNative(Native.Personalization.BandColor color)
        {
            nfloat red, green, blue, alpha;
            color.UIColor.GetRGBA(out red, out green, out blue, out alpha);
            return new BandColor((byte)(red * 255f), (byte)(green * 255f), (byte)(blue * 255f));
        }

        /// <summary>
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">共通配色情報</param>
        /// <returns>ネイティブ配色情報</returns>
        public static Native.Personalization.BandColor ToNative(BandColor color)
        {
            return Native.Personalization.BandColor.FromRgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// テーマカラーの変換
        /// </summary>
        /// <param name="nativeColor">ネイティブテーマカラー情報</param>
        /// <returns>共通テーマカラー情報</returns>
        public static BandTheme FromNative(Native.Personalization.BandTheme theme)
        {
            return new BandTheme
            {
                Base = FromNative(theme.Base),
                HighContrast = FromNative(theme.HighContrast),
                Highlight = FromNative(theme.Highlight),
                Lowlight = FromNative(theme.Lowlight),
                Muted = FromNative(theme.Muted),
                SecondaryText = FromNative(theme.SecondaryText),
            };
        }

        /// <summary>
        /// テーマカラーの変換
        /// </summary>
        /// <param name="color">共通テーマカラー情報</param>
        /// <returns>ネイティブテーマカラー情報</returns>
        public static Native.Personalization.BandTheme ToNative(BandTheme theme)
        {
            return new Native.Personalization.BandTheme
            {
                Base = ToNative(theme.Base),
                HighContrast = ToNative(theme.HighContrast),
                Highlight = ToNative(theme.Highlight),
                Lowlight = ToNative(theme.Lowlight),
                Muted = ToNative(theme.Muted),
                SecondaryText = ToNative(theme.SecondaryText),
            };
        }
    }
}