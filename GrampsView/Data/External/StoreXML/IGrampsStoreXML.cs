//-----------------------------------------------------------------------
//
// Interface definition for External Storage Providers
//
// <copyright file="IGrampsStoreXML.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface definitions for IExternal Storage.
    /// </summary>
    public interface IGrampsStoreXML
    {
        /// <summary>
        /// Initialises the data storage asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        // Task<bool> DataStorageInitialiseAsync(StorageFolder dataStorageFolder);

        /// <summary>
        /// Loads the gramps data only.
        /// </summary>
        /// <returns>
        /// Flag if Gramps Data Only loaded successfully
        /// </returns>
        Task<bool> DataStorageLoadXML();

        /// <summary>
        /// Loads the BookMark data asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadBookMarksAsync();

        /// <summary>
        /// Loads the citation data asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadCitationsAsync();

        ///// <summary>
        ///// Loads the new data asynchronous.
        ///// </summary>
        ///// <param name="grampsDataFile"> The gramps data file. </param>
        ///// <returns></returns>
        // Task<bool> DataStorageUnZIPFile(StorageFile grampsDataFile);
        /// <summary>
        /// load events from external storage.
        /// </summary>
        /// <param name="eventRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag if EVents loaded successfully.
        /// </returns>
        Task LoadEventsAsync();

        /// <summary>
        /// load families from external storage.
        /// </summary>
        /// <param name="familyRepository">
        /// The family repository.
        /// </param>
        /// <returns>
        /// </returns>
        Task<bool> LoadFamiliesAsync();

        /// <summary>
        /// Loads the header meta data.
        /// </summary>
        /// <returns>
        /// Flag if Header Metadata loaded successfully.
        /// </returns>
        Task LoadHeaderDataAsync();

        /// <summary>
        /// load media objects from external storage.
        /// </summary>
        /// <param name="mediaRepository">
        /// The media repository.
        /// </param>
        /// <returns>
        /// </returns>
        Task<bool> LoadMediaObjectsAsync();

        /// <summary>
        /// Loads the name maps asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadNameMapsAsync();

        /// <summary>
        /// load Notes from external storage.
        /// </summary>
        /// <param name="notesRepository">
        /// The notes repository.
        /// </param>
        /// <returns>
        /// Flag if Notes data loaded successfully.
        /// </returns>
        Task LoadNotesAsync();

        /// <summary>
        /// load the person data from the external storage XML file.
        /// </summary>
        /// <param name="peopleRepository">
        /// The people repository.
        /// </param>
        /// <returns>
        /// Flag if People Data loaded successfully.
        /// </returns>
        Task LoadPeopleDataAsync();

        /// <summary>
        /// Loads the places asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadPlacesAsync();

        /// <summary>
        /// Loads the repositories asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadRepositoriesAsync();

        /// <summary>
        /// Loads the sources.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadSourcesAsync();

        /// <summary>
        /// Loads the tags asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task LoadTagsAsync();

        /// <summary>
        /// Loads the XML data asynchronous.
        /// </summary>
        /// <returns>
        /// </returns>
        Task<bool> LoadXMLDataAsync();
    }
}