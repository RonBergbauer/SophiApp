namespace SophiApp.Views
{
    using Microsoft.UI.Xaml;
    using Microsoft.UI.Xaml.Controls;
    using Microsoft.UI.Xaml.Input;
    using Microsoft.UI.Xaml.Media;
    using SophiApp.Helpers;
    using SophiApp.ViewModels;
    using Windows.System;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NavigationPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationPage"/> class.
        /// </summary>
        public NavigationPage()
        {
            InitializeComponent();
            App.MainWindow.ExtendsContentIntoTitleBar = true;
            App.MainWindow.SetTitleBar(AppTitleBar);
            App.MainWindow.Activated += MainWindow_Activated;
            AppTitleBarText.Text = $"{App.Name} {App.Version}";
        }

        /// <summary>
        /// Gets <see cref="NavigationViewModel"/>.
        /// </summary>
        public NavigationViewModel? ViewModel
        {
            get;
        }

        private static KeyboardAccelerator BuildKeyboardAccelerator(VirtualKey key, VirtualKeyModifiers? modifiers = null)
        {
            var keyboardAccelerator = new KeyboardAccelerator() { Key = key };

            if (modifiers.HasValue)
            {
                keyboardAccelerator.Modifiers = modifiers.Value;
            }

            keyboardAccelerator.Invoked += OnKeyboardAcceleratorInvoked;

            return keyboardAccelerator;
        }

        private static void OnKeyboardAcceleratorInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
        }

        private void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            var resource = args.WindowActivationState == WindowActivationState.Deactivated ? "WindowCaptionForegroundDisabled" : "WindowCaptionForeground";

            AppTitleBarText.Foreground = (SolidColorBrush)App.Current.Resources[resource];
        }

        private void NavigationViewControl_DisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
        {
            AppTitleBar.Margin = new Thickness()
            {
                Left = sender.CompactPaneLength * (sender.DisplayMode == NavigationViewDisplayMode.Minimal ? 2 : 1),
                Top = AppTitleBar.Margin.Top,
                Right = AppTitleBar.Margin.Right,
                Bottom = AppTitleBar.Margin.Bottom,
            };
        }

        private void OnLoaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            TitleBarHelper.UpdateTitleBar(RequestedTheme);
            KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.Left, VirtualKeyModifiers.Menu));
            KeyboardAccelerators.Add(BuildKeyboardAccelerator(VirtualKey.GoBack));
        }
    }
}