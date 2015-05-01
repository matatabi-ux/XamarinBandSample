#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Band;
using Microsoft.Band.Personalization;
using Microsoft.Band.Sensors;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace XamarinBandSample.ViewModels
{
    /// <summary>
    /// 着せ替え設定 ViewModel
    /// </summary>
    public class PersonalizeViewModel : BindableBase
    {
        /// <summary>
        /// 接続クライアント
        /// </summary>
        private IBandClient client = null;

        /// <summary>
        /// 設定状況更新コマンド
        /// </summary>
        public ICommand PullCommand { get; private set; }

        /// <summary>
        /// 設定適用コマンド
        /// </summary>
        public ICommand ApplyCommand { get; private set; }

        /// <summary>
        /// 配色選択肢
        /// </summary>
        public List<string> ColorSelection { get; private set; }

        #region ThemeColors

        /// <summary>
        /// 基本色
        /// </summary>
        private ColorViewModel baseColor = new ColorViewModel();

        /// <summary>
        /// 基本色
        /// </summary>
        public ColorViewModel BaseColor
        {
            get { return this.baseColor; }
            set { this.SetProperty<ColorViewModel>(ref this.baseColor, value); }
        }

        /// <summary>
        /// コントラスト色
        /// </summary>
        private ColorViewModel highContrastColor = new ColorViewModel();

        /// <summary>
        /// コントラスト色
        /// </summary>
        public ColorViewModel HighContrastColor
        {
            get { return this.highContrastColor; }
            set { this.SetProperty<ColorViewModel>(ref this.highContrastColor, value); }
        }

        /// <summary>
        /// ハイライト色
        /// </summary>
        private ColorViewModel highlightColor = new ColorViewModel();

        /// <summary>
        /// ハイライト色
        /// </summary>
        public ColorViewModel HighlightColor
        {
            get { return this.highlightColor; }
            set { this.SetProperty<ColorViewModel>(ref this.highlightColor, value); }
        }

        /// <summary>
        /// ローライト色
        /// </summary>
        private ColorViewModel lowlightColor = new ColorViewModel();

        /// <summary>
        /// ローライト色
        /// </summary>
        public ColorViewModel LowlightColor
        {
            get { return this.lowlightColor; }
            set { this.SetProperty<ColorViewModel>(ref this.lowlightColor, value); }
        }

        /// <summary>
        /// 抑制色
        /// </summary>
        private ColorViewModel mutedColor = new ColorViewModel();

        /// <summary>
        /// 抑制色
        /// </summary>
        public ColorViewModel MutedColor
        {
            get { return this.mutedColor; }
            set { this.SetProperty<ColorViewModel>(ref this.mutedColor, value); }
        }

        /// <summary>
        /// 副文字色
        /// </summary>
        private ColorViewModel secondaryTextColor = new ColorViewModel();

        /// <summary>
        /// 副文字色
        /// </summary>
        public ColorViewModel SecondaryTextColor
        {
            get { return this.secondaryTextColor; }
            set { this.SetProperty<ColorViewModel>(ref this.secondaryTextColor, value); }
        }

        #endregion //ThemeColors

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="client">接続クライアント</param>
        [InjectionConstructor]
        public PersonalizeViewModel(IBandClient client)
        {
            this.client = client;

            this.PullCommand = DelegateCommand.FromAsyncHandler(this.Pull);
            this.ApplyCommand = DelegateCommand.FromAsyncHandler(this.Apply);

            this.BaseColor.SelecedIndex = 1;
            this.HighContrastColor.Color = "#FF9966ff";
            this.HighlightColor.Color = "#FF9966ff";
            this.LowlightColor.Color = "#FF6633cc";
            this.MutedColor.Color = "#FF663399";

            this.SecondaryTextColor.SelecedIndex = 5;

            this.baseColor.PropertyChanged += this.OnBaseColorChanged;
        }

        /// <summary>
        /// 基本色変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行者</param>
        /// <param name="e">イベント引数</param>
        private void OnBaseColorChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("SelecedIndex"))
            {
                return;
            }
            switch (this.baseColor.SelecedIndex)
            {
                case 0:
                    this.HighContrastColor.Color = "#FF6699ff";
                    this.HighlightColor.Color = "#FF6699ff";
                    this.LowlightColor.Color = "#FF3366cc";
                    this.MutedColor.Color = "#FF336699";
                    break;

                case 1:
                    this.HighContrastColor.Color = "#FF9966ff";
                    this.HighlightColor.Color = "#FF9966ff";
                    this.LowlightColor.Color = "#FF6633cc";
                    this.MutedColor.Color = "#FF663399";
                    break;

                case 2:
                    this.HighContrastColor.Color = "#FFff6699";
                    this.HighlightColor.Color = "#FFff6699";
                    this.LowlightColor.Color = "#FFcc3366";
                    this.MutedColor.Color = "#FF993366";
                    break;

                case 3:
                    this.HighContrastColor.Color = "#FF66ff99";
                    this.HighlightColor.Color = "#FF66ff99";
                    this.LowlightColor.Color = "#FF33cc66";
                    this.MutedColor.Color = "#FF339966";
                    break;

                case 4:
                    this.HighContrastColor.Color = "#FFffcc33";
                    this.HighlightColor.Color = "#FFffcc33";
                    this.LowlightColor.Color = "#FFff9900";
                    this.MutedColor.Color = "#FFcc9900";
                    break;

                case 5:
                    this.HighContrastColor.Color = "#FFcccccc";
                    this.HighlightColor.Color = "#FFcccccc";
                    this.LowlightColor.Color = "#FF999999";
                    this.MutedColor.Color = "#FF666666";
                    break;

                case 6:
                    this.HighContrastColor.Color = "#FF33ffff";
                    this.HighlightColor.Color = "#FF33ffff";
                    this.LowlightColor.Color = "#FF00ccff";
                    this.MutedColor.Color = "#FF0099cc";
                    break;

                case 7:
                    this.HighContrastColor.Color = "#FFff9933";
                    this.HighlightColor.Color = "#FFff9933";
                    this.LowlightColor.Color = "#FFff6600";
                    this.MutedColor.Color = "#FFcc6600";
                    break;

                case 8:
                    this.HighContrastColor.Color = "#FFff66ff";
                    this.HighlightColor.Color = "#FFff66ff";
                    this.LowlightColor.Color = "#FFcc33cc";
                    this.MutedColor.Color = "#FF993399";
                    break;

                case 9:
                    this.HighContrastColor.Color = "#FFccff33";
                    this.HighlightColor.Color = "#FFccff33";
                    this.LowlightColor.Color = "#FF99cc00";
                    this.MutedColor.Color = "#FF669900";
                    break;
            }
        }

        /// <summary>
        /// 設定状況取得
        /// </summary>
        /// <returns>Task</returns>
        private async Task Pull()
        {
            var theme = await this.client.PersonalizationManager.GetThemeAsync();

            this.BaseColor.Color = ColorToString(theme.Base);
            this.HighContrastColor.Color = ColorToString(theme.HighContrast);
            this.HighlightColor.Color = ColorToString(theme.Highlight);
            this.LowlightColor.Color = ColorToString(theme.Lowlight);
            this.MutedColor.Color = ColorToString(theme.Muted);
            this.SecondaryTextColor.Color = ColorToString(theme.SecondaryText);
        }

        /// <summary>
        /// BandColor をカラーコード文字列に変換する
        /// </summary>
        /// <param name="color">BandColor</param>
        /// <returns>カラーコード</returns>
        private static string ColorToString(BandColor color)
        {
            return string.Format("#FF{0}{1}{2}", color.R.ToString("X2"), color.G.ToString("X2"), color.B.ToString("X2"));
        }

        /// <summary>
        /// 配色文字列を BandColor に変換する
        /// </summary>
        /// <param name="colorString">配色文字列を</param>
        /// <returns>BandColor</returns>
        private static BandColor StringToColor(string colorString)
        {
            var color = (Color)ColorConverter.ConvertFrom(colorString);
            color.ToString();
            return new BandColor(
                (byte)(color.R * 255d),
                (byte)(color.G * 255d),
                (byte)(color.B * 255d));
        }

        /// <summary>
        /// 配色コンバーター
        /// </summary>
        private static ColorTypeConverter ColorConverter = new ColorTypeConverter();

        /// <summary>
        /// 設定適用
        /// </summary>
        /// <returns>Task</returns>
        private async Task Apply()
        {
            var theme = new BandTheme()
            {
                Base = StringToColor(this.BaseColor.Color),
                HighContrast = StringToColor(this.HighContrastColor.Color),
                Highlight = StringToColor(this.HighlightColor.Color),
                Lowlight = StringToColor(this.LowlightColor.Color),
                Muted = StringToColor(this.MutedColor.Color),
                SecondaryText = StringToColor(this.SecondaryTextColor.Color),
            };

            await this.client.PersonalizationManager.SetThemeAsync(theme);
        }
    }
}
