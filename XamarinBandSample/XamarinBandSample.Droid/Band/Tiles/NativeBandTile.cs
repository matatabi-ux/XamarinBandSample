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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using global::Microsoft.Band;
using global::Microsoft.Band.Personalization;
using Java.Util;
using Native = android::Microsoft.Band;
using Xamarin.Forms;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.Droid.Band.Tiles
{
    /// <summary>
    /// Android �p�A�v���^�C��
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

            this.Build(UUID.FromString(id.ToString("D")), name, icon, smallIcon);
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="tile">�A�v���^�C��</param>
        public NativeBandTile(Native.Tiles.BandTile tile)
        {
            this.tile = tile;

            this.tileId = Guid.Parse(tile.TileId.ToString());
            this.tileIconSource = NativeBandImageConvert.FromNative(tile.TileIcon);

            if (tile.TileSmallIcon != null)
            {
                this.smallIconSource = NativeBandImageConvert.FromNative(tile.TileSmallIcon);
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
            get { return this.tile.TileName; }
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
        /// �A�v���^�C���𐶐����Ȃ���
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">����</param>
        /// <param name="icon">�A�C�R��</param>
        /// <param name="smallIcon">�������A�C�R��</param>
        /// <param name="theme">�e�[�}�J���[</param>
        private void Build(UUID id, string name, Native.Tiles.BandIcon icon, Native.Tiles.BandIcon smallIcon = null,
            Native.Tiles.BandTheme theme = null)
        {
            var builder = new Native.Tiles.BandTile.Builder(id, name, icon);
            if (smallIcon != null)
            {
                builder.SetTileSmallIcon(smallIcon);
            }
            if (theme != null)
            {
                builder.SetTheme(theme);
            }
            this.tile = builder.Build();
        }

        /// <summary>
        /// �A�C�R����ݒ肷��
        /// </summary>
        /// <param name="source">�A�C�R���摜�\�[�X�i46 x 46 px�j</param>
        /// <returns>���������ꍇ <code>true</code>�A����ȊO�� <code>false</code></returns>
        public async Task<bool> SetTileIconSource(StreamImageSource source)
        {
            this.tileIconSource = source;

            if (source == null)
            {
                return false;
            }
            var icon = await NativeBandImageConvert.ToNativeIcon(source);
            this.Build(this.tile.TileId, this.tile.TileName, icon, this.tile.TileSmallIcon, this.tile.Theme);

            return true;
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

            Native.Tiles.BandIcon icon = null;
            if (source != null)
            {
                icon = await NativeBandImageConvert.ToNativeIcon(source);
            }
            this.Build(this.tile.TileId, this.tile.TileName, this.tile.TileIcon, icon, this.tile.Theme);

            return true;
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
                Native.Tiles.BandTheme theme = null;
                if (value != null)
                {
                    theme = NativeBandThemeConvert.ToNative(value);
                }
                this.Build(this.tile.TileId, this.tile.TileName, this.tile.TileIcon, this.tile.TileSmallIcon, theme);
            }
        }
    }
}