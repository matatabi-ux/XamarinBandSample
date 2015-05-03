#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Band;
using Microsoft.Band.Notifications;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinBandSample.Band.Personalizations;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.ViewModels
{
    /// <summary>
    /// アプリタイル情報 ViewModel
    /// </summary>
    public class TilesViewModel : BindableBase
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private IBandClient client = null;

        /// <summary>
        /// アプリタイル管理クラス
        /// </summary>
        private IBandTileImageManager manager = null;

        /// <summary>
        /// アプリタイルID
        /// </summary>
        private static readonly Guid TileId = Guid.Parse("e26c7ebb-5f51-4194-8140-1af8c001a8d7");

        /// <summary>
        /// アプリタイル取得コマンド
        /// </summary>
        public ICommand PullCommand { get; private set; }

        /// <summary>
        /// アプリタイル追加/削除コマンド
        /// </summary>
        public ICommand ToggleCommand { get; private set; }

        /// <summary>
        /// メッセージ送信コマンド
        /// </summary>
        public ICommand SendMessageCommand { get; private set; }

        /// <summary>
        /// ダイアログ表示コマンド
        /// </summary>
        public ICommand ShowDialogCommand { get; private set; }

        /// <summary>
        /// 振動コマンド
        /// </summary>
        public ICommand VibrationCommand { get; private set; }

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        private bool isBusy = false;

        /// <summary>
        /// 処理中フラグ
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set
            {
                this.SetProperty<bool>(ref this.isBusy, value);
                this.OnPropertyChanged("IsEnableTileManage");
            }
        }

        /// <summary>
        /// アプリタイル登録フラグ
        /// </summary>
        private bool existsTile = false;

        /// <summary>
        /// アプリタイル登録フラグ
        /// </summary>
        public bool ExistsTile
        {
            get { return this.existsTile; }
            set
            {
                this.SetProperty<bool>(ref this.existsTile, value);
                this.OnPropertyChanged("IsEnableTileManage");
            }
        }

        /// <summary>
        /// タイル変更可能フラグ
        /// </summary>
        public bool IsEnableTileManage
        {
            get { return !this.IsBusy && this.ExistsTile; }
        }

        /// <summary>
        /// タイルアイコン画像ソース
        /// </summary>
        private ImageSource icon = null;

        /// <summary>
        /// タイルアイコン画像ソース
        /// </summary>
        public ImageSource Icon
        {
            get { return this.icon; }
            set { this.SetProperty<ImageSource>(ref this.icon, value); }
        }

        /// <summary>
        /// タイル名称
        /// </summary>
        private string tileName = string.Empty;

        /// <summary>
        /// タイル名称
        /// </summary>
        public string TileName
        {
            get { return this.tileName; }
            set { this.SetProperty<string>(ref this.tileName, value); }
        }

        /// <summary>
        /// 基本色
        /// </summary>
        private ColorViewModel baseColor = new ColorViewModel();

        /// <summary>
        /// 基本色
        /// </summary>
        public ColorViewModel BaseColor
        {
            get { return this.baseColor; }
            set { this.SetProperty<ColorViewModel>(ref this.baseColor, value); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        [InjectionConstructor]
        public TilesViewModel(IBandClient client, IBandTileImageManager manager)
        {
            this.IsBusy = true;
            this.client = client;
            this.manager = manager;

            this.PullCommand = DelegateCommand.FromAsyncHandler(this.Pull);
            this.ToggleCommand = DelegateCommand.FromAsyncHandler(this.Toggle);
            this.SendMessageCommand = DelegateCommand.FromAsyncHandler(this.SendMessage);
            this.ShowDialogCommand = DelegateCommand.FromAsyncHandler(this.ShowDialog);
            this.VibrationCommand = DelegateCommand.FromAsyncHandler(this.Vibration);

            this.IsBusy = false;
        }

        /// <summary>
        /// アプリタイル情報を取得する
        /// </summary>
        /// <returns>Task</returns>
        public async Task Pull()
        {
            this.IsBusy = true;
            var tiles = await this.manager.GetTilesAsync();
            if (tiles == null || !tiles.Any())
            {
                this.ExistsTile = false;
                this.IsBusy = false;
                return;
            }
            var tile = tiles.First();

            this.ExistsTile = true;
            this.Icon = tile.TileIconSource;
            this.TileName = tile.Name;

            this.BaseColor.Color = string.Format("#FF{0}{1}{2}",
                tile.Theme.Base.R.ToString("X2"),
                tile.Theme.Base.G.ToString("X2"),
                tile.Theme.Base.B.ToString("X2"));
            this.IsBusy = false;
        }

        /// <summary>
        /// アプリタイルを追加/削除する
        /// </summary>
        /// <returns>Task</returns>
        private async Task Toggle()
        {
            this.IsBusy = true;

            Exception exception = null;

            try
            {
                var tile = (await this.manager.GetTilesAsync()).FirstOrDefault(t => TileId.Equals(t.TileId));
                if (tile != null)
                {
                    await this.manager.RemoveTileAsync(TileId);
                    this.ExistsTile = false;
                    this.IsBusy = false;
                    return;
                }

                var count = await this.manager.GetRemainingTileCapacityAsync();
                if (count < 1)
                {
                    await App.Navigation.CurrentPage.DisplayAlert("Warning", "Tile capacity is not enough.", "OK");
                    return;
                }
                var created = await this.manager.CreateTile(
                    TileId,
                    "matatabi Tile",
                    (StreamImageSource)ImageSource.FromResource(@"XamarinBandSample.Assets.tile-icon.png"),
                    (StreamImageSource)ImageSource.FromResource(@"XamarinBandSample.Assets.small-icon.png"),
                    new BandTheme
                    {
                        Base = new BandColor(0x00, 0x33, 0x99),
                        HighContrast = new BandColor(0x33, 0x66, 0xcc),
                        Highlight = new BandColor(0x33, 0x66, 0xcc),
                        Lowlight = new BandColor(0x00, 0x33, 0x99),
                        Muted = new BandColor(0x00, 0x00, 0x66),
                        SecondaryText = new BandColor(0x99, 0x99, 0x99),
                    });

                await this.manager.AddTileAsync(created);

                this.ExistsTile = true;
                this.Icon = created.TileIconSource;
                this.TileName = created.Name;
                this.BaseColor.Color = string.Format("#FF{0}{1}{2}",
                    created.Theme.Base.R.ToString("X2"),
                    created.Theme.Base.G.ToString("X2"),
                    created.Theme.Base.B.ToString("X2"));
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            if (exception != null)
            {
                await App.Navigation.CurrentPage.DisplayAlert("Error", exception.Message, "OK");
            }

            this.IsBusy = false;
        }

        /// <summary>
        /// メッセージ送信
        /// </summary>
        /// <returns>Task</returns>
        private async Task SendMessage()
        {
            this.IsBusy = true;

            await this.client.NotificationManager.SendMessageAsync(
                TileId, 
                "matatabi", 
                "No cat no life",
                new DateTimeOffset(DateTime.Now),
                MessageFlags.None);

            this.IsBusy = false;
        }

        /// <summary>
        /// ダイアログ表示
        /// </summary>
        /// <returns>Task</returns>
        private async Task ShowDialog()
        {
            this.IsBusy = true;

            await this.client.NotificationManager.ShowDialogAsync(
                TileId, 
                "matatabi", 
                "No cat no life");

            this.IsBusy = false;
        }

        /// <summary>
        /// 振動
        /// </summary>
        /// <returns>Task</returns>
        private async Task Vibration()
        {
            this.IsBusy = true;

            await this.client.NotificationManager.VibrateAsync(VibrationType.NotificationAlarm);

            this.IsBusy = false;
        }
    }
}
