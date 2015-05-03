#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using XamarinBandSample.Band.Personalizations;
using XamarinBandSample.Band.Tiles;

namespace XamarinBandSample.ViewModels
{
    /// <summary>
    /// トップ画面の ViewModel
    /// </summary>
    public class TopPageViewModel : BindableBase
    {
        /// <summary>
        /// Microsoft Band デバイス管理クラス
        /// </summary>
        private IBandClientManager manager = null;

        #region ShowBasics

        /// <summary>
        /// 基本設定表示フラグ
        /// </summary>
        private bool showBasics = true;

        /// <summary>
        /// 基本設定表示フラグ
        /// </summary>
        public bool ShowBasics
        {
            get { return this.showBasics; }
            set { this.SetProperty(ref this.showBasics, value); }
        }

        #endregion //ShowBasics

        #region ShowSensors

        /// <summary>
        /// センサー情報表示フラグ
        /// </summary>
        private bool showSensors = false;

        /// <summary>
        /// センサー情報表示フラグ
        /// </summary>
        public bool ShowSensors
        {
            get { return this.showSensors; }
            set { this.SetProperty(ref this.showSensors, value); }
        }

        #endregion //ShowSensors

        #region ShowPersonalize

        /// <summary>
        /// 着せ替え情報表示フラグ
        /// </summary>
        private bool showPersonalize = false;

        /// <summary>
        /// 着せ替え情報表示フラグ
        /// </summary>
        public bool ShowPersonalize
        {
            get { return this.showPersonalize; }
            set { this.SetProperty(ref this.showPersonalize, value); }
        }

        #endregion //ShowPersonalize

        #region ShowTiles

        /// <summary>
        /// アプリタイル情報表示フラグ
        /// </summary>
        private bool showTiles = false;

        /// <summary>
        /// アプリタイル情報表示フラグ
        /// </summary>
        public bool ShowTiles
        {
            get { return this.showTiles; }
            set { this.SetProperty(ref this.showTiles, value); }
        }

        #endregion //ShowTiles

        #region IsConnected

        /// <summary>
        /// 接続フラグ
        /// </summary>
        private bool isConnedted = false;

        /// <summary>
        /// 接続フラグ
        /// </summary>
        public bool IsConnected
        {
            get { return this.isConnedted; }
            set { this.SetProperty(ref this.isConnedted, value); }
        }

        #endregion //IsConnected

        #region BandName

        /// <summary>
        /// 接続デバイス名
        /// </summary>
        private string bandName = string.Empty;

        /// <summary>
        /// 接続デバイス名
        /// </summary>
        public string BandName
        {
            get { return this.bandName; }
            set { this.SetProperty(ref this.bandName, value); }
        }

        #endregion //BandName

        #region HardwareVersion

        /// <summary>
        /// ハードウェアバージョン
        /// </summary>
        private string hardwareVersion = string.Empty;

        /// <summary>
        /// ハードウェアバージョン
        /// </summary>
        public string HardwareVersion
        {
            get { return this.hardwareVersion; }
            set { this.SetProperty(ref this.hardwareVersion, value); }
        }

        #endregion //HardwareVersion

        #region FirmwareVersion

        /// <summary>
        /// ファームウェアバージョン
        /// </summary>
        private string firmwareVersion = string.Empty;

        /// <summary>
        /// ファームウェアバージョン
        /// </summary>
        public string FirmwareVersion
        {
            get { return this.firmwareVersion; }
            set { this.SetProperty(ref this.firmwareVersion, value); }
        }

        #endregion //FirmwareVersion

        #region SelectBasicsCommand

        /// <summary>
        /// 基本設定表示選択コマンド
        /// </summary>
        public ICommand SelectBasicsCommand { get; private set; }

        #endregion //SelectBasicsCommand

        #region SelectSensorsCommand

        /// <summary>
        /// センサー情報表示選択コマンド
        /// </summary>
        public ICommand SelectSensorsCommand { get; private set; }

        #endregion //SelectSensorsCommand

        #region SelectPersonalizeCommand

        /// <summary>
        /// 着せ替え情報表示選択コマンド
        /// </summary>
        public ICommand SelectPersonalizeCommand { get; private set; }

        #endregion //SelectPersonalizeCommand

        #region SelectTilesCommand

        /// <summary>
        /// アプリタイル情報表示選択コマンド
        /// </summary>
        public ICommand SelectTilesCommand { get; private set; }

        #endregion //SelectTilesCommand

        #region ConnectCommand

        /// <summary>
        /// 接続コマンド
        /// </summary>
        public ICommand ConnectCommand { get; private set; }

        #endregion //ConnectCommand

        #region ConnectMessage

        /// <summary>
        /// 接続メッセージ
        /// </summary>
        private string connectMessage = string.Empty;

        /// <summary>
        /// 接続メッセージ
        /// </summary>
        public string ConnectMessage
        {
            get { return this.connectMessage; }
            set { this.SetProperty(ref this.connectMessage, value); }
        }

        #endregion //ConnectMessage

        #region SensorReading

        /// <summary>
        /// センサー情報
        /// </summary>
        private SensorReadingViewModel sensorReading = null;

        /// <summary>
        /// センサー情報
        /// </summary>
        public SensorReadingViewModel SensorReading
        {
            get { return this.sensorReading; }
            set { this.SetProperty<SensorReadingViewModel>(ref this.sensorReading, value); }
        }

        #endregion //SensorReading

        #region PersonalizeViewModel

        /// <summary>
        /// 着せ替え情報
        /// </summary>
        private PersonalizeViewModel personalize = null;

        /// <summary>
        /// 着せ替え情報
        /// </summary>
        public PersonalizeViewModel Personalize
        {
            get { return this.personalize; }
            set { this.SetProperty<PersonalizeViewModel>(ref this.personalize, value); }
        }

        #endregion //PersonalizeViewModel

        #region TilesViewModel

        /// <summary>
        /// アプリタイル情報
        /// </summary>
        private TilesViewModel tiles = null;

        /// <summary>
        /// 着アプリタイル情報
        /// </summary>
        public TilesViewModel Tiles
        {
            get { return this.tiles; }
            set { this.SetProperty<TilesViewModel>(ref this.tiles, value); }
        }

        #endregion //TilesViewModel

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Microsoft Band デバイス管理クラス</param>
        [InjectionConstructor]
        public TopPageViewModel(IBandClientManager manager)
        {
            this.manager = manager;
            this.SelectBasicsCommand = new DelegateCommand(this.SelectBasics, () => { return !this.ShowBasics; });
            this.SelectSensorsCommand = DelegateCommand.FromAsyncHandler(this.SelectSensors, () => { return !this.ShowSensors; });
            this.SelectPersonalizeCommand = DelegateCommand.FromAsyncHandler(this.SelectPersonalize, () => { return !this.ShowPersonalize; });
            this.SelectTilesCommand = DelegateCommand.FromAsyncHandler(this.SelectTiles, () => { return !this.ShowTiles; });
            this.ConnectCommand = DelegateCommand.FromAsyncHandler(this.Connect);

            App.Container.RegisterType<SensorReadingViewModel>(new ContainerControlledLifetimeManager());
            App.Container.RegisterType<PersonalizeViewModel>(new ContainerControlledLifetimeManager());
            App.Container.RegisterType<TilesViewModel>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 基本設定表示切替
        /// </summary>
        private void SelectBasics()
        {
            this.ShowSensors = false;
            this.ShowPersonalize = false;
            this.ShowTiles = false;
            this.ShowBasics = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectPersonalizeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectTilesCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// センサー情報表示切替
        /// </summary>
        /// <returns>Task</returns>
        private async Task SelectSensors()
        {
            if (!this.IsConnected)
            {
                await App.Navigation.CurrentPage.DisplayAlert("Warning", "No Microsoft Band connected.", "OK");
                return;
            }
            this.ShowBasics = false;
            this.ShowPersonalize = false;
            this.ShowTiles = false;
            this.ShowSensors = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectPersonalizeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectTilesCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 着せ替え情報表示切替
        /// </summary>
        /// <returns>Task</returns>
        private async Task SelectPersonalize()
        {
            if (!this.IsConnected)
            {
                await App.Navigation.CurrentPage.DisplayAlert("Warning", "No Microsoft Band connected.", "OK");
                return;
            }
            this.ShowBasics = false;
            this.ShowSensors = false;
            this.ShowTiles = false;
            this.ShowPersonalize = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectPersonalizeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectTilesCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// アプリタイル情報表示切替
        /// </summary>
        /// <returns>Task</returns>
        private async Task SelectTiles()
        {
            if (!this.IsConnected)
            {
                await App.Navigation.CurrentPage.DisplayAlert("Warning", "No Microsoft Band connected.", "OK");
                return;
            }
            this.ShowBasics = false;
            this.ShowSensors = false;
            this.ShowPersonalize = false;
            this.ShowTiles = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectPersonalizeCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectTilesCommand).RaiseCanExecuteChanged();

            await this.Tiles.Pull();
        }

        /// <summary>
        /// 接続処理
        /// </summary>
        /// <returns>Task</returns>
        private async Task Connect()
        {
            var devices = await this.manager.GetBandsAsync();
            
            if (!devices.Any())
            {
                await App.Navigation.CurrentPage.DisplayAlert("Warning", "No Microsoft Band found.", "OK");
                return;
            }

            var device = devices.First();
            this.ConnectMessage = "Connecting to Band...";
            var client = await this.manager.ConnectAsync(device);

            if (client == null)
            {
                await App.Navigation.CurrentPage.DisplayAlert(
                    "Error",
                    string.Format("Failed to connect to Microsoft Band '{0}'.", device.Name),
                    "OK");
                return;
            }

            // 別の場所から利用できるように DI コンテナに登録
            App.Container.RegisterInstance<IBandClient>(client, new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandInfo>(device, new ContainerControlledLifetimeManager());
            App.Container.Resolve<IBandPersonalizationImageManager>().SetClient(client);
            App.Container.Resolve<IBandTileImageManager>().SetClient(client);
            this.SensorReading = App.Container.Resolve<SensorReadingViewModel>();
            this.Personalize = App.Container.Resolve<PersonalizeViewModel>();
            this.Tiles = App.Container.Resolve<TilesViewModel>();

            this.ConnectMessage = string.Empty;
            this.BandName = device.Name;
            this.HardwareVersion = await client.GetHardwareVersionAsync();
            this.FirmwareVersion = await client.GetFirmwareVersionAsync();

            this.IsConnected = true;
            await App.Navigation.CurrentPage.DisplayAlert(
                "Connected",
                string.Format("Microsoft Band '{0}' connected.", device.Name),
                "OK");
        }
    }
}
