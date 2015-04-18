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
using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Droid.Band
{
    /// <summary>
    /// Android �p Microsoft Band �f�o�C�X�Ǘ��N���X
    /// </summary>
    public class BandManager : IBandManager
    {
        /// <summary>
        /// �o�^ Band �f�o�C�X�����擾����
        /// </summary>
        /// <returns>�o�^ Band �f�o�C�X���</returns>
        public Task<IList<IBandDevice>> GetBandsAsync()
        {
            return Task.FromResult<IList<IBandDevice>>((
                from i in BandClientManager.Instance.GetPairedBands()
                select new BandDevice(i) as IBandDevice).ToList());
        }

        /// <summary>
        /// Band �f�o�C�X�ɐڑ�����
        /// </summary>
        /// <param name="device">Band �f�o�C�X���</param>
        /// <returns>Band �ڑ��T�[�r�X</returns>
        public async Task<IBandService> ConnectAsync(IBandDevice device)
        {
            var info = device as BandDevice;
            if (info == null)
            {
                throw new InvalidOperationException("Parameter 'device' is not BandDevice type.");
            }
            var client = BandClientManager.Instance.Create(Application.Context, info.DeviceInfo);

            if (client != null && !client.IsConnected)
            {
               var result = await client.ConnectTaskAsync();
            }

            return new BandService(client);
        }
    }
}