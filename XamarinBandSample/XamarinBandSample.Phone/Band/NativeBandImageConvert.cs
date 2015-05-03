#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Band.Tiles;
using Xamarin.Forms;

namespace XamarinBandSample.Phone.Band
{
    /// <summary>
    /// Windows 用画像コンバーター
    /// </summary>
    public static class NativeBandImageConvert
    {
        /// <summary>
        /// 画像の変換
        /// </summary>
        /// <param name="image">画像情報</param>
        /// <returns>画像ソース</returns>
        public static StreamImageSource FromNative(BandImage image)
        {
            return (StreamImageSource)ImageSource.FromStream(() => image.ToWriteableBitmap().PixelBuffer.AsStream());
        }

        /// <summary>
        /// 画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
        public static async Task<BandImage> ToNative(StreamImageSource source)
        {
            var stream = await source.Stream.Invoke(new CancellationToken());
            using (var ras = stream.AsRandomAccessStream())
            {
                var bitmap = new WriteableBitmap(310, 102);
                await bitmap.SetSourceAsync(ras);

                return bitmap.ToBandImage();
            }
        }

        /// <summary>
        /// アイコン画像の変換
        /// </summary>
        /// <param name="icon">アイコン画像情報</param>
        /// <returns>画像ソース</returns>
        public static StreamImageSource FromNative(BandIcon icon)
        {
            return (StreamImageSource)ImageSource.FromStream(() => icon.ToWriteableBitmap().PixelBuffer.AsStream());
        }

        /// <summary>
        /// アイコン画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
        public static async Task<BandIcon> ToNativeIcon(StreamImageSource source)
        {
            var stream = await source.Stream.Invoke(new CancellationToken());
            using (var ras = stream.AsRandomAccessStream())
            {
                var bitmap = new WriteableBitmap(1, 1);
                await bitmap.SetSourceAsync(ras);

                return bitmap.ToBandIcon();
            }
        }
    }
}
