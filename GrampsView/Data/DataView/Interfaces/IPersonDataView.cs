//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IPersonDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// I Person Repository.
    /// </summary>
    public interface IPersonDataView : IDataViewBase<PersonModel, HLinkPersonModel, HLinkPersonModelCollection>
    {
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        HLinkPersonModel Current
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        RepositoryModelDictionary<PersonModel, HLinkPersonModel> PersonData
        {
            get;
        }

        CardGroup GetAllAsGroupedBirthDayCardGroup();

        CardGroup GetAllAsGroupedSurnameCardGroup();

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkPersonModelCollection GetAllAsHLink();

        ///// <summary>
        ///// Gets the groups by category.
        ///// </summary>
        ///// <returns>
        ///// List.
        ///// </returns>
        //CommonGroupInfoCollection GetGroupsByCategory();
        /// <summary>
        /// Gets the person plus family events.
        /// </summary>
        /// <param name="argPerson">
        /// The argument person.
        /// </param>
        /// <returns>
        /// Person and where parent in families events.
        /// </returns>
        ObservableCollection<EventModel> GetPersonPlusFamilyEvents(PersonModel argPerson);

        List<HLinkPersonModel> SearchShell(string argQuery);

        ///// <summary>
        ///// Gets the default image from collection.
        ///// </summary>
        ///// <param name="argModel">
        ///// The argument ViewModel.
        ///// </param>
        ///// <returns>
        ///// </returns>
        //HLinkGlyph GetDefaultImageFromCollection(PersonModel argModel);
    }
}