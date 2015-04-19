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
using System.Linq;
using System.Text;

using global::Microsoft.Band;
using Native = android::Microsoft.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用 Band デバイス情報
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
        /// デバイス情報
        /// </summary>
        private Native.IBandDeviceInfo deviceInfo = null;

        /// <summary>
        /// デバイス情報
        /// </summary>
        public android::Microsoft.Band.IBandDeviceInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info">Band デバイス情報</param>
        public NativeBandInfo(android::Microsoft.Band.IBandDeviceInfo info)
        {
            this.deviceInfo = info;
            this.name = (string)info.Name;
        }
    }
}