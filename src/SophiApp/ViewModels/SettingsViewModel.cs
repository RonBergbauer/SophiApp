﻿namespace SophiApp.ViewModels
{
    using System;
    using System.Reflection;
    using System.Windows.Input;
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Microsoft.UI.Xaml;
    using SophiApp.Contracts.Services;
    using SophiApp.Helpers;
    using Windows.ApplicationModel;

    /// <summary>
    /// Model for settings view.
    /// </summary>
    public class SettingsViewModel : ObservableRecipient
    {
        private readonly IThemeSelectorService themeSelectorService;
        private ElementTheme elementTheme;
        private string versionDescription;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="themeSelectorService"><see cref="IThemeSelectorService"/>.</param>
        public SettingsViewModel(IThemeSelectorService themeSelectorService)
        {
            this.themeSelectorService = themeSelectorService;
            elementTheme = this.themeSelectorService.Theme;
            versionDescription = GetVersionDescription();

            SwitchThemeCommand = new RelayCommand<ElementTheme>(
                async (param) =>
                {
                    if (ElementTheme != param)
                    {
                        ElementTheme = param;
                        await this.themeSelectorService.SetThemeAsync(param);
                    }
                });
        }

        /// <summary>
        /// Gets or sets element theme.
        /// </summary>
        public ElementTheme ElementTheme
        {
            get => elementTheme;
            set => SetProperty(ref elementTheme, value);
        }

        /// <summary>
        /// Gets switch theme.
        /// </summary>
        public ICommand SwitchThemeCommand
        {
            get;
        }

        /// <summary>
        /// Gets or sets version description.
        /// </summary>
        public string VersionDescription
        {
            get => versionDescription;
            set => SetProperty(ref versionDescription, value);
        }

        private static string GetVersionDescription()
        {
            Version version;

            if (RuntimeHelper.IsMSIX)
            {
                var packageVersion = Package.Current.Id.Version;

                version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
            }
            else
            {
                version = Assembly.GetExecutingAssembly().GetName().Version!;
            }

            return $"{App.Name} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}