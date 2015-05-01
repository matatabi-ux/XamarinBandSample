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
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Practices.Unity;
using Native = ios::Microsoft.Band;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace XamarinBandSample.iOS.Band.Personalizations
{
    /// <summary>
    /// iOS �p�����ւ��Ǘ��N���X
    /// </summary>
    public class NativeBandPersonalizationManager : IBandPersonalizationManager
    {
        /// <summary>
        /// �����ւ��Ǘ��N���X
        /// </summary>
        private Native.Personalization.IBandPersonalizationManager manager = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">iOS �p�ڑ��N���C�A���g</param>
        public NativeBandPersonalizationManager(Native.BandClient client)
        {
            this.manager = client.PersonalizationManager;
        }

        /// <summary>
        /// �ǎ��̎擾
        /// </summary>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>�ǎ��摜</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task<BandImage> GetMeTileImageAsync(CancellationToken cancel)
        {
            return this.GetMeTileImageAsync();
        }

        /// <summary>
        /// �ǎ��̎擾
        /// </summary>
        /// <returns>�ǎ��摜</returns>
        public Task<BandImage> GetMeTileImageAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// �e�[�}�J���[�̎擾
        /// </summary>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>�e�[�}���</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task<BandTheme> GetThemeAsync(CancellationToken cancel)
        {
            return this.GetThemeAsync();
        }

        /// <summary>
        /// �e�[�}�J���[�̎擾
        /// </summary>
        /// <returns>�e�[�}���</returns>
        public async Task<BandTheme> GetThemeAsync()
        {
            var theme = await Native.Personalization.BandPersonalizationManagerExtensions.GetThemeTaskAsync(this.manager);
            return new BandTheme()
            {
                Base = ToCommon(theme.BaseColor),
                HighContrast = ToCommon(theme.HighContrastColor),
                Highlight = ToCommon(theme.HighLightColor),
                Lowlight = ToCommon(theme.LowLightColor),
                Muted = ToCommon(theme.MutedColor),
                SecondaryText = ToCommon(theme.SecondaryTextColor),
            };
        }

        /// <summary>
        /// �z�F���̕ϊ�
        /// </summary>
        /// <param name="nativeColor">�l�C�e�B�u�z�F���</param>
        /// <returns>���ʔz�F���</returns>
        private static BandColor ToCommon(Native.Tiles.BandColor nativeColor)
        {
            nfloat red, green, blue, alpha;
            nativeColor.UIColor.GetRGBA(out red, out green, out blue, out alpha);
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
        private static Native.Tiles.BandColor ToNative(BandColor color)
        {
            return Native.Tiles.BandColor.ColorWithRed(color.R, color.G, color.B);
        }

        /// <summary>
        /// �ǎ��̐ݒ�
        /// </summary>
        /// <param name="image">�ǎ��摜</param>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel)
        {
            return this.SetMeTileImageAsync(image);
        }

        /// <summary>
        /// �ǎ��̐ݒ�
        /// </summary>
        /// <param name="image">�ǎ��摜</param>
        /// <returns>Task</returns>
        public Task SetMeTileImageAsync(BandImage image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// �e�[�}�J���[�̐ݒ�
        /// </summary>
        /// <param name="theme">�e�[�}���</param>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task SetThemeAsync(BandTheme theme, CancellationToken cancel)
        {
            return this.SetThemeAsync(theme);
        }

        /// <summary>
        /// �e�[�}�J���[�̐ݒ�
        /// </summary>
        /// <param name="theme">�e�[�}���</param>
        /// <returns>Task</returns>
        public Task SetThemeAsync(BandTheme theme)
        {
            var nativeTheme = new Native.Tiles.BandTheme
            {
                BaseColor = ToNative(theme.Base),
                HighContrastColor = ToNative(theme.HighContrast),
                HighLightColor = ToNative(theme.Highlight),
                LowLightColor = ToNative(theme.Lowlight),
                MutedColor = ToNative(theme.Muted),
                SecondaryTextColor = ToNative(theme.SecondaryText),
            };
            return Native.Personalization.BandPersonalizationManagerExtensions.UpdateThemeAsync(this.manager, nativeTheme);
        }
    }
}