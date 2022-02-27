using System;
using Foundation;
using UIKit;

namespace TestApp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        private IDisposable testDisposable;
        
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            var formsApp = new App();
            LoadApplication(formsApp);

            testDisposable = formsApp
                .WhenChanged(a => a.IsLightTheme)
                .Subscribe(isLightTheme =>
                {
                    //do something
                });

            return base.FinishedLaunching(app, options);
        }
    }
}
