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
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using global::Microsoft.Band;
using Native = android::Microsoft.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p Microsoft Band �f�o�C�X�Ǘ��N���X
    /// </summary>
    public class NativeBandClientManager : IBandClientManager
    {
        /// <summary>
        /// �o�^ Band �f�o�C�X�����擾����
        /// </summary>
        /// <returns>�o�^ Band �f�o�C�X���</returns>
        public Task<IBandInfo[]> GetBandsAsync()
        {
            return Task.FromResult((
                from i in Native.BandClientManager.Instance.GetPairedBands()
                select new NativeBandInfo(i) as IBandInfo).ToArray());
        }

        /// <summary>
        /// Band �f�o�C�X�ɐڑ�����
        /// </summary>
        /// <param name="device">Band �f�o�C�X���</param>
        /// <returns>Band �ڑ��T�[�r�X</returns>
        public async Task<IBandClient> ConnectAsync(IBandInfo bandInfo)
        {
            var info = bandInfo as NativeBandInfo;
            if (info == null)
            {
                throw new InvalidOperationException("Parameter 'device' is not BandDevice type.");
            }
            var client = Native.BandClientManager.Instance.Create(Application.Context, info.DeviceInfo);

            if (client != null && !client.IsConnected)
            {
                var result = await Native.BandClientExtensions.ConnectTaskAsync(client);
            }

            return new NativeBandClient(client);
        }
    }
}