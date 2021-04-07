﻿// <copyright file="DatabaseReloadDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using GrampsView.Common;

    using System.Threading.Tasks;

    // For instructions on testing this service see https://github.com/Microsoft/WindowsTemplateStudio/tree/master/docs/features/whats-new-prompt.md
    public class DatabaseReloadDisplayService : IDatabaseReloadDisplayService
    {
        public DatabaseReloadDisplayService()
        {
        }

        /// <summary>
        /// Shows database reload view if appropriate.
        /// </summary>
        /// <returns>
        /// if the view is displayed.
        /// </returns>
        public async Task<bool> ShowIfAppropriate()
        {
            if (CommonLocalSettings.DatabaseReloadNeeded)
            {
                return true;
            }

            return false;
        }
    }
}