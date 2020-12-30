// <copyright file="IWhatsNewDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Services
{
    using Prism.Events;

    public interface IWhatsNewDisplayService
    {
        bool ShowIfAppropriate(IEventAggregator iocEventAggregator);
    }
}