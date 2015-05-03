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
    /// Android 用着せ替え管理クラス
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
        public NativeBandPersonalizationManager(Native.IBandClient client)
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
        public async Task<StreamImageSource> GetMeTileImageSourceAsync()
        {
            return NativeBandImageConvert.FromNative(
                await Native.Personalization.BandPersonalizationManagerExtensions.GetMeTileImageTaskAsync(this.manager));
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="stream">壁紙画像の入力ストリーム</param>
        /// <returns>Task</returns>
        public async Task SetMeTileImageSourceAsync(StreamImageSource source)
        {
            await Native.Personalization.BandPersonalizationManagerExtensions.SetMeTileImageTaskAsync(
               this.manager,
               await NativeBandImageConvert.ToNative(source));
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>壁紙画像</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task<BandImage> GetMeTileImageAsync(CancellationToken cancel)
        {
            return this.GetMeTileImageAsync();
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像</returns>
        public Task<BandImage> GetMeTileImageAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// テーマカラーの取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>テーマ情報</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return NativeBandThemeConvert.FromNative(
                await Native.Personalization.BandPersonalizationManagerExtensions.GetThemeTaskAsync(this.manager));
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <param name="cancel">中断トークン</param>
        /// <returns>Task</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
        public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel)
        {
            return this.SetMeTileImageAsync(image);
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <returns>Task</returns>
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
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return Native.Personalization.BandPersonalizationManagerExtensions.SetThemeTaskAsync(
                this.manager, NativeBandThemeConvert.ToNative(theme));
        }
    }
}