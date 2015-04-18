using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p Microsoft Band �f�o�C�X���̃C���^�[�t�F�[�X
    /// </summary>
    public class BandService : IBandService
    {
        /// <summary>
        /// �ڑ��N���C�A���g
        /// </summary>
        private IBandClient client = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public BandService(IBandClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// �t�@�[���E�F�A�o�[�W�������擾����
        /// </summary>
        /// <returns>�t�@�[���E�F�A�o�[�W����</returns>
        public Task<string> GetFirmwareVersionAsync()
        {
            return this.client.GetFirmwareVersionTaskAsync();
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
        public Task<string> GetHardwareVersionAsync()
        {
            return this.client.GetHardwareVersionTaskAsync();
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
    }
}