using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Phone.Band
{
    /// <summary>
    /// Windows Phone 用 Band デバイス情報
    /// </summary>
    public class BandDevice : IBandDevice
    {
        /// <summary>
        /// 名称
        /// </summary>
        private string name = null;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// デバイス情報
        /// </summary>
        private IBandInfo deviceInfo = null;

        /// <summary>
        /// デバイス情報
        /// </summary>
        public IBandInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">Band クライアント</param>
        public BandDevice(IBandInfo info)
        {
            this.deviceInfo = info;
            this.name = info.Name;
        }
    }
}