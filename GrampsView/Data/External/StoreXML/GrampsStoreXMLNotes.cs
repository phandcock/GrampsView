namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using Xamarin.Forms;

    /// <summary>
    /// Private Storage Routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// load Notes from external storage.
        /// </summary>
        /// <param name="noteRepository">
        /// The event repository.
        /// </param>
        /// <returns>
        /// Flag of loaded successfully.
        /// </returns>
        public async Task LoadNotesAsync()
        {
            await DataStore.CN.DataLogEntryAdd("Loading Note data").ConfigureAwait(false);
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
                        INoteModel loadNote = DV.NoteDV.NewModel();

                        // Note attributes
                        loadNote.LoadBasics(GetBasics(pname));
                        //loadNote.Id = (string)pname.Attribute("id");
                        //loadNote.Change = GetDateTime(pname, "change");
                        //loadNote.Priv = SetPrivateObject((string)pname.Attribute("priv"));
                        //loadNote.Handle = (string)pname.Attribute("handle");

                        //loadNote.HLinkKey = loadNote.Handle;
                        loadNote.GIsFormated = GetBool(pname, "format");
                        loadNote.GType = (string)pname.Attribute("type");

                        // Load Styled Text
                        if (loadNote.Id == "N0384")
                        {
                        }

                        StyledTextModel tempStyledText = GetStyledTextCollection(pname);
                        loadNote.GStyledText.GText = tempStyledText.GText;
                        foreach (IGrampsStyle item in tempStyledText.Styles)
                        {
                            loadNote.GStyledText.Styles.Add(item);
                        }

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
                    DataStore.CN.NotifyException("Exception loading Notes form the Gramps file", ex);

                    throw;
                }
            }

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