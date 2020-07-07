// <copyright file="GrampsStoreXMLTags.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

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
                    foreach (XElement pcitation in de)
                    {
                        TagModel loadTag = new TagModel
                        {
                            // Citation attributes
                            Id = GetAttribute(pcitation, "id"),
                            Change = GetDateTime(pcitation, "change"),
                            Priv = SetPrivateObject(GetAttribute(pcitation, "priv")),
                            Handle = GetAttribute(pcitation, "handle"),

                            // Tag fields
                            GColor = GetColour(pcitation, "color"),
                            GName = GetAttribute(pcitation, "name"),
                            GPriority = int.Parse(GetAttribute(pcitation, "priority"), System.Globalization.CultureInfo.CurrentCulture)
                        };

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