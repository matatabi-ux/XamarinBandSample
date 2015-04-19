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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinBandSample.Band;

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
        private IBandManager manager = null;

        /// <summary>
        /// Microsoft Band 接続サービスクラス
        /// </summary>
        private IBandService service = null;

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

        #region IsSensorDetecting

        /// <summary>
        /// センサー監視フラグ
        /// </summary>
        private bool isSensorDetecting = false;

        /// <summary>
        /// センサー監視フラグ
        /// </summary>
        public bool IsSensorDetecting
        {
            get { return this.isSensorDetecting; }
            set { this.SetProperty(ref this.isSensorDetecting, value); }
        }

        #endregion //IsSensorDetecting

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

        #region ChangeDetectSensorsCommand

        /// <summary>
        /// センサー監視切替コマンド
        /// </summary>
        public ICommand ChangeDetectSensorsCommand { get; private set; }

        #endregion //ChangeDetectSensorsCommand

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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Microsoft Band デバイス管理クラス</param>
        [InjectionConstructor]
        public TopPageViewModel(IBandManager manager)
        {
            this.manager = manager;
            this.SelectBasicsCommand = new DelegateCommand(this.SelectBasics, () => { return !this.ShowBasics; });
            this.SelectSensorsCommand = new DelegateCommand(this.SelectSensors, () => { return !this.ShowSensors; });
            this.ConnectCommand = DelegateCommand.FromAsyncHandler(this.Connect);
            this.ChangeDetectSensorsCommand = DelegateCommand<bool>.FromAsyncHandler(this.ChangeDetectSensors);
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
        private void SelectSensors()
        {
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
            this.service = await this.manager.ConnectAsync(device);

            if (this.service == null)
            {
                await App.Navigation.CurrentPage.DisplayAlert(
                    "Error",
                    string.Format("Failed to connect to Microsoft Band '{0}'.", device.Name),
                    "OK");
                return;
            }
            this.ConnectMessage = string.Empty;
            this.BandName = device.Name;
            this.HardwareVersion = await this.service.GetHardwareVersionAsync();
            this.FirmwareVersion = await this.service.GetFirmwareVersionAsync();

            this.IsConnected = true;
            await App.Navigation.CurrentPage.DisplayAlert(
                "Connected",
                string.Format("Microsoft Band '{0}' connected.", device.Name),
                "OK");
        }

        /// <summary>
        /// センサー監視切替
        /// </summary>
        /// <param name="detecting">センサー監視フラグ</param>
        /// <returns>Task</returns>
        private async Task ChangeDetectSensors(bool detecting)
        {
            if (detecting)
            {
                //TODO: センサー監視開始処理
            }
            else
            {
                //TODO: センサー監視終了処理
            }
        }
    }
}
