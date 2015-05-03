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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Foundation;
using global::Microsoft.Band;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Tiles;
using Native = ios::Microsoft.Band;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.iOS.Band.Tiles
{
    /// <summary>
    /// iOS �p�A�v���^�C���Ǘ��N���X
    /// </summary>
    public class NativeBandTileManager : IBandTileImageManager
    {
        /// <summary>
        /// �A�v���^�C���Ǘ��N���X
        /// </summary>
        private Native.Tiles.IBandTileManager manager = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public NativeBandTileManager(Native.BandClient client)
        {
            this.manager = client.TileManager;
        }

        /// <summary>
        /// �ڑ��N���C�A���g��ݒ肷��
        /// </summary>
        /// <param name="client">�ڑ��N���C�A���g</param>
        public void SetClient(IBandClient client)
        {
            // Dummy
        }

        /// <summary>
        /// �A�v���^�C���𐶐�����
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">����</param>
        /// <param name="icon">�A�C�R���̉摜�\�[�X�i46 x 46 px�j</param>
        /// <param name="smallIcon">�������A�C�R���̉摜�\�[�X�i24 x 24 px�j</param>
        /// <param name="theme">�e�[�}�J���[</param>
        /// <returns>�A�v���^�C��</returns>
        public async Task<IBandTile> CreateTile(Guid id, string name, StreamImageSource icon, StreamImageSource smallIcon, BandTheme theme = null)
        {
            var nativeIcon = await NativeBandImageConvert.ToNativeIcon(icon);
            var nativeSmallIcon = await NativeBandImageConvert.ToNativeIcon(smallIcon);
            var tile = new NativeBandTile(id, name, nativeIcon, nativeSmallIcon);
            tile.Theme = theme;

            return tile;
        }

        /// <summary>
        /// �A�v���^�C����ǉ��o�^����
        /// </summary>
        /// <remarks>�e�[�}�J���[���Ȃ����㏑������Ȃ�</remarks>
        /// <param name="tile">�A�v���^�C��</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> AddTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return false;
            }
            await Native.Tiles.BandTileManagerExtensions.AddTileTaskAsync(this.manager, native.Tile);
            return true;
        }

        /// <summary>
        /// �c��̃A�v���g�����擾����
        /// </summary>
        /// <returns>�c��A�v���g��</returns>
        public async Task<int> GetRemainingTileCapacityAsync()
        {
            return (int)await Native.Tiles.BandTileManagerExtensions.RemainingTileCapacityTaskAsync(this.manager);
        }

        /// <summary>
        /// �o�^����Ă���A�v���^�C�����擾����
        /// </summary>
        /// <remarks>�������A�C�R���ƃe�[�}�J���[���Ȃ����擾�ł��Ȃ�</remarks>
        /// <returns>�A�v���^�C���̃R���N�V����</returns>
        public async Task<IEnumerable<IBandTile>> GetTilesAsync()
        {
            var tiles = new List<IBandTile>();
            var nativeTiles = await Native.Tiles.BandTileManagerExtensions.GetTilesTaskAsync(this.manager);

            foreach (var tile in nativeTiles)
            {
                tiles.Add(new NativeBandTile(tile));
            }
            return tiles;
        }

        /// <summary>
        /// �A�v���^�C���̓o�^���폜����
        /// </summary>
        /// <param name="tileId">ID</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> RemoveTileAsync(Guid tileId)
        {
            await Native.Tiles.BandTileManagerExtensions.RemoveTileTaskAsync(
                this.manager,
                new NSUuid(tileId.ToByteArray()));
            return true;
        }

        /// <summary>
        /// �A�v���^�C���̓o�^���폜����
        /// </summary>
        /// <param name="tile">�A�v���^�C��</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> RemoveTileAsync(IBandTile tile)
        {
            var native = tile as NativeBandTile;
            if (native == null)
            {
                return false;
            }
            await Native.Tiles.BandTileManagerExtensions.RemoveTileTaskAsync(
                this.manager,
                native.Tile);
            return true;
        }
    }
}