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
using Xamarin.Forms;

namespace XamarinBandSample.ViewModels
{
    /// <summary>
    /// 配色 ViewModel
    /// </summary>
    public class ColorViewModel : BindableBase
    {
        /// <summary>
        /// 配色名選択肢
        /// </summary>
        public List<string> ColorLabelSelection { get; private set; }

        /// <summary>
        /// 配色選択肢
        /// </summary>
        public List<string> ColorSelection { get; private set; }

        /// <summary>
        /// 配色
        /// </summary>
        private string color = "Transparent";

        /// <summary>
        /// 配色
        /// </summary>
        public string Color
        {
            get { return this.color; }
            set { this.SetProperty<string>(ref this.color, value); }
        }

        /// <summary>
        /// 選択肢連番
        /// </summary>
        private int selecedIndex = 0;

        /// <summary>
        /// 選択肢連番
        /// </summary>
        public int SelecedIndex
        {
            get { return this.selecedIndex; }
            set
            {
                this.SetProperty<int>(ref this.selecedIndex, value);
                if (this.selecedIndex >= 0 && this.selecedIndex < this.ColorSelection.Count)
                {
                    this.Color = this.ColorSelection[this.selecedIndex];
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColorViewModel()
        {
            this.ColorLabelSelection = new List<string>
            {
                "Strong blue",
                "Strong violet",
                "Strong pink",
                "Lime green",
                "Pure orange",
                "Dark gray",
                "Pure cyan",
                "Strong orange",
                "Strong magenta",
                "Strong green",
            };
            this.ColorSelection = new List<string>
            {
                "#FF3366cc",
                "#FF6633cc",
                "#FFcc3366",
                "#FF33cc66",
                "#FFff9900",
                "#FF999999",
                "#FF00ccff",
                "#FFff6600",
                "#FFcc33cc",
                "#FF99cc00",
            };
            this.Color = this.ColorSelection[this.selecedIndex];
        }
    }
}
