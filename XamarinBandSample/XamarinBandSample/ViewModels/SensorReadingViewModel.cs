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
using Microsoft.Band.Sensors;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;

namespace XamarinBandSample.ViewModels
{
    /// <summary>
    /// センサー情報 ViewModel
    /// </summary>
    public class SensorReadingViewModel : BindableBase
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private IBandClient client = null;

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

        #region ChangeDetectSensorsCommand

        /// <summary>
        /// センサー監視切替コマンド
        /// </summary>
        public ICommand ChangeDetectSensorsCommand { get; private set; }

        #endregion //ChangeDetectSensorsCommand

        #region Acceleromerter

        /// <summary>
        /// X 軸加速度
        /// </summary>
        private double accelerationX = 0d;

        /// <summary>
        /// X 軸加速度
        /// </summary>
        public double AccelerationX
        {
            get { return this.accelerationX; }
            set { this.SetProperty<double>(ref this.accelerationX, value); }
        }

        /// <summary>
        /// Y 軸加速度
        /// </summary>
        private double accelerationY = 0d;

        /// <summary>
        /// Y 軸加速度
        /// </summary>
        public double AccelerationY
        {
            get { return this.accelerationY; }
            set { this.SetProperty<double>(ref this.accelerationY, value); }
        }

        /// <summary>
        /// Z 軸加速度
        /// </summary>
        private double accelerationZ = 0d;

        /// <summary>
        /// Z 軸加速度
        /// </summary>
        public double AccelerationZ
        {
            get { return this.accelerationZ; }
            set { this.SetProperty<double>(ref this.accelerationZ, value); }
        }

        #endregion //Acceleromerter

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        [InjectionConstructor]
        public SensorReadingViewModel(IBandClient client)
        {
            this.client = client;
            this.ChangeDetectSensorsCommand = DelegateCommand<bool>.FromAsyncHandler(this.ChangeDetectSensors);
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
                // 加速度センサーの検知開始
                if (this.client.SensorManager.Accelerometer.IsSupported)
                {
                    await this.client.SensorManager.Accelerometer.StartReadingsAsync();
                    this.client.SensorManager.Accelerometer.ReadingChanged += this.OnAccelerometerReadingChanged;
                }
            }
            else
            {
                // 加速度センサーの検知開始
                if (this.client.SensorManager.Accelerometer.IsSupported)
                {
                    await this.client.SensorManager.Accelerometer.StopReadingsAsync();
                    this.AccelerationX = 0d;
                    this.AccelerationY = 0d;
                    this.AccelerationZ = 0d;
                }
            }
        }

        /// <summary>
        /// 加速度変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnAccelerometerReadingChanged(object sender, BandSensorReadingEventArgs<IBandAccelerometerReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.AccelerationX = e.SensorReading.AccelerationX;
            this.AccelerationY = e.SensorReading.AccelerationY;
            this.AccelerationZ = e.SensorReading.AccelerationZ;
        }
    }
}
