namespace GrampsView.Data.DataView
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;
    using GrampsView.Models.Collections.HLinks;
    using GrampsView.Models.DataModels;

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

        Group<HLinkPersonModelCollection> GetAllAsGroupedBirthDayCardGroup(bool BirthdayShowOnlyLivingFlag);

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkPersonModelCollection GetAllAsHLink();

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

        List<SearcHandlerItem> SearchShell(string argQuery);
    }
}