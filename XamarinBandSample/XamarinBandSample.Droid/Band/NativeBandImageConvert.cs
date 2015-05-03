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
using global::Microsoft.Band;
using Microsoft.Band.Personalization;
using Native = android::Microsoft.Band;
using Android.Graphics;
using Xamarin.Forms;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p�摜�R���o�[�^�[
    /// </summary>
    public static class NativeBandImageConvert
    {
        /// <summary>
        /// �摜�̕ϊ�
        /// </summary>
        /// <param name="image">�摜���</param>
        /// <returns>�摜�\�[�X</returns>
        public static StreamImageSource FromNative(Bitmap image)
        {
            return (StreamImageSource)ImageSource.FromStream(() =>
            {
                var stream = new MemoryStream();
                image.Compress(Bitmap.CompressFormat.Png, 100, stream);
                stream.Seek(0L, SeekOrigin.Begin);
                return stream;
            });
        }

        /// <summary>
        /// �摜�̕ϊ�
        /// </summary>
        /// <param name="source">�摜�\�[�X</param>
        /// <returns>�摜���</returns>
        public static async Task<Bitmap> ToNative(StreamImageSource source)
        {
            var stream = await source.Stream.Invoke(new CancellationToken());
            return await BitmapFactory.DecodeStreamAsync(stream);
        }

        /// <summary>
        /// �A�C�R���摜�̕ϊ�
        /// </summary>
        /// <param name="icon">�A�C�R���摜���</param>
        /// <returns>�摜�\�[�X</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandIcon icon)
        {
            return FromNative(icon.Icon);
        }

        /// <summary>
        /// �A�C�R���摜�̕ϊ�
        /// </summary>
        /// <param name="source">�摜�\�[�X</param>
        /// <returns>�摜���</returns>
        public static async Task<Native.Tiles.BandIcon> ToNativeIcon(StreamImageSource source)
        {
            return Native.Tiles.BandIcon.ToBandIcon(await ToNative(source));
        }
    }
}