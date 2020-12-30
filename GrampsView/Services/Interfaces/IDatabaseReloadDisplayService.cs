// <copyright file="IDatabaseReloadDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using Prism.Events;

    /// <summary>
    /// Display the database reload view if required.
    /// </summary>
    public interface IDatabaseReloadDisplayService
    {
        bool ShowIfAppropriate(IEventAggregator iocEventAggregator);
    }
}