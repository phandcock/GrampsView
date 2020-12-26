// <copyright file="InputFileDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Views;

    using Prism.Navigation;

    using Xamarin.Forms;

    // For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    public class InputFileDisplayService : IInputFileDisplayService
    {
        private static bool shown;

        public InputFileDisplayService()
        {
        }

        /// <summary>
        /// Shows if appropriate asynchronous.
        /// </summary>
        public void ShowIfAppropriate()
        {
            if (!CommonLocalSettings.DataSerialised && !shown)
            {
                shown = true;

                Shell.Current.GoToAsync(nameof(FileInputHandlerPage));
                //DataStore.Instance.NV.Nav(nameof(FileInputHandlerPage));
            }
        }
    }
}