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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;

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
            this.ConnectCommand = DelegateCommand.FromAsyncHandler(this.Connect);

            App.Container.RegisterType<SensorReadingViewModel>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 基本設定表示切替
        /// </summary>
        private void SelectBasics()
        {
            this.ShowSensors = false;
            this.ShowBasics = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// センサー情報表示切替
        /// </summary>
        private async Task SelectSensors()
        {
            if (!this.IsConnected)
            {
                await App.Navigation.CurrentPage.DisplayAlert("Warning", "No Microsoft Band connected.", "OK");
                return;
            }
            this.ShowBasics = false;
            this.ShowSensors = true;
            ((DelegateCommand)this.SelectBasicsCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)this.SelectSensorsCommand).RaiseCanExecuteChanged();
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
            this.SensorReading = App.Container.Resolve<SensorReadingViewModel>();

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
