using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Band;

namespace XamarinBandSample.Band
{
    /// <summary>
    /// Microsoft Band デバイス情報のインターフェース
    /// </summary>
    public interface IBandDevice
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }
    }
}
