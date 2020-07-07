// <copyright file="IInputFIleDisplayService.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Prism.Navigation;

namespace GrampsView.Services
{
    public interface IInputFileDisplayService
    {
        void ShowIfAppropriate(INavigationService iocNavigationService);
    }
}