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
    /// Android �p Band �f�o�C�X���
    /// </summary>
    public class NativeBandInfo : IBandInfo
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
        /// �ڑ�����
        /// </summary>
        public BandConnectionType ConnectionType
        {
            get { return BandConnectionType.Bluetooth; }
        }

        /// <summary>
        /// �f�o�C�X���
        /// </summary>
        private Native.IBandDeviceInfo deviceInfo = null;

        /// <summary>
        /// �f�o�C�X���
        /// </summary>
        public android::Microsoft.Band.IBandDeviceInfo DeviceInfo
        {
            get { return this.deviceInfo; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="info">Band �f�o�C�X���</param>
        public NativeBandInfo(android::Microsoft.Band.IBandDeviceInfo info)
        {
            this.deviceInfo = info;
            this.name = (string)info.Name;
        }
    }
}