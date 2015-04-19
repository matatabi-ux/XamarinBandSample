using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using global::Microsoft.Band;
using Microsoft.Practices.Unity;
using XamarinBandSample.Droid.Band;

namespace XamarinBandSample.Droid
{
    [Activity(Label = "XamarinBandSample", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Microsoft Band デバイス管理クラスを DI コンテナに登録
            App.Container.RegisterType<IBandClientManager, NativeBandClientManager>(new ContainerControlledLifetimeManager());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

