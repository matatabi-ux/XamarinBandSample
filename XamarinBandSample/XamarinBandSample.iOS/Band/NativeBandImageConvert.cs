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
using Xamarin.Forms;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS �p�摜�R���o�[�^�[
    /// </summary>
    public static class NativeBandImageConvert
    {
        /// <summary>
        /// �摜�̕ϊ�
        /// </summary>
        /// <param name="image">�摜���</param>
        /// <returns>�摜�\�[�X</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandImage image)
        {
            return (StreamImageSource)ImageSource.FromStream(image.UIImage.AsPNG().AsStream);
        }

        /// <summary>
        /// �摜�̕ϊ�
        /// </summary>
        /// <param name="source">�摜�\�[�X</param>
        /// <returns>�摜���</returns>
        public static async Task<Native.Tiles.BandImage> ToNative(StreamImageSource source)
        {
            var stream = await source.Stream.Invoke(new CancellationToken());
            var image = await Task.Run(() =>
            {
                using (var data = NSData.FromStream(stream))
                {
                    return UIImage.LoadFromData(data);
                }
            });
            return new Native.Tiles.BandImage(image);
        }

        /// <summary>
        /// �A�C�R���摜�̕ϊ�
        /// </summary>
        /// <param name="icon">�A�C�R���摜���</param>
        /// <returns>�摜�\�[�X</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandIcon icon)
        {
            return (StreamImageSource)ImageSource.FromStream(icon.UIImage.AsPNG().AsStream);
        }

        /// <summary>
        /// �A�C�R���摜�̕ϊ�
        /// </summary>
        /// <param name="source">�摜�\�[�X</param>
        /// <returns>�摜���</returns>
        public static async Task<Native.Tiles.BandIcon> ToNativeIcon(StreamImageSource source)
        {
            var error = new NSError();
            return Native.Tiles.BandIcon.FromImage(await ToNative(source), out error);
        }
    }
}