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
    /// iOS �p�e�[�}�J���[�R���o�[�^�[
    /// </summary>
    public static class NativeBandThemeConvert
    {
        /// <summary>
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="color">�l�C�e�B�u�z�F���</param>
        /// <returns>���ʔz�F���</returns>
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
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="color">���ʔz�F���</param>
        /// <returns>�l�C�e�B�u�z�F���</returns>
        public static Native.Tiles.BandColor ToNative(BandColor color)
        {
            return Native.Tiles.BandColor.ColorWithRed(color.R, color.G, color.B);
        }
        
        /// <summary>
        /// �e�[�}�J���[�̕ϊ�
        /// </summary>
        /// <param name="nativeColor">�l�C�e�B�u�e�[�}�J���[���</param>
        /// <returns>���ʃe�[�}�J���[���</returns>
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
        /// �e�[�}�J���[�̕ϊ�
        /// </summary>
        /// <param name="color">���ʃe�[�}�J���[���</param>
        /// <returns>�l�C�e�B�u�e�[�}�J���[���</returns>
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