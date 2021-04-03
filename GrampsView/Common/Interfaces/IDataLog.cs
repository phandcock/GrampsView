//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="ICommonNotifications.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IDataLog
    {
        ObservableCollection<DataLogEntry> DataLoadLog
        {
            get;
        }

        Task<bool> Add(string entry);

        Task<bool> Clear();

        Task<bool> Remove();

        Task<bool> Replace(string entry);
    }
}