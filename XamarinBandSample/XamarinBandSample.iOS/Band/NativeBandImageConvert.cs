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
    /// iOS 用画像コンバーター
    /// </summary>
    public static class NativeBandImageConvert
    {
        /// <summary>
        /// 画像の変換
        /// </summary>
        /// <param name="image">画像情報</param>
        /// <returns>画像ソース</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandImage image)
        {
            return (StreamImageSource)ImageSource.FromStream(image.UIImage.AsPNG().AsStream);
        }

        /// <summary>
        /// 画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
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
        /// アイコン画像の変換
        /// </summary>
        /// <param name="icon">アイコン画像情報</param>
        /// <returns>画像ソース</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandIcon icon)
        {
            return (StreamImageSource)ImageSource.FromStream(icon.UIImage.AsPNG().AsStream);
        }

        /// <summary>
        /// アイコン画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
        public static async Task<Native.Tiles.BandIcon> ToNativeIcon(StreamImageSource source)
        {
            var error = new NSError();
            return Native.Tiles.BandIcon.FromImage(await ToNative(source), out error);
        }
    }
}