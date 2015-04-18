using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;
using XamarinBandSample.Band;

namespace XamarinBandSample.Phone.Band
{
    /// <summary>
    /// Windows Phone �p BandClientManager
    /// </summary>
    public class BandManager : IBandManager
    {
        /// <summary>
        /// �o�^ Band �f�o�C�X�����擾����
        /// </summary>
        /// <returns>�o�^ Band �f�o�C�X���</returns>
        public async Task<IList<IBandDevice>> GetBandsAsync()
        {
            return (from i in await BandClientManager.Instance.GetBandsAsync()
                    select new BandDevice(i) as IBandDevice).ToList();
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
                throw  new InvalidOperationException("Parameter 'device' is not BandDevice type.");
            }

            var client = await BandClientManager.Instance.ConnectAsync(info.DeviceInfo);

            return new BandService(client);
        }
    }
}