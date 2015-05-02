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
using XamarinBandSample.Band.Personalizations;
using Xamarin.Forms;

namespace XamarinBandSample.Phone.Band.Personalizations
{
    /// <summary>
    /// Windows 用着せ替え管理クラス
    /// </summary>
    public class NativeBandPersonalizationManager : IBandPersonalizationImageManager
    {
        /// <summary>
        /// 着せ替え管理クラス
        /// </summary>
        private IBandPersonalizationManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NativeBandPersonalizationManager()
        {
        }

        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        public void SetClient(IBandClient client)
        {
            this.manager = client.PersonalizationManager;
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像のソース</returns>
        public async Task<ImageSource> GetMeTileImageSourceAsync()
        {
            var image = await this.GetMeTileImageAsync();
            return ImageSource.FromStream(() => image.ToWriteableBitmap().PixelBuffer.AsStream());
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="stream">壁紙画像の入力ストリーム</param>
        /// <returns>Task</returns>
        public async Task SetMeTileImageSourceAsync(Stream stream)
        {
            using (var ras = stream.AsRandomAccessStream())
            {
                var bitmap = new WriteableBitmap(310, 102);
                await bitmap.SetSourceAsync(ras);

                await this.SetMeTileImageAsync(bitmap.ToBandImage());
            }
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>壁紙画像</returns>
        public Task<BandImage> GetMeTileImageAsync(CancellationToken cancel)
        {
            return this.manager.GetMeTileImageAsync(cancel);
        }

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像</returns>
        public Task<BandImage> GetMeTileImageAsync()
        {
            return this.manager.GetMeTileImageAsync();
        }

        /// <summary>
        /// テーマカラーの取得
        /// </summary>
        /// <param name="cancel">中断トークン</param>
        /// <returns>テーマ情報</returns>
        public Task<BandTheme> GetThemeAsync(CancellationToken cancel)
        {
            return this.manager.GetThemeAsync(cancel);
        }

        /// <summary>
        /// テーマカラーの取得
        /// </summary>
        /// <returns>テーマ情報</returns>
        public Task<BandTheme> GetThemeAsync()
        {
            return this.manager.GetThemeAsync();
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <param name="cancel">中断トークン</param>
        /// <returns>Task</returns>
        public Task SetMeTileImageAsync(BandImage image, CancellationToken cancel)
        {
            return this.manager.SetMeTileImageAsync(image, cancel);
        }

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="image">壁紙画像</param>
        /// <returns>Task</returns>
        public Task SetMeTileImageAsync(BandImage image)
        {
            return this.manager.SetMeTileImageAsync(image);
        }

        /// <summary>
        /// テーマカラーの設定
        /// </summary>
        /// <param name="theme">テーマ情報</param>
        /// <param name="cancel">中断トークン</param>
        /// <returns>Task</returns>
        public Task SetThemeAsync(BandTheme theme, CancellationToken cancel)
        {
            return this.manager.SetThemeAsync(theme, cancel);
        }

        /// <summary>
        /// テーマカラーの設定
        /// </summary>
        /// <param name="theme">テーマ情報</param>
        /// <returns>Task</returns>
        public Task SetThemeAsync(BandTheme theme)
        {
            return this.manager.SetThemeAsync(theme);
        }
    }
}
