// <copyright file="IFirstRunDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Events;

namespace GrampsView.Services
{
    public interface IFirstRunDisplayService
    {
        bool ShowIfAppropriate(IEventAggregator iocEventAggregator);
    }
}