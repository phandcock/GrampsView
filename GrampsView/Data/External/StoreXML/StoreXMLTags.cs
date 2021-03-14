namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Load Tags from Gramps XML file.
    /// </summary>
    public partial class StoreXML : IStoreXML
    {
        /// <summary>
        /// Loads the tags from Gramps XML file asynchronously.
        /// </summary>
        /// <returns>
        /// True if loaded successfully.
        /// </returns>
        public async Task LoadTagsAsync()
        {
            await DataStore.Instance.CN.DataLogEntryAdd("Loading Tag data").ConfigureAwait(false);
            {
                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "tag")
                        select el;

                    // get Tag fields

                    // Loop through results
                    foreach (XElement pTagElement in de)
                    {
                        TagModel loadTag = new TagModel
                        {
                            // Citation attributes

                            //Id = GetAttribute(pTagElement, "id"),
                            //Change = GetDateTime(pTagElement, "change"),
                            //Priv = SetPrivateObject(GetAttribute(pTagElement, "priv")),
                            //Handle = GetAttribute(pTagElement, "handle"),

                            // Tag fields
                            GColor = GetColour(pTagElement, "color"),
                            GName = GetAttribute(pTagElement, "name"),
                            GPriority = int.Parse(GetAttribute(pTagElement, "priority"), System.Globalization.CultureInfo.CurrentCulture)
                        };

                        loadTag.LoadBasics(GetBasics(pTagElement));

                        // Set tag colour
                        loadTag.ModelItemGlyph.SymbolColour = loadTag.GColor;

                        // save the Tag
                        DV.TagDV.TagData.Add(loadTag);
                    }
                }
                catch (Exception ex)
                {
                    DataStore.Instance.CN.NotifyException("Error in LoadTagsAsync", ex);
                    throw;
                }
            }

            await DataStore.Instance.CN.DataLogEntryReplace("Tag load complete");

            return;
        }
    }
}