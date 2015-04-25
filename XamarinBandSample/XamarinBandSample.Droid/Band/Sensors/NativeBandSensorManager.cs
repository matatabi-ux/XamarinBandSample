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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using global::Microsoft.Band;
using global::Microsoft.Band.Notifications;
using global::Microsoft.Band.Personalization;
using global::Microsoft.Band.Sensors;
using global::Microsoft.Band.Tiles;
using Microsoft.Practices.Unity;
using Native = android::Microsoft.Band;


namespace XamarinBandSample.Droid.Band.Sensors
{
    /// <summary>
    /// Android 用センサー管理クラス
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
        public NativeBandSensorManager(Native.IBandClient client)
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
            App.Container.RegisterInstance<IBandSensor<IBandUltravioletLightReading>>(
                new NativeBandUltravioletLightSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandContactSensor>(
                new NativeBandContactSensor(manager), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 加速度センサー
        /// </summary>
        public IBandSensor<IBandAccelerometerReading> Accelerometer
        {
            get { return App.Container.Resolve<IBandSensor<IBandAccelerometerReading>>(); }
        }

        /// <summary>
        /// 着用状態センサー
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
        public IBandSensor<IBandUltravioletLightReading> Ultraviolet
        {
            get { return App.Container.Resolve<IBandSensor<IBandUltravioletLightReading>>(); }
        }
    }
}