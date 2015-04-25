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
    /// iOS 用着用状態センサー
    /// </summary>
    public class NativeBandContactReading : IBandContactReading
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="data">センサーデータ</param>
        public NativeBandContactReading(Native.Sensors.BandSensorContactData data)
        {
            this.Timestamp = DateTime.Now;
            this.State = BandContactState.NotWorn;
            if (data.WornState == Native.Sensors.BandContactStatus.Worn)
            {
                this.State = BandContactState.Worn;
            }
        }

        /// <summary>
        /// 検出日時
        /// </summary>
        public DateTimeOffset Timestamp { get; private set; }

        /// <summary>
        /// 着用状態
        /// </summary>
        public BandContactState State { get; private set; }
    }

}