using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android 用 Band デバイス情報
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
        private IBandDeviceInfo deviceInfo = null;

        /// <summary>
        /// デバイス情報
        /// </summary>
        public IBandDeviceInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="info">Band デバイス情報</param>
        public BandDevice(IBandDeviceInfo info)
        {
            this.deviceInfo = info;
            this.name = (string)info.Name;
        }
    }
}