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
using System.Windows.Input;
using Microsoft.Practices.Unity.Utility;
using Xamarin.Forms;

namespace XamarinBandSample.Triggers
{
    /// <summary>
    /// Command を実行するトリガーアクション
    /// </summary>
    public class InvokeCommandAction : TriggerAction<Element>
    {
        #region Command

        /// <summary>
        /// Command
        /// </summary>
        private ICommand command = null;

        /// <summary>
        /// Command の Binding 情報
        /// </summary>
        public Binding Command { get; set; }

        #endregion //Command

        #region CommandParameter

        /// <summary>
        /// Command のパラメーター
        /// </summary>
        private object commandParameter = null;

        /// <summary>
        /// Command のパラメーターの Binding 情報
        /// </summary>
        public Binding CommandParameter { get; set; }

        #endregion //CommandParameter

        /// <summary>
        /// トリガーアクションを実行する
        /// </summary>
        /// <param name="sender">アクション実行者</param>
        protected override void Invoke(Element sender)
        {
            if (this.Command == null || this.Command.Path == null || sender.BindingContext == null)
            {
                return;
            }

            // Binding 情報を解析して、Element.BidingContext から Command と CommandParameter を取得する 
            var bindingContext = sender.BindingContext;
            if (string.IsNullOrWhiteSpace(this.Command.Path))
            {
                this.command = bindingContext as ICommand;
            }
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                             where p.CanRead && this.Command.Path.Equals(p.Name)
                             select p.GetValue(bindingContext)).FirstOrDefault();
                if (this.Command.Converter != null)
                {
                    value = this.Command.Converter.Convert(
                        value,
                        typeof(ICommand),
                        this.Command.ConverterParameter,
                        CultureInfo.CurrentCulture);
                }
                this.command = value as ICommand;
            }
            if (string.IsNullOrWhiteSpace(this.CommandParameter.Path))
            {
                this.commandParameter = bindingContext;
            }
            else
            {
                var value = (from p in bindingContext.GetType().GetPropertiesHierarchical()
                             where p.CanRead && this.CommandParameter.Path.Equals(p.Name)
                             select p.GetValue(bindingContext)).FirstOrDefault();
                if (this.CommandParameter.Converter != null)
                {
                    value = this.CommandParameter.Converter.Convert(
                        value, 
                        typeof(object), 
                        this.CommandParameter.ConverterParameter,
                        CultureInfo.CurrentCulture);
                }
                this.commandParameter = value;
            }

            // 実行可能であれば Command を呼び出す
            if (this.command == null || !this.command.CanExecute(this.commandParameter))
            {
                return;
            }

            this.command.Execute(this.commandParameter);
        }
    }
}
