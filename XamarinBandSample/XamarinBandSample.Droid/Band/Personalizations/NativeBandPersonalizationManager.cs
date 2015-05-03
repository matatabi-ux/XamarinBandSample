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
using Native = android::Microsoft.Band;
using Xamarin.Forms;
using XamarinBandSample.Band.Personalizations;
using Color = Android.Graphics.Color;

namespace XamarinBandSample.Droid.Band.Personalizations
{
    /// <summary>
    /// Android �p�����ւ��Ǘ��N���X
    /// </summary>
    public class NativeBandPersonalizationManager : IBandPersonalizationImageManager
    {
        /// <summary>
        /// �����ւ��Ǘ��N���X
        /// </summary>
        private Native.Personalization.IBandPersonalizationManager manager = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public NativeBandPersonalizationManager(Native.IBandClient client)
        {
            this.manager = client.PersonalizationManager;
        }

        /// <summary>
        /// �ڑ��N���C�A���g��ݒ肷��
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public void SetClient(IBandClient client)
        {
            // Dummy
        }

        /// <summary>
        /// �ǎ��̎擾
        /// </summary>
        /// <returns>�ǎ��摜�̃\�[�X</returns>
        public async Task<StreamImageSource> GetMeTileImageSourceAsync()
        {
            return NativeBandImageConvert.FromNative(
                await Native.Personalization.BandPersonalizationManagerExtensions.GetMeTileImageTaskAsync(this.manager));
        }

        /// <summary>
        /// �ǎ��̐ݒ�
        /// </summary>
        /// <param name="stream">�ǎ��摜�̓��̓X�g���[��</param>
        /// <returns>Task</returns>
        public async Task SetMeTileImageSourceAsync(StreamImageSource source)
        {
            await Native.Personalization.BandPersonalizationManagerExtensions.SetMeTileImageTaskAsync(
               this.manager,
               await NativeBandImageConvert.ToNative(source));
        }

        /// <summary>
        /// �ǎ��̎擾
        /// </summary>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>�ǎ��摜</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return NativeBandThemeConvert.FromNative(
                await Native.Personalization.BandPersonalizationManagerExtensions.GetThemeTaskAsync(this.manager));
        }

        /// <summary>
        /// �ǎ��̐ݒ�
        /// </summary>
        /// <param name="image">�ǎ��摜</param>
        /// <param name="cancel">���f�g�[�N��</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return Native.Personalization.BandPersonalizationManagerExtensions.SetThemeTaskAsync(
                this.manager, NativeBandThemeConvert.ToNative(theme));
        }
    }
}