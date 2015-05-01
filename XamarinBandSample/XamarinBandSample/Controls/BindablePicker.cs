#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinBandSample.Controls
{
    /// <summary>
    /// Binding 可能な Picker
    /// </summary>
    public class BindablePicker : Picker
    {
        /// <summary>
        /// ItemsSource の BindableProperty
        /// </summary>
        public readonly static BindableProperty ItemsSourceProperty = BindableProperty.Create<BindablePicker, IEnumerable>(
            o => o.ItemsSource,
            default(IEnumerable),
            BindingMode.Default,
            null,
            OnItemsSourceChanged);

        /// <summary>
        /// ItemsSource の CLR プロパティ
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource の変更イベントハンドラ
        /// </summary>
        /// <param name="bindable">イベント発行者</param>
        /// <param name="oldValue">古い値</param>
        /// <param name="newValue">新しい値</param>
        private static void OnItemsSourceChanged(BindableObject bindable, IEnumerable oldValue, IEnumerable newValue)
        {
            var picker = bindable as BindablePicker;
            picker.Items.Clear();
            if (newValue == null)
            {
                return;
            }

            foreach (var item in newValue)
            {
                picker.Items.Add(item.ToString());
            }
        }
    }
}
