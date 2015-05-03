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
using XamarinBandSample.Band;
using XamarinBandSample.Band.Tiles;
using BandIcon = ios::Microsoft.Band.Tiles.BandIcon;

namespace XamarinBandSample.iOS.Band.Tiles
{
    /// <summary>
    /// iOS �p�A�v���^�C��
    /// </summary>
    public class NativeBandTile : IBandTile
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">����</param>
        /// <param name="icon">�A�C�R���i46 x 46 px�j</param>
        /// <param name="smallIcon">�������A�C�R���i24 x 24 px�j</param>
        public NativeBandTile(Guid id, string name, Native.Tiles.BandIcon icon, Native.Tiles.BandIcon smallIcon)
        {
            this.tileId = id;
            this.tileIconSource = NativeBandImageConvert.FromNative(icon);
            this.smallIconSource = NativeBandImageConvert.FromNative(smallIcon);

            var error = new NSError();
            this.tile = Native.Tiles.BandTile.Create(new NSUuid(id.ToByteArray()), name, icon, smallIcon, out error);
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="tile">�A�v���^�C��</param>
        public NativeBandTile(Native.Tiles.BandTile tile)
        {
            this.tile = tile;

            this.tileId = new Guid(tile.TileId.GetBytes());
            this.tileIconSource = NativeBandImageConvert.FromNative(tile.TileIcon);

            // Band ����^�C�������擾�����ꍇ�������A�C�R�����Ȃ������Ȃ�
            if (tile.SmallIcon != null)
            {
                this.smallIconSource = NativeBandImageConvert.FromNative(tile.SmallIcon);                
            }
        }

        /// <summary>
        /// �A�v���^�C��
        /// </summary>
        private Native.Tiles.BandTile tile = null;

        /// <summary>
        /// �A�v���^�C��
        /// </summary>
        public Native.Tiles.BandTile Tile
        {
            get { return this.tile; }
        }

        /// <summary>
        /// ID
        /// </summary>
        private Guid tileId = Guid.Empty;

        /// <summary>
        /// ID
        /// </summary>
        public Guid TileId
        {
            get { return this.tileId; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get { return this.tile.Name; }
        }

        /// <summary>
        /// �A�C�R���摜�\�[�X
        /// </summary>
        private StreamImageSource tileIconSource = null;

        /// <summary>
        /// �A�C�R���摜�\�[�X
        /// </summary>
        public StreamImageSource TileIconSource
        {
            get { return this.tileIconSource; }
        }

        /// <summary>
        /// �A�C�R����ݒ肷��
        /// </summary>
        /// <param name="source">�A�C�R���摜�\�[�X�i46 x 46 px�j</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> SetTileIconSource(StreamImageSource source)
        {
            this.tileIconSource = source;

            var error = new NSError();
            if (source == null)
            {
                return this.tile.SetTileIcon(null, out error);
            }

            return this.tile.SetTileIcon(await NativeBandImageConvert.ToNativeIcon(source), out error);
        }

        /// <summary>
        /// �������A�C�R���摜�\�[�X
        /// </summary>
        private StreamImageSource smallIconSource = null;

        /// <summary>
        /// �������A�C�R���摜�\�[�X
        /// </summary>
        public StreamImageSource SmallIconSource
        {
            get { return this.smallIconSource; }
        }

        /// <summary>
        /// �������A�C�R����ݒ肷��
        /// </summary>
        /// <param name="source">�������A�C�R���摜�\�[�X�i24 x 24 px�j</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> SetSmallIconSource(StreamImageSource source)
        {
            this.smallIconSource = source;

            var error = new NSError();
            if (source == null)
            {
                this.tile.SetSmallIcon(null, out error);
                return true;
            }

            return this.tile.SetSmallIcon(await NativeBandImageConvert.ToNativeIcon(source), out error);
        }

        /// <summary>
        /// �e�[�}�J���[
        /// </summary>
        public BandTheme Theme
        {
            get
            {
                if (this.tile.Theme == null)
                {
                    return null;
                }
                return NativeBandThemeConvert.FromNative(this.tile.Theme);
            }

            set
            {
                if (value == null)
                {
                    this.tile.Theme = null;
                    return;
                }
                this.tile.Theme = NativeBandThemeConvert.ToNative(value);
            }
        }
    }
}