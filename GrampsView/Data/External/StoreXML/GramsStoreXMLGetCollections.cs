// <summary>
// Utility routines for GramspStore XML readers
// </summary>
// <remarks>
// Can not load and sort as we go as we then lose the ability to choose the first image link for
// references. This can only be done when everything is fully loaded.
// </remarks>
// <copyright file="GramsStoreXMLUtility.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// Various utility and loading routines for XML data.
    /// </summary>
    /// <seealso cref="GrampsView.Data.ExternalStorageNS.IGrampsStoreXML"/>
    /// <seealso cref="IGrampsStoreXML"/>
    public partial class GrampsStoreXML : IGrampsStoreXML
    {
        private HLinkOCAddressModelCollection GetAddressCollection(XElement xmlData)
        {
            HLinkOCAddressModelCollection t = new HLinkOCAddressModelCollection();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "address")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load address object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AddressModel newAddressModel = new AddressModel
                    {
                        GCitationRefCollection = GetCitationCollection(theLoadORElement),

                        GCity = GetElement(theLoadORElement, "city"),

                        GCountry = GetElement(theLoadORElement, "country"),

                        GCounty = GetElement(theLoadORElement, "county"),

                        GDate = GetDate(theLoadORElement),

                        GLocality = GetElement(theLoadORElement, "locality"),

                        GPhone = GetElement(theLoadORElement, "phone"),

                        GPostal = GetElement(theLoadORElement, "postal"),

                        GState = GetElement(theLoadORElement, "state"),

                        GStreet = GetElement(theLoadORElement, "street"),

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                    };

                    // Set model hlinkkey etc
                    string newGuid = Guid.NewGuid().ToString();
                    newAddressModel.HLinkKey = newGuid;
                    newAddressModel.Id = newGuid;
                    newAddressModel.Handle = newGuid;
                    DataStore.DS.AddressData.Add(newAddressModel);

                    // Create a HLink to the model
                    HLinkAdressModel newHlink = new HLinkAdressModel
                    {
                        HLinkKey = newAddressModel.HLinkKey,
                    };

                    t.Add(newHlink);
                }

                // Sort by date
                t.Sort(x => x.DeRef.GDate);
            }

            return t;
        }

        /// <summary>
        /// Gets the attribute collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private OCAttributeModelCollection GetAttributeCollection(XElement xmlData)
        {
            OCAttributeModelCollection t = new OCAttributeModelCollection();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "attribute")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AttributeModel newAttributeModel = new AttributeModel
                    {
                        Handle = "AttributeCollection",

                        GCitationReferenceCollection = GetCitationCollection(theLoadORElement),

                        GNoteModelReferenceCollection = GetNoteCollection(theLoadORElement),

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(newAttributeModel);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the citation collection.
        /// </summary>
        /// <remarks>
        /// Can not sort as we go as we then lose the ability to choose the first image link for
        /// references. This can only be done when everything is fully loaded.
        /// </remarks>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkCitationModelCollection GetCitationCollection(XElement xmlData)
        {
            HLinkCitationModelCollection t = new HLinkCitationModelCollection();

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "citationref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load citation object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkCitationModel t2 = new HLinkCitationModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };
                    t.Add(t2);

                    if (t2.DeRef.Id == "C0525")
                    {
                    }
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the event collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkEventModelCollection GetEventCollection(XElement xmlData)
        {
            HLinkEventModelCollection t = new HLinkEventModelCollection();

            var theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "eventref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkEventModel t2 = new HLinkEventModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                        GRole = GetAttribute(theLoadORElement.Attribute("role")),
                        GAttributeRefCollection = GetAttributeCollection(theLoadORElement),
                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                    };

                    t2.GAttributeRefCollection.Title = "HLink Attributes";
                    t2.GNoteRefCollection.Title = "HLink Notes";

                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the note collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkNoteModelCollection GetNoteCollection(XElement xmlData)
        {
            HLinkNoteModelCollection t = new HLinkNoteModelCollection();

            // Load NoteRefs
            var localNoteElement =
                             from ElementEl in xmlData.Elements(ns + "noteref")
                             select ElementEl;

            if (localNoteElement.Any())
            {
                // load note references
                foreach (XElement loadNoteElement in localNoteElement)
                {
                    HLinkNoteModel noteHLink = new HLinkNoteModel
                    {
                        // object details
                        HLinkKey = GetAttribute(loadNoteElement.Attribute("hlink")),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        /// <summary>
        /// Gets the object collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private async Task<HLinkMediaModelCollection> GetObjectCollection(XElement xmlData)
        {
            HLinkMediaModelCollection t = new HLinkMediaModelCollection();

            var theORElement = from _ORElementEl in xmlData.Elements(ns + "objref")
                               select _ORElementEl;

            if (theORElement.Any())
            {
                // load media object references
                foreach (XElement theLoadORElement in theORElement)
                {
                    // save the MediaObject reference
                    HLinkMediaModel t2 = new HLinkMediaModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };

                    // Get region
                    XElement regionDetails = theLoadORElement.Element(ns + "region");
                    if (regionDetails != null)
                    {
                        HLinkLoadImageModel t3 = new HLinkLoadImageModel
                        {
                            HLinkKey = t2.HLinkKey,
                            HomeImageType = CommonEnums.HomeImageType.ThumbNail,

                            GCorner1X = (int)regionDetails.Attribute("corner1_x"),
                            GCorner1Y = (int)regionDetails.Attribute("corner1_y"),
                            GCorner2X = (int)regionDetails.Attribute("corner2_x"),
                            GCorner2Y = (int)regionDetails.Attribute("corner2_y"),
                        };

                        t2 = await CreateClippedMediaModel(t3).ConfigureAwait(false);
                    }

                    // Get remaining fields
                    t2.GAttributeRefCollection = GetAttributeCollection(theLoadORElement);
                    t2.GCitationRefCollection = GetCitationCollection(theLoadORElement);
                    t2.GNoteRefCollection = GetNoteCollection(theLoadORElement);

                    // TODO !ELEMENT objref (region?, attribute*, citationref*, noteref*)&gt;
                    // !ATTLIST objref hlink IDREF #REQUIRED priv (0|1) #IMPLIED
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.SortAndSetFirst();

            return t;
        }

        private HLinkPersonNameModelCollection GetPersonNameCollection(XElement xmlData)
        {
            HLinkPersonNameModelCollection t = new HLinkPersonNameModelCollection();

            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "name")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    // TODO is date handling correct
                    PersonNameModel newPersonNameModel = new PersonNameModel
                    {
                        Handle = "PersonNameCollection",

                        GCitationRefCollection = GetCitationCollection(theLoadORElement),

                        GDate = SetDate(theLoadORElement),

                        GDisplay = GetElement(theLoadORElement, "display"),

                        GFamilyNick = GetElement(theLoadORElement, "familynick"),

                        GFirstName = GetElement(theLoadORElement, "first"),

                        GGroup = GetElement(theLoadORElement, "group"),

                        GNick = GetElement(theLoadORElement, "nick"),

                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GSort = GetElement(theLoadORElement, "sort"),

                        GSuffix = GetElement(theLoadORElement, "suffix"),

                        GSurName = GetSurnameCollection(theLoadORElement),

                        GTitle = GetElement(theLoadORElement, "title"),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GNoteReferenceCollection = GetNoteCollection(theLoadORElement),
                    };

                    newPersonNameModel.GAlt.SetAlt(GetAttribute(theLoadORElement, "alt"));

                    // Set model hlinkkey
                    newPersonNameModel.HLinkKey = Guid.NewGuid().ToString();
                    DataStore.DS.PersonNameData.Add(newPersonNameModel);

                    // Create a HLink to the model
                    HLinkPersonNameModel newHlink = new HLinkPersonNameModel
                    {
                        HLinkKey = newPersonNameModel.HLinkKey
                    };

                    t.Add(newHlink);
                }
            }

            // Do not sort as the source file order is the one the creator wanted

            return t;
        }

        private HLinkPersonRefModelCollection GetPersonRefCollection(XElement xmlData)
        {
            HLinkPersonRefModelCollection t = new HLinkPersonRefModelCollection();

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "personref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load person object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkPersonRefModel t2 = new HLinkPersonRefModel
                    {
                        GCitationCollection = GetCitationCollection(theLoadORElement),
                        GNoteCollection = GetNoteCollection(theLoadORElement),
                        GRelationship = GetAttribute(theLoadORElement, "rel"),
                    };

                    t2.HLinkKey = GetAttribute(theLoadORElement, "hlink");

                    t.Add(t2);
                }
            }

            return t;
        }

        private PlaceNameModelCollection GetPlaceNameModelCollection(XElement xmlData)
        {
            PlaceNameModelCollection t = new PlaceNameModelCollection();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "pname")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    PlaceNameModel newAttributeModel = new PlaceNameModel
                    {
                        Handle = "PlaceNameModel",

                        GValue = (string)theLoadORElement.Attribute("value"),

                        GLang = (string)theLoadORElement.Attribute("lang"),

                        GDate = GetDate(theLoadORElement),
                    };

                    newAttributeModel.HomeImageHLink.HomeSymbol = CommonConstants.IconPlace;

                    t.Add(newAttributeModel);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private HLinkPlaceModelCollection GetPlaceRefCollection(XElement xmlData)
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection();

            // Load NoteRefs
            var localPlaceElement =
                             from ElementEl in xmlData.Elements(ns + "placeref")
                             select ElementEl;

            if (localPlaceElement.Any())
            {
                // load note references
                foreach (XElement loadPlaceElement in localPlaceElement)
                {
                    HLinkPlaceModel noteHLink = new HLinkPlaceModel
                    {
                        // object details
                        HLinkKey = GetAttribute(loadPlaceElement.Attribute("hlink")),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the repository collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// HLink Repository Model Collection.
        /// </returns>
        private HLinkRepositoryModelCollection GetRepositoryCollection(XElement xmlData)
        {
            HLinkRepositoryModelCollection t = new HLinkRepositoryModelCollection();

            var theERElement = from _ORElementEl in xmlData.Elements(ns + "reporef")
                               select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkRepositoryModel t2 = new HLinkRepositoryModel
                    {
                        // "callno" Done "medium" Done; "noteref" Done
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),

                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GCallNo = GetAttribute(theLoadORElement.Attribute("callno")),

                        GMedium = GetAttribute(theLoadORElement.Attribute("medium")),

                        GNoteRef = GetNoteCollection(theLoadORElement),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the source attribute collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private OCSrcAttributeModelCollection GetSrcAttributeCollection(XElement xmlData)
        {
            OCSrcAttributeModelCollection t = new OCSrcAttributeModelCollection();

            var theERElement =
                    from oRElementEl in xmlData.Elements(ns + "srcattribute")
                    select oRElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SrcAttributeModel tt = new SrcAttributeModel
                    {
                        GPriv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),
                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(tt);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private StyledTextModelCollection GetStyledTextCollection(XElement xmlData)
        {
            StyledTextModelCollection t = new StyledTextModelCollection();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "style")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    StyledTextModel newStyleModel = new StyledTextModel
                    {
                        Handle = "StyledTextCollection",

                        GStyle = GetTextStyle(theLoadORElement),

                        //GCitationReferenceCollection = GetCitationCollection(theLoadORElement),

                        //GNoteModelReferenceCollection = GetNoteCollection(theLoadORElement),

                        //Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        //GType = GetAttribute(theLoadORElement.Attribute("type")),

                        //GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(newStyleModel);
                }
            }

            return t;
        }

        private SurnameModelCollection GetSurnameCollection(XElement xmlData)
        {
            SurnameModelCollection t = new SurnameModelCollection();

            var theERElement = from _ORElementEl in xmlData.Elements(ns + "surname")
                               select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SurnameModel t2 = new SurnameModel
                    {
                        GText = GetElement(theLoadORElement),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Gets the tag collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// </returns>
        private HLinkTagModelCollection GetTagCollection(XElement xmlData)
        {
            HLinkTagModelCollection t = new HLinkTagModelCollection();

            var theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "tagref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load tag references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkTagModel t2 = new HLinkTagModel
                    {
                        HLinkKey = GetAttribute(theLoadORElement.Attribute("hlink")),
                    };

                    t.Add(t2);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        /// <summary>
        /// Load zero or more url content xml elements into alist of url models.
        /// </summary>
        /// <param name="xmlData">
        /// the xElement containing the url references.
        /// </param>
        private OCURLModelCollection GetURLCollection(XElement xmlData)
        {
            OCURLModelCollection t = new OCURLModelCollection();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "url")
                    select orElementEl;

            if (theERElement.Any())
            {
                HLinkHomeImageModel newHomeLink = new HLinkHomeImageModel
                {
                    HomeImageType = CommonEnums.HomeImageType.Symbol,
                    HomeSymbol = CommonConstants.IconBookMark // TODO  Windows.UI.Xaml.Controls.Symbol.World,
                };

                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    URLModel tt = new URLModel
                    {
                        Handle = "URL Collection",

                        HomeImageHLink = newHomeLink,

                        Priv = SetPrivateObject(GetAttribute(theLoadORElement.Attribute("priv"))),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GHRef = GetUri(GetAttribute(theLoadORElement.Attribute("href"))),

                        GDescription = GetAttribute(theLoadORElement.Attribute("description")),
                    };

                    t.Add(tt);
                }
            }

            // Return sorted by the default text
            t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }
    }
}