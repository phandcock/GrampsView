//-----------------------------------------------------------------------
// Storage routines for GrampsStoreXML
//
// <copyright file="GrampsStoreXMLCitations.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

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

    /// <summary>
    /// Load Citations from external storage routines.
    /// </summary>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        /// <summary>
        /// Sets the Citation home image.
        /// </summary>
        /// <param name="argModel">
        /// The argument model.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// argModel
        /// </exception>
        public static CitationModel SetHomeImage(CitationModel argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            // Try media reference collection first
            HLinkHomeImageModel hlink = argModel.GMediaRefCollection.FirstHLinkHomeImage;

            // Check Source for Image
            if ((!hlink.Valid) && (argModel.GSourceRef.DeRef.HomeImageHLink.LinkToImage))
            {
                hlink = argModel.GSourceRef.DeRef.HomeImageHLink;
            }

            // Handle the link if we can
            if (hlink.Valid)
            {
                argModel.HomeImageHLink.HomeImageType = CommonEnums.HomeImageType.ThumbNail;
                argModel.HomeImageHLink.HLinkKey = hlink.HLinkKey;
            }

            return argModel;
        }

        /// <summary>
        /// Load Citations from external storage.
        /// </summary>
        /// <returns>
        /// Flag if loaded successfully.
        /// </returns>
        public async Task LoadCitationsAsync()
        {
            await DataStore.CN.MajorStatusAdd("Loading Citation data").ConfigureAwait(false);
            {
                // Load notes

                try
                {
                    // Run query
                    var de =
                        from el in localGrampsXMLdoc.Descendants(ns + "citation")
                        select el;

                    // Loop through results to get the Citation

                    foreach (XElement pcitation in de)
                    {
                        CitationModel loadCitation = DV.CitationDV.NewModel();

                        // Citation attributes
                        loadCitation.LoadBasics(GetBasics(pcitation));
                        //loadCitation.Id = GetAttribute(pcitation, "id");
                        //loadCitation.Change = GetDateTime(pcitation, "change");
                        //loadCitation.Priv = SetPrivateObject(GetAttribute(pcitation, "priv"));
                        //loadCitation.Handle = GetAttribute(pcitation, "handle");

                        if (loadCitation.Id == "C0656")
                        {
                        }

                        // Citation fields

                        loadCitation.GDateContent = GetDate(pcitation);

                        loadCitation.GPage = GetElement(pcitation.Element(ns + "page"));

                        loadCitation.GConfidence = GetElement(pcitation.Element(ns + "confidence"));

                        loadCitation.GNoteRefCollection = GetNoteCollection(pcitation);

                        // ObjectRef loading
                        loadCitation.GMediaRefCollection = await GetObjectCollection(pcitation).ConfigureAwait(false);

                        loadCitation.GSourceAttributeCollection = GetSrcAttributeCollection(pcitation);

                        loadCitation.GSourceRef.HLinkKey = GetAttribute(pcitation.Element(ns + "sourceref"), "hlink");

                        loadCitation.GTagRef = GetTagCollection(pcitation);

                        // set the Home image or symbol now that everything is laoded
                        loadCitation = SetHomeImage(loadCitation);

                        // save the event
                        DV.CitationDV.CitationData.Add(loadCitation);
                    }
                }
                catch (Exception e)
                {
                    DataStore.CN.NotifyException("Exception loading Citations form XML", e);
                    throw;
                }
            }

            await DataStore.CN.MajorStatusDelete().ConfigureAwait(false);
            return;
        }
    }
}