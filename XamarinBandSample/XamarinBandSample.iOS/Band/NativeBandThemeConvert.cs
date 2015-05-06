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
        public static BandColor FromNative(Native.Personalization.BandColor color)
        {
            nfloat red, green, blue, alpha;
            color.UIColor.GetRGBA(out red, out green, out blue, out alpha);
            return new BandColor((byte)(red * 255f), (byte)(green * 255f), (byte)(blue * 255f));
        }

        /// <summary>
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="color">���ʔz�F���</param>
        /// <returns>�l�C�e�B�u�z�F���</returns>
        public static Native.Personalization.BandColor ToNative(BandColor color)
        {
            return Native.Personalization.BandColor.FromRgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// �e�[�}�J���[�̕ϊ�
        /// </summary>
        /// <param name="nativeColor">�l�C�e�B�u�e�[�}�J���[���</param>
        /// <returns>���ʃe�[�}�J���[���</returns>
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
        /// �e�[�}�J���[�̕ϊ�
        /// </summary>
        /// <param name="color">���ʃe�[�}�J���[���</param>
        /// <returns>�l�C�e�B�u�e�[�}�J���[���</returns>
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