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
using Xamarin.Forms.Platform.iOS;
using XamarinBandSample.Band.Personalizations;

namespace XamarinBandSample.iOS.Band.Personalizations
{
    /// <summary>
    /// iOS 用着せ替え管理クラス
    /// </summary>
    public class NativeBandPersonalizationManager : IBandPersonalizationImageManager
    {
        /// <summary>
        /// 着せ替え管理クラス
        /// </summary>
        private Native.Personalization.IBandPersonalizationManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NativeBandPersonalizationManager(Native.BandClient client)
        {
            this.manager = client.PersonalizationManager;
        }

        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public void SetClient(IBandClient client)
        {
            // Dummy
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像のソース</returns>
        public async Task<ImageSource> GetMeTileImageSourceAsync()
        {
            var image = await Native.Personalization.BandPersonalizationManagerExtensions.GetMeTileImageTaskAsync(this.manager);
            return ImageSource.FromStream(image.UIImage.AsPNG().AsStream);
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="stream">壁紙画像の入力ストリーム</param>
        /// <returns>Task</returns>
        public async Task SetMeTileImageSourceAsync(Stream stream)
        {
            var image = await Task.Run(() =>
            {
                using (var data = NSData.FromStream(stream))
                {
                    return UIImage.LoadFromData(data);
                }
            });
            await Native.Personalization.BandPersonalizationManagerExtensions.UpdateMeTileImageAsync(
                this.manager,
                new ios::Microsoft.Band.Tiles.BandImage(image));
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>壁紙画像</returns>
        [Obsolete("Not Implemented")]
        public Task<BandImage> GetMeTileImageAsync(CancellationToken cancel)
        {
            return this.GetMeTileImageAsync();
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像</returns>
        [Obsolete("Not Implemented")]
        public Task<BandImage> GetMeTileImageAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// テーマカラーの取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>テーマ情報</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task<BandTheme> GetThemeAsync(CancellationToken cancel)
        {
            return this.GetThemeAsync();
        }

        /// <summary>
        /// テーマカラーの取得
        /// </summary>
        /// <returns>テーマ情報</returns>
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
        /// 配色情報の変換
        /// </summary>
        /// <param name="nativeColor">ネイティブ配色情報</param>
        /// <returns>共通配色情報</returns>
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
        /// 配色情報の変換
        /// </summary>
        /// <param name="color">共通配色情報</param>
        /// <returns>ネイティブ配色情報</returns>
        private static Native.Tiles.BandColor ToNative(BandColor color)
        {
            return Native.Tiles.BandColor.ColorWithRed(color.R, color.G, color.B);
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <param name="cancel">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("Not Implemented")]
        public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel)
        {
            return this.SetMeTileImageAsync(image);
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <returns>Task</returns>
        [Obsolete("Not Implemented")]
        public Task SetMeTileImageAsync(BandImage image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// テーマカラーの設定
        /// </summary>
        /// <param name="theme">テーマ情報</param>
        /// <param name="cancel">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task SetThemeAsync(BandTheme theme, CancellationToken cancel)
        {
            return this.SetThemeAsync(theme);
        }

        /// <summary>
        /// テーマカラーの設定
        /// </summary>
        /// <param name="theme">テーマ情報</param>
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