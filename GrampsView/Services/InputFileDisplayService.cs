// <copyright file="InputFileDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Common;
    using GrampsView.Data.Repository;
    using GrampsView.Views;

    using Prism.Navigation;

    // For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    public class InputFileDisplayService : IInputFileDisplayService
    {
        private static bool shown = false;

        public InputFileDisplayService()
        {
        }

        /// <summary>
        /// Shows if appropriate asynchronous.
        /// </summary>
        public void ShowIfAppropriate(INavigationService iocNavigationService)
        {
            if (!CommonLocalSettings.DataSerialised && !shown)
            {
                shown = true;

                DataStore.NV.Nav(nameof(FileInputHandlerPage));
            }
        }
    }
}