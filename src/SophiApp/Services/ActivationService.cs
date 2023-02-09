﻿namespace SophiApp.Services
{
    using System.Threading.Tasks;
    using Microsoft.UI.Xaml;
    using SophiApp.Contracts.Services;
    using SophiApp.Views;

    /// <inheritdoc/>
    public class ActivationService : IActivationService
    {
        private UIElement? shell;

        /// <inheritdoc/>
        public async Task ActivateAsync(object activationArgs)
        {
            // Execute tasks before activation.
            await InitializeAsync();

            // Set the MainWindow Content.
            if (App.MainWindow.Content == null)
            {
                shell = App.GetService<NavigationPage>();
                App.MainWindow.Content = shell;
            }

            // Activate the MainWindow.
            App.MainWindow.Activate();

            // Execute tasks after activation.
            await StartupAsync();
        }

        private async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        private async Task StartupAsync()
        {
            await Task.CompletedTask;
        }
    }
}