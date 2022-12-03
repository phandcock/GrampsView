using GrampsView.Data.DataView;
using GrampsView.Data.External.StoreXML;
using GrampsView.Data.Model;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
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
        public Task LoadTagsAsync()
        {
            MyLog.DataLogEntryAdd("Loading Tag data");
            {
                try
                {
                    // Run query
                    System.Collections.Generic.IEnumerable<XElement> de =
                        from el in LocalGrampsXMLdoc.Descendants(ns + "tag")
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
                    MyNotifications.NotifyException("Error in LoadTagsAsync",ex,null);
                    throw;
                }
            }

            MyLog.DataLogEntryReplace("Tag load complete");
            return Task.CompletedTask;
        }
    }
}