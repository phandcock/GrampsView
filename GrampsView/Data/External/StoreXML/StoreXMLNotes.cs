namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// Load Notes from external storage.
        /// </summary>
        /// <param name="noteRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadNotesAsync()
        {
            _iocCommonNotifications.DataLogEntryAdd("Loading Note data");
            {
                // Load notes
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "note")
                        select el;

                    // get event fields TODO

                    // Loop through results to get the Notes Uri _baseUri = new Uri("ms-appx:///");
                    foreach (XElement pname in de)
                    {
                        INoteModel loadNote = new NoteModel();

                        // Note attributes
                        loadNote.LoadBasics(GetBasics(pname));

                        //loadNote.HLinkKey = loadNote.Handle;
                        loadNote.GIsFormated = GetBool(pname, "format");
                        loadNote.GType = (string)pname.Attribute("type");

                        // Load Styled Text
                        if (loadNote.Id == "N0482")
                        {
                        }

                        if (loadNote.GIsFormated)
                        {
                        }

                        // Get Text Styles
                        StyledTextModel tempStyledText = GetStyledTextCollection(pname);
                        loadNote.GStyledText.GText = tempStyledText.GText;

                        loadNote.GStyledText.Styles.Clear();
                        foreach (GrampsStyle item in tempStyledText.Styles)
                        {
                            loadNote.GStyledText.Styles.Add(item);
                        }

                        // Get Tags
                        loadNote.GTagRefCollection.Clear();
                        foreach (HLinkTagModel item in GetTagCollection(pname))
                        {
                            loadNote.GTagRefCollection.Add(item);
                        }

                        DV.NoteDV.NoteData.Add((NoteModel)loadNote);
                    }
                }
                catch (Exception ex)
                {
                    // TODO handle this
                    _iocCommonNotifications.NotifyException("Exception loading Notes from the Gramps file", ex);

                    throw;
                }
            }

            _iocCommonNotifications.DataLogEntryReplace("Note load complete");
            return;
        }

        private FormattedString GetFormattedString(XElement argStyledText)
        {
            FormattedString loadString = new FormattedString();

            string theText = (string)argStyledText.Element(ns + "text");

            loadString.Spans.Add(new Span { Text = theText, FontSize = CommonFontSize.FontMedium });

            return loadString;
        }
    }
}