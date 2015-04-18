using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Microsoft.Band;
using UIKit;
using XamarinBandSample.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 Band デバイス情報
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
        /// コンストラクタ
        /// </summary>
        /// <param name="client">Band クライアント</param>
        public BandDevice(BandClient client)
        {
            this.name = (string)client.Name;
        }
    }
}