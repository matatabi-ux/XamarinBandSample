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
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Sensors;
using global::Microsoft.Band.Tiles;
using Native = ios::Microsoft.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS �p Microsoft Band �f�o�C�X���̃C���^�[�t�F�[�X
    /// </summary>
    public class NativeBandClient : IBandClient
    {
        /// <summary>
        /// �ڑ��N���C�A���g
        /// </summary>
        private Native.BandClient client = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public NativeBandClient(Native.BandClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// �t�@�[���E�F�A�o�[�W�������擾����
        /// </summary>
        /// <returns>�t�@�[���E�F�A�o�[�W����</returns>
        public async Task<string> GetFirmwareVersionAsync()
        {
            NSString version = await this.client.GetFirmwareVersionAsyncAsync();
            return version != null ? version.ToString() : string.Empty;
        }

        /// <summary>
        /// �t�@�[���E�F�A�o�[�W�������擾����
        /// </summary>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>�t�@�[���E�F�A�o�[�W����</returns>
        public Task<string> GetFirmwareVersionAsync(CancellationToken token)
        {
            return this.GetFirmwareVersionAsync();
        }

        /// <summary>
        /// �n�[�h�E�F�A�o�[�W�������擾����
        /// </summary>
        /// <returns>�n�[�h�E�F�A�o�[�W����</returns>
        public async Task<string> GetHardwareVersionAsync()
        {
            NSString version = await this.client.GetHardwareVersionAsycAsync();
            return version != null ? version.ToString() : string.Empty;
        }

        /// <summary>
        /// �n�[�h�E�F�A�o�[�W�������擾����
        /// </summary>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>�n�[�h�E�F�A�o�[�W����</returns>
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            return this.GetHardwareVersionAsync();
        }

        //TODO:��Ŏ����\��

        public IBandNotificationManager NotificationManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandPersonalizationManager PersonalizationManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandSensorManager SensorManager
        {
            get { throw new NotImplementedException(); }
        }

        public IBandTileManager TileManager
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
        }
    }
}