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
    /// iOS 用運動量データ
    /// </summary>
    public class NativeBandCaloriesReading : IBandCaloriesReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandCaloriesReading(Native.Sensors.BandSensorCaloriesData data)
        {
            this.Timestamp = DateTime.Now;
            this.Calories =  (long)data.Calories;
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }
        
        /// <summary>
        /// 運動量
        /// </summary>
        public long Calories { get; private set; }
    }
}