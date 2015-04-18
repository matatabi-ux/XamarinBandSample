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
    /// iOS �p Band �f�o�C�X���
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
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">Band �N���C�A���g</param>
        public BandDevice(BandClient client)
        {
            this.name = (string)client.Name;
        }
    }
}