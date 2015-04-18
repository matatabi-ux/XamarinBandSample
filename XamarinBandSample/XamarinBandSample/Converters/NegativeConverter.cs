#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinBandSample.Converters
{
    /// <summary>
    /// 値を反転させるコンバーター
    /// </summary>
    public class NegativeConverter : IValueConverter
    {
        /// <summary>
        /// 値を反転した値に変換します
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">引数</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>反転値</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool) value;
            }
            if (value is int)
            {
                return -(int)value;
            }
            if (value is double)
            {
                return -(double)value;
            }
            if (value is long)
            {
                return -(long)value;
            }

            return value;
        }

        /// <summary>
        /// 反転値を元の値に変換します
        /// </summary>
        /// <param name="value">反転値</param>
        /// <param name="targetType">対象の型</param>
        /// <param name="parameter">引数</param>
        /// <param name="culture">カルチャ</param>
        /// <returns>元の値</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }
    }
}
