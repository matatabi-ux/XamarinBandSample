using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Xamarin.Forms;

namespace XamarinBandSample.Views
{
    public partial class TopPage : ContentPage, IView
    {
        public TopPage()
        {
            InitializeComponent();
        }

        public object DataContext
        {
            get
            {
                return this.BindingContext;
            }
            set
            {
                this.BindingContext = value;
            }
        }
    }
}
