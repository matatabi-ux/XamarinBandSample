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

        #region Gyroscope

        /// <summary>
        /// X 軸角速度
        /// </summary>
        private double angularVelocityX = 0d;

        /// <summary>
        /// X 軸角速度
        /// </summary>
        public double AngularVelocityX
        {
            get { return this.angularVelocityX; }
            set { this.SetProperty<double>(ref this.angularVelocityX, value); }
        }

        /// <summary>
        /// Y 軸角速度
        /// </summary>
        private double angularVelocityY = 0d;

        /// <summary>
        /// Y 軸角速度
        /// </summary>
        public double AngularVelocityY
        {
            get { return this.angularVelocityY; }
            set { this.SetProperty<double>(ref this.angularVelocityY, value); }
        }

        /// <summary>
        /// Z 軸角速度
        /// </summary>
        private double angularVelocityZ = 0d;

        /// <summary>
        /// Z 軸角速度
        /// </summary>
        public double AngularVelocityZ
        {
            get { return this.angularVelocityZ; }
            set { this.SetProperty<double>(ref this.angularVelocityZ, value); }
        }

        /// <summary>
        /// X 軸角加速度
        /// </summary>
        private double gyroAccelerationX = 0d;

        /// <summary>
        /// X 軸角加速度
        /// </summary>
        public double GyroAccelerationX
        {
            get { return this.gyroAccelerationX; }
            set { this.SetProperty<double>(ref this.gyroAccelerationX, value); }
        }

        /// <summary>
        /// Y 軸角加速度
        /// </summary>
        private double gyroAccelerationY = 0d;

        /// <summary>
        /// Y 軸角加速度
        /// </summary>
        public double GyroAccelerationY
        {
            get { return this.gyroAccelerationY; }
            set { this.SetProperty<double>(ref this.gyroAccelerationY, value); }
        }

        /// <summary>
        /// Z 軸角加速度
        /// </summary>
        private double gyroAccelerationZ = 0d;

        /// <summary>
        /// Z 軸角加速度
        /// </summary>
        public double GyroAccelerationZ
        {
            get { return this.gyroAccelerationZ; }
            set { this.SetProperty<double>(ref this.gyroAccelerationZ, value); }
        }

        #endregion //Gyroscope

        #region HeartRateSensor

        /// <summary>
        /// 心拍数
        /// </summary>
        private int heartRate = 0;

        /// <summary>
        /// 心拍数
        /// </summary>
        public int HeartRate
        {
            get { return this.heartRate; }
            set { this.SetProperty<int>(ref this.heartRate, value); }
        }

        /// <summary>
        /// 心拍計測状況
        /// </summary>
        private HeartRateQuality heartRateQuality = HeartRateQuality.Acquiring;

        /// <summary>
        /// 心拍計測状況
        /// </summary>
        public HeartRateQuality HeartRateQuality
        {
            get { return this.heartRateQuality; }
            set { this.SetProperty<HeartRateQuality>(ref this.heartRateQuality, value); }
        }

        #endregion //HeartRateSensor

        #region Pedometer

        /// <summary>
        /// 歩数合計
        /// </summary>
        private long totalSteps = 0;

        /// <summary>
        /// 歩数合計
        /// </summary>
        public long TotalSteps
        {
            get { return this.totalSteps; }
            set { this.SetProperty<long>(ref this.totalSteps, value); }
        }

        #endregion //Pedometer

        #region DistanceSensor

        /// <summary>
        /// 移動状態
        /// </summary>
        private MotionType currentMotion = MotionType.Unknown;

        /// <summary>
        /// 移動状態
        /// </summary>
        public MotionType CurrentMotion
        {
            get { return this.currentMotion; }
            set { this.SetProperty<MotionType>(ref this.currentMotion, value); }
        }

        /// <summary>
        /// 移動ペース
        /// </summary>
        private double pace = 0d;

        /// <summary>
        /// 移動ペース
        /// </summary>
        public double Pace
        {
            get { return this.pace; }
            set { this.SetProperty<double>(ref this.pace, value); }
        }

        /// <summary>
        /// 移動速度
        /// </summary>
        private double speed = 0d;

        /// <summary>
        /// 移動速度
        /// </summary>
        public double Speed
        {
            get { return this.speed; }
            set { this.SetProperty<double>(ref this.speed, value); }
        }

        /// <summary>
        /// 移動距離合計
        /// </summary>
        private long totalDistance = 0L;

        /// <summary>
        /// 移動距離合計
        /// </summary>
        public long TotalDistance
        {
            get { return this.totalDistance; }
            set { this.SetProperty<long>(ref this.totalDistance, value); }
        }

        #endregion //DistanceSensor

        #region SkinTemperatureSensor

        /// <summary>
        /// 肌温度
        /// </summary>
        private double skinTempature = 0;

        /// <summary>
        /// 肌温度
        /// </summary>
        public double SkinTemperature
        {
            get { return this.skinTempature; }
            set { this.SetProperty<double>(ref this.skinTempature, value); }
        }

        #endregion //SkinTemperatureSensor

        #region UVSensor

        /// <summary>
        /// 紫外線レベル
        /// </summary>
        private UltravioletExposureLevel exposureLevel = 0;

        /// <summary>
        /// 紫外線レベル
        /// </summary>
        public UltravioletExposureLevel ExposureLevel
        {
            get { return this.exposureLevel; }
            set { this.SetProperty<UltravioletExposureLevel>(ref this.exposureLevel, value); }
        }

        #endregion //UVSensor

        #region ContactSensor

        /// <summary>
        /// 着用状態
        /// </summary>
        private BandContactState contactState = 0;

        /// <summary>
        /// 着用状態
        /// </summary>
        public BandContactState ContactState
        {
            get { return this.contactState; }
            set { this.SetProperty<BandContactState>(ref this.contactState, value); }
        }

        #endregion //ContactSensor

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
                // ジャイロセンサーの検知開始
                if (this.client.SensorManager.Gyroscope.IsSupported)
                {
                    await this.client.SensorManager.Gyroscope.StartReadingsAsync();
                    this.client.SensorManager.Gyroscope.ReadingChanged += this.OnGyroscopeReadingChanged;
                }
                // 心拍数の検知開始
                if (this.client.SensorManager.HeartRate.IsSupported)
                {
                    await this.client.SensorManager.HeartRate.StartReadingsAsync();
                    this.client.SensorManager.HeartRate.ReadingChanged += this.OnHeartRateReadingChanged;
                }
                // 歩数の検知開始
                if (this.client.SensorManager.Pedometer.IsSupported)
                {
                    await this.client.SensorManager.Pedometer.StartReadingsAsync();
                    this.client.SensorManager.Pedometer.ReadingChanged += this.OnPedometerReadingChanged;
                }
                // 移動距離の検知開始
                if (this.client.SensorManager.Distance.IsSupported)
                {
                    await this.client.SensorManager.Distance.StartReadingsAsync();
                    this.client.SensorManager.Distance.ReadingChanged += this.OnDistanceReadingChanged;
                }
                // 肌温度の検知開始
                if (this.client.SensorManager.SkinTemperature.IsSupported)
                {
                    await this.client.SensorManager.SkinTemperature.StartReadingsAsync();
                    this.client.SensorManager.SkinTemperature.ReadingChanged += this.OnSkinTemperatureReadingChanged;
                }
                // 紫外線レベルの検知開始
                if (this.client.SensorManager.Ultraviolet.IsSupported)
                {
                    await this.client.SensorManager.Ultraviolet.StartReadingsAsync();
                    this.client.SensorManager.Ultraviolet.ReadingChanged += this.OnUltravioletReadingChanged;
                }
                // 着用状態の検知開始
                if (this.client.SensorManager.Contact.IsSupported)
                {
                    await this.client.SensorManager.Contact.StartReadingsAsync();
                    this.client.SensorManager.Contact.ReadingChanged += this.OnContactReadingChanged;
                }
            }
            else
            {
                // 加速度センサーの検知終了
                if (this.client.SensorManager.Accelerometer.IsSupported)
                {
                    await this.client.SensorManager.Accelerometer.StopReadingsAsync();
                    this.client.SensorManager.Accelerometer.ReadingChanged -= this.OnAccelerometerReadingChanged;
                    this.AccelerationX = 0d;
                    this.AccelerationY = 0d;
                    this.AccelerationZ = 0d;
                }
                // ジャイロセンサーの検知終了
                if (this.client.SensorManager.Gyroscope.IsSupported)
                {
                    await this.client.SensorManager.Gyroscope.StopReadingsAsync();
                    this.client.SensorManager.Gyroscope.ReadingChanged -= this.OnGyroscopeReadingChanged;
                    this.AngularVelocityX = 0d;
                    this.AngularVelocityY = 0d;
                    this.AngularVelocityZ = 0d;
                    this.GyroAccelerationX = 0d;
                    this.GyroAccelerationY = 0d;
                    this.GyroAccelerationZ = 0d;
                }
                // 心拍数の検知終了
                if (this.client.SensorManager.HeartRate.IsSupported)
                {
                    await this.client.SensorManager.HeartRate.StopReadingsAsync();
                    this.client.SensorManager.HeartRate.ReadingChanged -= this.OnHeartRateReadingChanged;
                    this.HeartRate = 0;
                    this.HeartRateQuality = HeartRateQuality.Acquiring;
                }
                // 歩数の検知終了
                if (this.client.SensorManager.Pedometer.IsSupported)
                {
                    await this.client.SensorManager.Pedometer.StopReadingsAsync();
                    this.client.SensorManager.Pedometer.ReadingChanged -= this.OnPedometerReadingChanged;
                    this.TotalSteps = 0L;
                }
                // 移動距離の検知終了
                if (this.client.SensorManager.Distance.IsSupported)
                {
                    await this.client.SensorManager.Distance.StopReadingsAsync();
                    this.client.SensorManager.Distance.ReadingChanged -= this.OnDistanceReadingChanged;
                    this.CurrentMotion = MotionType.Unknown;
                    this.Pace = 0d;
                    this.Speed = 0d;
                    this.TotalDistance = 0L;
                }
                // 肌温度の検知開始
                if (this.client.SensorManager.SkinTemperature.IsSupported)
                {
                    await this.client.SensorManager.SkinTemperature.StopReadingsAsync();
                    this.client.SensorManager.SkinTemperature.ReadingChanged -= this.OnSkinTemperatureReadingChanged;
                    this.SkinTemperature = 0d;
                }
                // 紫外線レベルの検知終了
                if (this.client.SensorManager.Ultraviolet.IsSupported)
                {
                    await this.client.SensorManager.Ultraviolet.StopReadingsAsync();
                    this.client.SensorManager.Ultraviolet.ReadingChanged -= this.OnUltravioletReadingChanged;
                    this.ExposureLevel = UltravioletExposureLevel.None;
                }
                // 着用状態の検知終了
                if (this.client.SensorManager.Contact.IsSupported)
                {
                    await this.client.SensorManager.Contact.StopReadingsAsync();
                    this.client.SensorManager.Contact.ReadingChanged -= this.OnContactReadingChanged;
                    this.ContactState = BandContactState.NotWorn;
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

        /// <summary>
        /// 角速度変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnGyroscopeReadingChanged(object sender, BandSensorReadingEventArgs<IBandGyroscopeReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.AngularVelocityX = e.SensorReading.AngularVelocityX;
            this.AngularVelocityY = e.SensorReading.AngularVelocityY;
            this.AngularVelocityZ = e.SensorReading.AngularVelocityZ;
            this.GyroAccelerationX = e.SensorReading.AccelerationX;
            this.GyroAccelerationY = e.SensorReading.AccelerationY;
            this.GyroAccelerationZ = e.SensorReading.AccelerationZ;
        }

        /// <summary>
        /// 心拍数変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnHeartRateReadingChanged(object sender, BandSensorReadingEventArgs<IBandHeartRateReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.HeartRate = e.SensorReading.HeartRate;
            this.HeartRateQuality = e.SensorReading.Quality;
        }

        /// <summary>
        /// 歩数変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnPedometerReadingChanged(object sender, BandSensorReadingEventArgs<IBandPedometerReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.TotalSteps = e.SensorReading.TotalSteps;
        }

        /// <summary>
        /// 移動距離変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnDistanceReadingChanged(object sender, BandSensorReadingEventArgs<IBandDistanceReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.CurrentMotion = e.SensorReading.CurrentMotion;
            this.Pace = e.SensorReading.Pace;
            this.Speed = e.SensorReading.Speed;
            this.TotalDistance = e.SensorReading.TotalDistance;
        }

        /// <summary>
        /// 肌温度変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnSkinTemperatureReadingChanged(object sender, BandSensorReadingEventArgs<IBandSkinTemperatureReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.SkinTemperature = e.SensorReading.Temperature;
        }

        /// <summary>
        /// 紫外線レベル変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnUltravioletReadingChanged(object sender, BandSensorReadingEventArgs<IBandUltravioletLightReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.ExposureLevel = e.SensorReading.ExposureLevel;
        }

        /// <summary>
        /// 着用状態変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnContactReadingChanged(object sender, BandSensorReadingEventArgs<IBandContactReading> e)
        {
            if (e == null)
            {
                return;
            }
            this.ContactState = e.SensorReading.State;
        }
    }
}
