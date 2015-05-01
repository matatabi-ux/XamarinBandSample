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
using Microsoft.Practices.Unity;
using XamarinBandSample.iOS.Band.Personalizations;
using XamarinBandSample.iOS.Band.Sensors;
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

            App.Container.RegisterInstance<IBandSensorManager>(new NativeBandSensorManager(this.client), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandPersonalizationManager>(new NativeBandPersonalizationManager(this.client), new ContainerControlledLifetimeManager());
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
        [Obsolete("CancellationToken is not supported for iOS.")]
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
        [Obsolete("CancellationToken is not supported for iOS.")]
        public Task<string> GetHardwareVersionAsync(CancellationToken token)
        {
            return this.GetHardwareVersionAsync();
        }

        /// <summary>
        /// �v�b�V���ʒm�Ǘ��N���X
        /// </summary>
        public IBandNotificationManager NotificationManager
        {
            get { return App.Container.Resolve<IBandNotificationManager>(); }
        }

        /// <summary>
        /// �J�X�^�}�C�Y�ݒ�Ǘ��N���X
        /// </summary>
        public IBandPersonalizationManager PersonalizationManager
        {
            get { return App.Container.Resolve<IBandPersonalizationManager>(); }
        }

        /// <summary>
        /// �Z���T�[�Ǘ��N���X
        /// </summary>
        public IBandSensorManager SensorManager
        {
            get { return App.Container.Resolve<IBandSensorManager>(); }
        }

        /// <summary>
        /// �^�C���Ǘ��N���X
        /// </summary>
        public IBandTileManager TileManager
        {
            get { return App.Container.Resolve<IBandTileManager>(); }
        }

        /// <summary>
        /// �j������
        /// </summary>
        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}