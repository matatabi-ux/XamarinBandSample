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
using System.Linq;
using System.Text;

using Foundation;
using global::Microsoft.Band;
using Microsoft.Band.Sensors;
using Microsoft.Practices.Unity;
using Native = ios::Microsoft.Band;
using UIKit;

namespace XamarinBandSample.iOS.Band.Sensors
{
    /// <summary>
    /// iOS 用センサー管理クラス
    /// </summary>
    public class NativeBandSensorManager : IBandSensorManager
    {
        /// <summary>
        /// センサー管理クラス
        /// </summary>
        private Native.Sensors.IBandSensorManager manager = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">iOS 用接続クライアント</param>
        public NativeBandSensorManager(Native.BandClient client)
        {
            this.manager = client.SensorManager;

            App.Container.RegisterInstance<IBandSensor<IBandAccelerometerReading>>(
                new NativeBandAcceleromerter(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandGyroscopeReading>>(
                new NativeBandGyroscope(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandHeartRateReading>>(
                new NativeBandHeartRateSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandPedometerReading>>(
                new NativeBandPedometer(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandDistanceReading>>(
                new NativeBandDistanceSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandSkinTemperatureReading>>(
                new NativeBandSkinTemperatureSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandUVReading>>(
                new NativeBandUltravioletLightSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandContactSensor>(
                new NativeBandContactSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandCaloriesReading>>(
                new NativeBandCaloriesSensor(manager), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 加速度センサー
        /// </summary>
        public IBandSensor<IBandAccelerometerReading> Accelerometer
        {
            get { return App.Container.Resolve<IBandSensor<IBandAccelerometerReading>>(); }
        }

        /// <summary>
        /// Galvanic 肌反応 (GSR) センサー
        /// </summary>
        public IBandContactSensor Contact
        {
            get { return App.Container.Resolve<IBandContactSensor>(); }
        }

        /// <summary>
        /// 移動距離センサー
        /// </summary>
        public IBandSensor<IBandDistanceReading> Distance
        {
            get { return App.Container.Resolve<IBandSensor<IBandDistanceReading>>(); }
        }

        /// <summary>
        /// ジャイロセンサー
        /// </summary>
        public IBandSensor<IBandGyroscopeReading> Gyroscope
        {
            get { return App.Container.Resolve<IBandSensor<IBandGyroscopeReading>>(); }
        }

        /// <summary>
        /// 心拍センサー
        /// </summary>
        public IBandSensor<IBandHeartRateReading> HeartRate
        {
            get { return App.Container.Resolve<IBandSensor<IBandHeartRateReading>>(); }
        }

        /// <summary>
        /// 歩数センサー
        /// </summary>
        public IBandSensor<IBandPedometerReading> Pedometer
        {
            get { return App.Container.Resolve<IBandSensor<IBandPedometerReading>>(); }
        }

        /// <summary>
        /// 肌温度センサー
        /// </summary>
        public IBandSensor<IBandSkinTemperatureReading> SkinTemperature
        {
            get { return App.Container.Resolve<IBandSensor<IBandSkinTemperatureReading>>(); }
        }

        /// <summary>
        /// 紫外線センサー
        /// </summary>
        public IBandSensor<IBandUVReading> UV
        {
            get { return App.Container.Resolve<IBandSensor<IBandUVReading>>(); }
        }

        /// <summary>
        /// 運動量センサー
        /// </summary>
        public IBandSensor<IBandCaloriesReading> Calories
        {
            get { return App.Container.Resolve<IBandSensor<IBandCaloriesReading>>(); }
        }
    }
}