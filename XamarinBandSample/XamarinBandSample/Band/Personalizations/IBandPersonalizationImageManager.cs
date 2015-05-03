#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2014.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Xamarin.Forms;

namespace XamarinBandSample.Band.Personalizations
{
    /// <summary>
    /// Xamarin.Forms から壁紙を管理できるようにした BandPersonalizationManager のインタフェース
    /// </summary>
    public interface IBandPersonalizationImageManager : IBandPersonalizationManager
    {
        /// <summary>
        /// 接続クライアントを設定する
        /// </summary>
        /// <param name="client">接続クライアント</param>
        void SetClient(IBandClient client);

        /// <summary>
        /// 壁紙の取得
        /// </summary>
        /// <returns>壁紙画像のソース</returns>
        Task<StreamImageSource> GetMeTileImageSourceAsync();

        /// <summary>
        /// 壁紙の設定
        /// </summary>
        /// <param name="source">壁紙画像のソース</param>
        /// <returns>Task</returns>
        Task SetMeTileImageSourceAsync(StreamImageSource source);
    }
}
