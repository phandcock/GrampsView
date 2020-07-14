// <copyright file="GrampsStoreXMLTags.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
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
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Loads the tags from Gramps XML file asynchronously.
        /// </summary>
        /// <returns>
        /// True if loaded successfully.
        /// </returns>
        public async Task LoadTagsAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Tag data").ConfigureAwait(false);
            {
                // XNamespace ns = grampsXMLNameSpace;
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
                        loadTag.HomeImageHLink.HomeSymbolColour = loadTag.GColor;

                        // save the Tag
                        DV.TagDV.TagData.Add(loadTag);
                    }
                }
                catch (Exception ex)
                {
                    DataStore.CN.NotifyException("Error in LoadTagsAsync", ex);
                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);

            return;
        }
    }
}