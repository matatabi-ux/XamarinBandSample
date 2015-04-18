using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;

namespace XamarinBandSample.Band
{
    /// <summary>
    /// Microsoft Band デバイス管理クラスのインターフェース
    /// </summary>
    public interface IBandManager
    {
        /// <summary>
        /// 登録 Band デバイス情報を取得する
        /// </summary>
        /// <returns>登録 Band デバイス情報</returns>
        Task<IList<IBandDevice>> GetBandsAsync();

        /// <summary>
        /// Band デバイスに接続する
        /// </summary>
        /// <param name="device">Band デバイス情報</param>
        /// <returns>Band 接続サービス</returns>
        Task<IBandService> ConnectAsync(IBandDevice device);
    }
}
