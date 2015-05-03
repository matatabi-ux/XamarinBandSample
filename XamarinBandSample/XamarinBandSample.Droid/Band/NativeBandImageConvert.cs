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
    /// Android 用画像コンバーター
    /// </summary>
    public static class NativeBandImageConvert
    {
        /// <summary>
        /// 画像の変換
        /// </summary>
        /// <param name="image">画像情報</param>
        /// <returns>画像ソース</returns>
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
        /// 画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
        public static async Task<Bitmap> ToNative(StreamImageSource source)
        {
            var stream = await source.Stream.Invoke(new CancellationToken());
            return await BitmapFactory.DecodeStreamAsync(stream);
        }

        /// <summary>
        /// アイコン画像の変換
        /// </summary>
        /// <param name="icon">アイコン画像情報</param>
        /// <returns>画像ソース</returns>
        public static StreamImageSource FromNative(Native.Tiles.BandIcon icon)
        {
            return FromNative(icon.Icon);
        }

        /// <summary>
        /// アイコン画像の変換
        /// </summary>
        /// <param name="source">画像ソース</param>
        /// <returns>画像情報</returns>
        public static async Task<Native.Tiles.BandIcon> ToNativeIcon(StreamImageSource source)
        {
            return Native.Tiles.BandIcon.ToBandIcon(await ToNative(source));
        }
    }
}