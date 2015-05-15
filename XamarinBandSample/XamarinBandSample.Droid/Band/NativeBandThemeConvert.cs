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
    /// Android �p�e�[�}�J���[�R���o�[�^�[
    /// </summary>
    public static class NativeBandThemeConvert
    {
        /// <summary>
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="color">�l�C�e�B�u�z�F���</param>
        /// <returns>���ʔz�F���</returns>
        public static BandColor FromNative(Color color)
        {
            return new BandColor(color.R, color.G, color.B);
        }

        /// <summary>
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="color">���ʔz�F���</param>
        /// <returns>�l�C�e�B�u�z�F���</returns>
        public static Color ToNative(BandColor color)
        {
            return new Color(color.R, color.G, color.B);
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
                Highlight = FromNative(theme.HighContrastColor),
                Lowlight = FromNative(theme.LowlightColor),
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