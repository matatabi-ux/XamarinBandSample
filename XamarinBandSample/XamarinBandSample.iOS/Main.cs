using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.Band;
using Microsoft.Practices.Unity;
using UIKit;
using XamarinBandSample.iOS.Band;

namespace XamarinBandSample.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // Microsoft Band デバイス管理クラスを DI コンテナに登録
            App.Container.RegisterType<IBandClientManager, NativeBandClientManager>(new ContainerControlledLifetimeManager());

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
