// Copyright (c) phandcock.  All rights reserved.

namespace GrampsView.Common
{
    /// <summary>
    /// Common Constants.
    /// </summary>
    public static class Constants
    {
        public const string DatabaseFilename = "GrampsViewSQLite.db3";

        /// <summary>
        /// The gramps view database version.
        /// </summary>
        public const int GrampsViewDatabaseVersion = 90;

        public static readonly string DirectoryCacheBase = "~GV";

        public static readonly string DirectoryImageCache = "~worka";
        //public const SQLite.SQLiteOpenFlags Flags =
        //    // open the database in read/write mode
        //    SQLite.SQLiteOpenFlags.ReadWrite |
        //    // create the database if it doesn't exist
        //    SQLite.SQLiteOpenFlags.Create |
        //    // enable multi-threaded database access
        //    SQLite.SQLiteOpenFlags.SharedCache;

        /// <summary>
        /// The event type birth.
        /// </summary>
        public static readonly string EventTypeBirth = "Birth";

        /// <summary>
        /// The event type death.
        /// </summary>
        public static readonly string EventTypeDeath = "Death";

        /// <summary>
        /// The event type marriage.
        /// </summary>
        public static readonly string EventTypeMarriage = "Marriage";

        /// <summary>
        /// The Gramps XML name space.
        /// </summary>
        public static readonly string GrampsXMLNameSpace = "http://gramps-project.org/xml/1.5.1/";

        /// <summary>
        /// The Gramps XML public identifier.
        /// </summary>
        public static readonly string GrampsXMLPublicId = "-//Gramps//DTD Gramps XML 1.5.1//EN";

        public static readonly string IconAbout = IconMaterialIconsOutline.Info;

        /// <summary>
        /// The icon Address.
        /// </summary>
        public static readonly string IconAddress = IconMaterialIconsOutline.Mail;

        ///// <summary>
        ///// The icon attribute.
        ///// </summary>
        public static readonly string IconAttribute = IconMaterialIconsOutline.Attribution;

        /// <summary>
        /// The icon bookMark.
        /// </summary>
        public static readonly string IconBookMark = IconMaterialIconsOutline.Bookmark;

        /// <summary>
        /// The icon ChooseDataFile.
        /// </summary>
        public static readonly string IconChooseDataFile = IconMaterialIconsOutline.Download;

        /// <summary>
        /// The icon citation.
        /// </summary>
        public static readonly string IconCitation = IconMaterialIconsOutline.Receipt;

        /// <summary>
        /// The icon for a date symbol.
        /// </summary>
        public static readonly string IconDate = IconMaterialIconsOutline.Date_range;

        /// <summary>
        /// The icon for a default symbol.
        /// </summary>
        public static readonly string IconDDefault = IconMaterialIconsOutline.Emoji_symbols;

        /// <summary>
        /// The icon diagram.
        /// </summary>
        public static readonly string IconDiagram = IconMaterialIconsOutline.Area_chart;

        /// <summary>
        /// The icon events.
        /// </summary>
        public static readonly string IconEvents = IconMaterialIconsOutline.Event;

        /// <summary>
        /// The icon families.
        /// </summary>
        public static readonly string IconFamilies = IconMaterialIconsOutline.Family_restroom;

        public static readonly string IconHeader = IconMaterialIconsOutline.View_headline;

        /// <summary>
        /// The icon hub.
        /// </summary>
        public static readonly string IconHub = IconMaterialIconsOutline.Hub;

        public static readonly string IconLog = IconMaterialIconsOutline.List;

        /// <summary>
        /// The map icon.
        /// </summary>
        public static readonly string IconMap = IconMaterialIconsOutline.Map;

        /// <summary>
        /// The icon media.
        /// </summary>
        public static readonly string IconMedia = IconMaterialIconsOutline.Picture_in_picture;

        // "\uf82f";
        /// <summary>
        /// The symbol for NameMaps.
        /// </summary>
        public static readonly string IconNameMaps = IconMaterialIconsOutline.Tsunami;

        /// <summary>
        /// The icon notes.
        /// </summary>
        public static readonly string IconNotes = IconMaterialIconsOutline.Book;

        /// <summary>
        /// The icon people.
        /// </summary>
        public static readonly string IconPeople = IconMaterialIconsOutline.People;

        public static readonly string IconPeopleBirthday = IconMaterialIconsOutline.Cake;

        /// <summary>
        /// The icon people graph.
        /// </summary>
        public static readonly string IconPeopleGraph = IconMaterialIconsOutline.Auto_graph;

        /// <summary>
        /// The icon person female.
        /// </summary>
        public static readonly string IconPersonFemale = IconMaterialIconsOutline.Female;

        /// <summary>
        /// The icon person male.
        /// </summary>
        public static readonly string IconPersonMale = IconMaterialIconsOutline.Male;

        public static readonly string IconPersonName = IconMaterialIconsOutline.Tsunami;

        /// <summary>
        /// The symbol Places.
        /// </summary>
        public static readonly string IconPlace = IconMaterialIconsOutline.Place;

        /// <summary>
        /// The symbol Repository.
        /// </summary>
        public static readonly string IconRepository = IconMaterialIconsOutline.Health_and_safety;

        /// <summary>
        /// The icon Search.
        /// </summary>
        public static readonly string IconSearch = IconMaterialIconsOutline.Search;

        /// <summary>
        /// The icon Settings.
        /// </summary>
        public static readonly string IconSettings = IconMaterialIconsOutline.Settings;

        /// <summary>
        /// The icon Source.
        /// </summary>
        public static readonly string IconSource = IconMaterialIconsOutline.Source;

        public static readonly string IconSurname = IconMaterialIconsOutline.Surfing;

        /// <summary>
        /// The icon person male.
        /// </summary>
        public static readonly string IconTag = IconMaterialIconsOutline.Tag;

        public static readonly string IconURL = IconMaterialIconsOutline.Link;

        //public static readonly string IconThemeSystem = IconMaterialIconsOutline.Globe;
        /// <summary>
        /// A base name to use when moving generated log files into the app's log file folder.
        /// </summary>
        public static readonly string LogAppLogFileBaseName = "GrampsViewLog";

        //public static readonly string IconThemeLight = IconMaterialIconsOutline.Sun;
        /// <summary>
        /// LoggingScenario moves generated logs files into the this folder under the LocalState folder.
        /// </summary>
        public static readonly string LogAppLogFolderName = "LogFiles";

        //public static readonly string IconThemeDark = IconMaterialIconsOutline.Moon;
        /// <summary>
        /// The App Channel default.
        /// </summary>
        public static readonly string LogDefaultChannelName = "GrampsViewChannelDefault";

        /// <summary>
        /// The this.localFileLogSession Name Default.
        /// </summary>
        public static readonly string LogDefaultSessionName = "GrampsViewSessionDefault";

        public static readonly string ModelNameFamily = "family";

        public static readonly string ModelNamePerson = "person";

        public static readonly string NameTypeMarried = "Married Name";

        public static readonly string NoteTypeBiography = "Biography";

        public static readonly string NoteTypeCitation = "Citation";

        public static readonly string NoteTypeEvent = "Event Note";

        public static readonly string NoteTypeLink = "Link";

        public static readonly string NoteTypePersonNote = "Person Note";

        public static readonly string NoteTypeToDo = "To Do";

        /// <summary>
        /// The search no results.
        /// </summary>
        public static readonly string SearchNoResults = "No Results";

        public static readonly string SettingsDataStorageFolder = "SettingsDataStorageFolder";

        /// <summary>
        /// The settings GPKG file last date time modified.
        /// </summary>
        public static readonly string SettingsGPKGFileLastDateTimeModified = "SettingsGPKGFileLastDateTimeModified";

        /// <summary>
        /// The settings GPKG file name.
        /// </summary>
        public static readonly string SettingsGPKGFileName = "GPKGFileName";

        /// <summary>
        /// The settings gpramps file last date time modified.
        /// </summary>
        public static readonly string SettingsGPRAMPSFileLastDateTimeModified = "SettingsGPRAMPSFileLastDateTimeModified";

        /// <summary>
        /// The settings XML file last date time modified.
        /// </summary>
        public static readonly string SettingsXMLFileLastDateTimeModified = "SettingsXMLFileLastDateTimeModified";

        //public static readonly string SettingsGrampsViewDatabaseVersion = "GrampsViewDatabaseVersion";
        /// <summary>
        /// The local data file extension.
        /// </summary>
        public static readonly string StorageGPKGMimeType = "gpkg";

        /// <summary>
        /// The storage gramp no media file name.
        /// </summary>
        public static readonly string StorageGRAMPSFileName = "data.gramps";

        /// <summary>
        /// The local data folder.
        /// </summary>
        public static readonly string StorageInternalFolder = "data\\";

        /// <summary>
        /// The gramps XML file name.
        /// </summary>
        public static readonly string StorageXMLFileName = "data.xml";

        public static string DatabasePath =>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        /*
         * Note Types
         */
        ///// <summary>
        ///// The storage thumbnail folder.
        ///// </summary>
        //public static readonly string StorageThumbNailFolder = "Thumbnails";
    }
}