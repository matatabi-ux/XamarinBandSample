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
using System.Threading;
using System.Threading.Tasks;

using Foundation;
using global::Microsoft.Band;
using Microsoft.Band.Sensors;
using Microsoft.Practices.Unity;
using Native = ios::Microsoft.Band;
using UIKit;
namespace XamarinBandSample.iOS.Band.Sensors
{
    /// <summary>
    /// iOS 用運動量センサー
    /// </summary>
    public class NativeBandCaloriesSensor : NativeBandSensorBase<IBandCaloriesReading>
    {
        /// <summary>
        /// 運動量センサー
        /// </summary>
        private Native.Sensors.CaloriesSensor sensor = null;

        /// <summary>
        /// センサー値変更イベント
        /// </summary>
        public override event EventHandler<BandSensorReadingEventArgs<IBandCaloriesReading>> ReadingChanged;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="manager">Band センサー管理クラス</param>
        public NativeBandCaloriesSensor(Native.Sensors.IBandSensorManager manager)
            : base(manager)
        {
            this.sensor = Native.Sensors.BandSensorManagerExtensions.CreateCaloriesSensor(manager);
            this.sensor.ReadingChanged += this.OnReadingChanged;
        }

        /// <summary>
        /// センサー値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        protected void OnReadingChanged(object sender, Native.Sensors.BandSensorDataEventArgs<Native.Sensors.BandSensorCaloriesData> e)
        {
            if (this.ReadingChanged == null)
            {
                return;
            }
            this.ReadingChanged.Invoke(
                this, new BandSensorReadingEventArgs<IBandCaloriesReading>(new NativeBandCaloriesReading(e.SensorReading)));
        }

        /// <summary>
        /// センサー検知を開始する
        /// </summary>
        /// <returns>成功した場合は<code>true</code>、それ以外は<code>false</code></returns>
        public override Task<bool> StartReadingsAsync()
        {
            this.sensor.StartReadings();
            return Task.FromResult(true);
        }

        /// <summary>
        /// センサー検知を停止する
        /// </summary>
        /// <returns>Task</returns>
        public override Task StopReadingsAsync()
        {
            return Task.Run(() => this.sensor.StopReadings());
        }
    }
}