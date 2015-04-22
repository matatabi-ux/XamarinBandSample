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
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Sensors;
using global::Microsoft.Band.Tiles;
using Microsoft.Practices.Unity;
using Native = android::Microsoft.Band;
using XamarinBandSample.Droid.Band.Sensors;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p Microsoft Band �f�o�C�X���̃C���^�[�t�F�[�X
    /// </summary>
    public class NativeBandClient : IBandClient
    {
        /// <summary>
        /// �ڑ��N���C�A���g
        /// </summary>
        private Native.IBandClient client = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public NativeBandClient(Native.IBandClient client)
        {
            this.client = client;

            App.Container.RegisterInstance<IBandSensorManager>(new NativeBandSensorManager(this.client), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// �t�@�[���E�F�A�o�[�W�������擾����
        /// </summary>
        /// <returns>�t�@�[���E�F�A�o�[�W����</returns>
        public Task<string> GetFirmwareVersionAsync()
        {
            return Native.BandClientExtensions.GetFirmwareVersionTaskAsync(this.client);
        }

        /// <summary>
        /// �t�@�[���E�F�A�o�[�W�������擾����
        /// </summary>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>�t�@�[���E�F�A�o�[�W����</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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
            return Native.BandClientExtensions.GetHardwareVersionTaskAsync(this.client);
        }

        /// <summary>
        /// �n�[�h�E�F�A�o�[�W�������擾����
        /// </summary>
        /// <param name="token">���f�g�[�N��</param>
        /// <returns>�n�[�h�E�F�A�o�[�W����</returns>
        [Obsolete("CancellationToken is not supported for Android.")]
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