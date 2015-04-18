using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Phone.Band
{
    /// <summary>
    /// Windows Phone �p Band �f�o�C�X���
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
        private IBandInfo deviceInfo = null;

        /// <summary>
        /// �f�o�C�X���
        /// </summary>
        public IBandInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">Band �N���C�A���g</param>
        public BandDevice(IBandInfo info)
        {
            this.deviceInfo = info;
            this.name = info.Name;
        }
    }
}