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
using System.Linq;
using System.Text;

using Foundation;
using global::Microsoft.Band;
using Native = ios::Microsoft.Band;
using UIKit;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS 用 Band デバイス情報
    /// </summary>
    public class NativeBandInfo : IBandInfo
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
        /// 接続方式
        /// </summary>
        public BandConnectionType ConnectionType
        {
            get { return BandConnectionType.Bluetooth; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">Band クライアント</param>
        public NativeBandInfo(Native.BandClient client)
        {
            this.name = (string)client.Name;
        }
    }
}