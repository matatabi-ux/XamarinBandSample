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
    /// iOS �p Band �f�o�C�X���
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
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">Band �N���C�A���g</param>
        public NativeBandInfo(Native.BandClient client)
        {
            this.name = (string)client.Name;
        }
    }
}