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
    /// iOS �p�Z���T�[�Ǘ��N���X
    /// </summary>
    public class NativeBandSensorManager : IBandSensorManager
    {
        /// <summary>
        /// �Z���T�[�Ǘ��N���X
        /// </summary>
        private Native.Sensors.IBandSensorManager manager = null;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="client">iOS �p�ڑ��N���C�A���g</param>
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
            App.Container.RegisterInstance<IBandSensor<IBandUltravioletLightReading>>(
                new NativeBandUltravioletLightSensor(manager), new ContainerControlledLifetimeManager());
            App.Container.RegisterInstance<IBandSensor<IBandContactReading>>(
                new NativeBandContactSensor(manager), new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// �����x�Z���T�[
        /// </summary>
        public IBandSensor<IBandAccelerometerReading> Accelerometer
        {
            get { return App.Container.Resolve<IBandSensor<IBandAccelerometerReading>>(); }
        }

        /// <summary>
        /// Galvanic ������ (GSR) �Z���T�[
        /// </summary>
        public IBandContactSensor Contact
        {
            get { return App.Container.Resolve<IBandContactSensor>(); }
        }

        /// <summary>
        /// �ړ������Z���T�[
        /// </summary>
        public IBandSensor<IBandDistanceReading> Distance
        {
            get { return App.Container.Resolve<IBandSensor<IBandDistanceReading>>(); }
        }

        /// <summary>
        /// �W���C���Z���T�[
        /// </summary>
        public IBandSensor<IBandGyroscopeReading> Gyroscope
        {
            get { return App.Container.Resolve<IBandSensor<IBandGyroscopeReading>>(); }
        }

        /// <summary>
        /// �S���Z���T�[
        /// </summary>
        public IBandSensor<IBandHeartRateReading> HeartRate
        {
            get { return App.Container.Resolve<IBandSensor<IBandHeartRateReading>>(); }
        }

        /// <summary>
        /// �����Z���T�[
        /// </summary>
        public IBandSensor<IBandPedometerReading> Pedometer
        {
            get { return App.Container.Resolve<IBandSensor<IBandPedometerReading>>(); }
        }

        /// <summary>
        /// �����x�Z���T�[
        /// </summary>
        public IBandSensor<IBandSkinTemperatureReading> SkinTemperature
        {
            get { return App.Container.Resolve<IBandSensor<IBandSkinTemperatureReading>>(); }
        }

        /// <summary>
        /// ���O���Z���T�[
        /// </summary>
        public IBandSensor<IBandUltravioletLightReading> Ultraviolet
        {
            get { return App.Container.Resolve<IBandSensor<IBandUltravioletLightReading>>(); }
        }
    }
}