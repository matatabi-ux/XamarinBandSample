#region License
//-----------------------------------------------------------------------
// <copyright>
//     Copyright matatabi-ux 2015.
// </copyright>
//-----------------------------------------------------------------------
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using Xamarin.Forms;
using XamarinBandSample.Views;

namespace XamarinBandSample
{
    /// <summary>
    /// アプリケーション基盤クラス
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// DI コンテナ
        /// </summary>
        public static UnityContainer Container = new UnityContainer();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        static App()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public App()
        {
            // ViewModel をインスタンス化するデフォルトメソッドを指定
            ViewModelLocationProvider.SetDefaultViewModelFactory((type) => Container.Resolve(type));

            this.MainPage = new TopPage();
        }

        /// <summary>
        /// アプリ起動時処理
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// アプリ中断時処理
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// アプリ再開時処理
        /// </summary>
        protected override void OnResume()
        {
        }
    }

}
