using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using Microsoft.Band;
using UIKit;
using XamarinBandSample.Band;

namespace XamarinBandSample.iOS.Band
{
    /// <summary>
    /// iOS �p BandClientManager
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
                from c in BandClientManager.Instance.AttachedClients
                select new BandDevice(c) as IBandDevice).ToList());
        }

        /// <summary>
        /// Band �f�o�C�X�ɐڑ�����
        /// </summary>
        /// <param name="device">Band �f�o�C�X���</param>
        /// <returns>Band �ڑ��T�[�r�X</returns>
        public async Task<IBandService> ConnectAsync(IBandDevice device)
        {
            var client = BandClientManager.Instance.AttachedClients.FirstOrDefault(c => c.Name.Equals(device.Name));

            if (client != null && !client.IsDeviceConnected)
            {
                await BandClientManager.Instance.ConnectTaskAsync(client);
            }

            return new BandService(client);
        }
    }
}