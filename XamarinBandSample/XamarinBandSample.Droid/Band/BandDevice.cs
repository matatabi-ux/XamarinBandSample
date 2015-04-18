using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p Band �f�o�C�X���
    /// </summary>
    public class BandDevice : IBandDevice
    {
        /// <summary>
        /// ����
        /// </summary>
        private string name = null;

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// �f�o�C�X���
        /// </summary>
        private IBandDeviceInfo deviceInfo = null;

        /// <summary>
        /// �f�o�C�X���
        /// </summary>
        public IBandDeviceInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="info">Band �f�o�C�X���</param>
        public BandDevice(IBandDeviceInfo info)
        {
            this.deviceInfo = info;
            this.name = (string)info.Name;
        }
    }
}