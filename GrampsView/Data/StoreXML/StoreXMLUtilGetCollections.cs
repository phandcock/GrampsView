// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common;
using GrampsView.Common.CustomClasses;
using GrampsView.Data.Collections;
using GrampsView.Data.Model;
using GrampsView.Data.Repository;
using GrampsView.Data.StoreXML;
using GrampsView.Models.Collections.HLinks;
using GrampsView.Models.DataModels.Minor;
using GrampsView.Models.HLinks.Models;

using SharedSharp.Errors;

using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary>
    /// Various utility and loading routines for XML data.
    /// </summary>
    /// <seealso cref="IStoreXML"/>
    public partial class StoreXML : IStoreXML
    {
        private static HLinkPlaceLocationCollection GetPlaceLocationModelCollection(XElement xmlData)
        {
            HLinkPlaceLocationCollection t = new()
            {
                Title = "Place Location Collection"
            };

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "location")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load location object references
                foreach (XElement theLocation in theERElement)
                {
                    PlaceLocationModel newLocationModel = new();

                    // Load individual attributes
                    IEnumerable<XAttribute> attributeElements = theLocation.Attributes();

                    foreach (XAttribute theLoadORElement in attributeElements)
                    {
                        switch (theLoadORElement.Name.LocalName)
                        {
                            case "city":
                                {
                                    newLocationModel.GCity = theLoadORElement.Value;
                                    break;
                                }
                            case "country":
                                {
                                    newLocationModel.GCountry = theLoadORElement.Value;
                                    break;
                                }
                            case "county":
                                {
                                    newLocationModel.GCounty = theLoadORElement.Value;
                                    break;
                                }
                            case "locality":
                                {
                                    newLocationModel.GLocality = theLoadORElement.Value;
                                    break;
                                }
                            case "parish":
                                {
                                    newLocationModel.GParish = theLoadORElement.Value;
                                    break;
                                }
                            case "phone":
                                {
                                    newLocationModel.GPhone = theLoadORElement.Value;
                                    break;
                                }
                            case "postal":
                                {
                                    newLocationModel.GPostal = theLoadORElement.Value;
                                    break;
                                }
                            case "state":
                                {
                                    newLocationModel.GState = theLoadORElement.Value;
                                    break;
                                }
                            case "street":
                                {
                                    newLocationModel.GStreet = theLoadORElement.Value;
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }
                    }

                    newLocationModel.ModelItemGlyph.Symbol = Constants.IconPlace;

                    HLinkPlaceLocationModel newHLink = new()
                    {
                        DeRef = newLocationModel,
                    };

                    t.Add(newHLink);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.ToString());
            return t;
        }

        private static HLinkPlaceNameModelCollection GetPlaceNameModelCollection(XElement xmlData)
        {
            HLinkPlaceNameModelCollection t = new()
            {
                Title = "Place Name Collection"
            };

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "pname")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    PlaceNameModel newPlaceNameModel = new()
                    {
                        GValue = GetAttribute(theLoadORElement, "value"),

                        GLang = GetAttribute(theLoadORElement, "lang"),

                        GDate = GetDate(theLoadORElement),
                    };

                    newPlaceNameModel.ModelItemGlyph.Symbol = Constants.IconPlace;
                    newPlaceNameModel.HLinkKey = HLinkKey.NewAsGUID();

                    HLinkPlaceNameModel tt = new()
                    {
                        DeRef = newPlaceNameModel
                    };

                    if (string.IsNullOrEmpty(newPlaceNameModel.GValue))
                    {
                    }

                    t.Add(tt);
                }
            }

            return t;
        }

        private HLinkAddressModelCollection GetAddressCollection(XElement xmlData)
        {
            HLinkAddressModelCollection t = new()
            {
                Title = "Address Collection"
            };

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "address")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load address object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AddressModel newAddressModel = new()
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

                        Priv = GetPrivateObject(theLoadORElement),

                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                        // Set model hlinkkey etc
                        HLinkKey = HLinkKey.NewAsGUID()
                    };
                    newAddressModel.Id = newAddressModel.HLinkKey.Value;
                    DataStore.Instance.DS.AddressData.Add(newAddressModel);

                    // Create a HLink to the model
                    HLinkAdressModel newHlink = new()
                    {
                        HLinkKey = newAddressModel.HLinkKey,
                    };

                    t.Add(newHlink);
                }
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
        private HLinkAttributeModelCollection GetAttributeCollection(XElement xmlData)
        {
            HLinkAttributeModelCollection t = new()
            {
                Title = "Attribute Collection"
            };
            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "attribute")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    AttributeModel newAttributeModel = new()
                    {
                        GCitationReferenceCollection = GetCitationCollection(theLoadORElement),

                        GNoteModelReferenceCollection = GetNoteCollection(theLoadORElement),

                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    HLinkAttributeModel tt = new()
                    {
                        DeRef = newAttributeModel
                    };

                    t.Add(tt);
                }
            }

            return t;
        }

        private HLinkChildRefCollection GetChildRefCollection(XElement xmlData)
        {
            HLinkChildRefCollection t = new()
            {
                Title = "Child Reference Collection"
            };

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "childref")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkChildRefModel newChildRefModel = new()
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement),

                        GFatherRel = GetAttribute(theLoadORElement.Attribute("frel")),

                        Priv = GetPrivateObject(theLoadORElement),

                        GMotherRel = GetAttribute(theLoadORElement.Attribute("mrel")),

                        GCitationCollectionReference = GetCitationCollection(theLoadORElement),

                        GNoteCollectionReference = GetNoteCollection(theLoadORElement),
                    };

                    // Force glyph valid while loading
                    newChildRefModel.HLinkGlyphItem.ImageType = CommonEnums.HLinkGlyphType.TempLoading;

                    t.Add(newChildRefModel);
                }
            }

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
            HLinkCitationModelCollection t = new()
            {
                Title = "Citation Collection"
            };

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "citationref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load citation object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkCitationModel t2 = new()
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement),
                    };

                    t.Add(t2);

                    if (t2.DeRef.Id == "C0525")
                    {
                    }
                }
            }

            // Do not sort but accept input Citation order

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
            HLinkEventModelCollection t = new()
            {
                Title = "Event Collection"
            };

            IEnumerable<XElement> theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "eventref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkEventModel t2 = new()
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement),
                        GRole = GetAttribute(theLoadORElement.Attribute("role")),
                        GAttributeRefCollection = GetAttributeCollection(theLoadORElement),
                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                    };

                    t2.GAttributeRefCollection.Title = "HLink Attributes";
                    t2.GNoteRefCollection.Title = "HLink Notes";

                    t.Add(t2);
                }
            }

            // Return sorted by the default text t.SortAndSetFirst();

            return t;
        }

        private HLinkLdsOrdModelCollection GetLDSOrdCollection(XElement xmlData)
        {
            HLinkLdsOrdModelCollection t = new()
            {
                Title = "LDS Ordination Collection"
            };

            IEnumerable<XElement> theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "ldsord")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load LDS Ordination object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    LdsOrdModel t2 = new()
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement),
                    };

                    HLinkLDSModel ttt = new()
                    {
                        DeRef = t2
                    };

                    t.Add(ttt);
                }
            }

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
            HLinkNoteModelCollection t = new()
            {
                Title = "Note Collection"
            };

            // Load NoteRefs
            IEnumerable<XElement> localNoteElement =
                             from ElementEl in xmlData.Elements(ns + "noteref")
                             select ElementEl;

            if (localNoteElement.Any())
            {
                // load note references
                foreach (XElement loadNoteElement in localNoteElement)
                {
                    HLinkNoteModel noteHLink = new()
                    {
                        HLinkKey = GetHLinkKey(loadNoteElement),
                    };

                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text t.SortAndSetFirst();

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
            HLinkMediaModelCollection t = new()
            {
                Title = "Media Collection"
            };

            try
            {
                IEnumerable<XElement> theORElement = from _ORElementEl in xmlData.Elements(ns + "objref")
                                                     select _ORElementEl;

                if (theORElement.Any())
                {
                    // load media object references
                    foreach (XElement theLoadORElement in theORElement)
                    {
                        // save the MediaObject reference
                        HLinkMediaModel outHLMediaModel = new()
                        {
                            HLinkKey = GetHLinkKey(theLoadORElement),
                            Priv = GetPrivateObject(theLoadORElement),
                        };

                        if (outHLMediaModel.HLinkKey.Value == "_f1e1e39ec1c45034e54f3385298")
                        {
                        }

                        // Get region
                        XElement regionDetails = theLoadORElement.Element(ns + "region");
                        if (regionDetails != null)
                        {
                            outHLMediaModel.HLinkGlyphItem.ImageType = CommonEnums.HLinkGlyphType.Image;

                            outHLMediaModel.GCorner1X = (int)regionDetails.Attribute("corner1_x");
                            outHLMediaModel.GCorner1Y = (int)regionDetails.Attribute("corner1_y");
                            outHLMediaModel.GCorner2X = (int)regionDetails.Attribute("corner2_x");
                            outHLMediaModel.GCorner2Y = (int)regionDetails.Attribute("corner2_y");

                            outHLMediaModel = await CreateClippedMediaModel(outHLMediaModel).ConfigureAwait(false);
                        }

                        // Get remaining fields
                        outHLMediaModel.GAttributeRefCollection = GetAttributeCollection(theLoadORElement);
                        outHLMediaModel.GCitationRefCollection = GetCitationCollection(theLoadORElement);
                        outHLMediaModel.GNoteRefCollection = GetNoteCollection(theLoadORElement);

                        t.Add(outHLMediaModel);
                    }
                }
            }
            catch (Exception ex)
            {
                MyNotifications.NotifyException("Load Media Collection", ex);
            }

            return t;
        }

        private HLinkPersonNameModelCollection GetPersonNameCollection(XElement xmlData)
        {
            HLinkPersonNameModelCollection t = new()
            {
                Title = "Person Name Collection"
            };

            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "name")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    // TODO is date handling correct
                    PersonNameModel newPersonNameModel = new()
                    {
                        GCitationRefCollection = GetCitationCollection(theLoadORElement),

                        GDate = SetDate(theLoadORElement),

                        GDisplay = GetElement(theLoadORElement, "display"),

                        GFamilyNick = GetElement(theLoadORElement, "familynick"),

                        GFirstName = GetElement(theLoadORElement, "first"),

                        GGroup = GetElement(theLoadORElement, "group"),

                        GNick = GetElement(theLoadORElement, "nick"),

                        Priv = GetPrivateObject(theLoadORElement),

                        GSort = GetElement(theLoadORElement, "sort"),

                        GSuffix = GetElement(theLoadORElement, "suffix"),

                        GSurName = GetSurnameCollection(theLoadORElement),

                        GTitle = GetElement(theLoadORElement, "title"),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GNoteReferenceCollection = GetNoteCollection(theLoadORElement),
                        GAlt = new AltModel(GetAttribute(theLoadORElement, "alt")),

                        HLinkKey = HLinkKey.NewAsGUID()
                    };

                    DataStore.Instance.DS.PersonNameData.Add(newPersonNameModel);



                    // Create a HLink to the model
                    HLinkPersonNameModel newHlink = new()
                    {
                        HLinkKey = newPersonNameModel.HLinkKey
                    };

                    t.Add(newHlink);
                }
            }

            // Do not sort as the source file order is the one the creator wanted

            return t;
        }

        /// <summary>
        /// Gets the person reference collection.
        /// </summary>
        /// <param name="xmlData">
        /// The XML data.
        /// </param>
        /// <returns>
        /// HLinkPersonRefModelCollection <br/>
        /// </returns>
        private HLinkPersonRefModelCollection GetPersonRefCollection(XElement xmlData)
        {
            HLinkPersonRefModelCollection t = new()
            {
                Title = "Person Collection"
            };

            IEnumerable<XElement> theERElement =
                    from ORElementEl in xmlData.Elements(ns + "personref")
                    select ORElementEl;

            if (theERElement.Any())
            {
                // load person object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkPersonRefModel t2 = new()
                    {
                        Priv = GetPrivateObject(theLoadORElement),
                        GCitationCollection = GetCitationCollection(theLoadORElement),
                        GNoteCollection = GetNoteCollection(theLoadORElement),
                        GRelationship = GetAttribute(theLoadORElement, "rel"),
                        HLinkKey = GetHLinkKey(theLoadORElement)
                    };

                    t.Add(t2);
                }
            }

            return t;
        }

        private HLinkPlaceModelCollection GetPlaceRefCollection(XElement xmlData)
        {
            HLinkPlaceModelCollection t = new()
            {
                Title = "Place Collection"
            };

            // Load NoteRefs
            IEnumerable<XElement> localPlaceElement =
                             from ElementEl in xmlData.Elements(ns + "placeref")
                             select ElementEl;

            if (localPlaceElement.Any())
            {
                // load note references
                foreach (XElement loadPlaceElement in localPlaceElement)
                {
                    HLinkPlaceModel noteHLink = new()
                    {
                        // object details
                        HLinkKey = GetHLinkKey(loadPlaceElement),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

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
        private HLinkRepositoryRefCollection GetRepositoryCollection(XElement xmlData)
        {
            HLinkRepositoryRefCollection t = new()
            {
                Title = "Repository Reference Collection"
            };

            IEnumerable<XElement> theERElement = from _ORElementEl in xmlData.Elements(ns + "reporef")
                                                 select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkRepositoryRefModel t2 = new()
                    {
                        // "callno" Done "medium" Done; "noteref" Done
                        HLinkKey = GetHLinkKey(theLoadORElement),

                        Priv = GetPrivateObject(theLoadORElement),

                        GCallNo = GetAttribute(theLoadORElement.Attribute("callno")),

                        GMedium = GetAttribute(theLoadORElement.Attribute("medium")),

                        GNoteRef = GetNoteCollection(theLoadORElement),
                    };

                    t.Add(t2);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.ToString());

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
        private HLinkOCSrcAttributeCollection GetSrcAttributeCollection(XElement xmlData)
        {
            HLinkOCSrcAttributeCollection t = new()
            {
                Title = "Source Attribute Collection"
            };

            IEnumerable<XElement> theERElement =
                    from oRElementEl in xmlData.Elements(ns + "srcattribute")
                    select oRElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SrcAttributeModel tt = new()
                    {
                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),
                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    HLinkSrcAttributeModel newHLink = new()
                    {
                        DeRef = tt,
                    };

                    t.Add(newHLink);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.ToString());

            return t;
        }

        private StyledTextModel GetStyledTextCollection(XElement xmlData)
        {
            StyledTextModel t = new()
            {
                GText = (string)xmlData.Element(ns + "text")
            };

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "style")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    GrampsStyle newStyleModel = new()
                    {
                        GStyle = GetTextStyle(theLoadORElement),

                        GValue = GetAttribute(theLoadORElement, "Value"),

                        GRange = GetStyledTextRangeCollection(theLoadORElement),
                    };

                    t.Styles.Add(newStyleModel);
                }
            }

            return t;
        }

        private List<GrampsStyleRangeModel> GetStyledTextRangeCollection(XElement xmlData)
        {
            List<GrampsStyleRangeModel> returnValue = new();

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "range")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    GrampsStyleRangeModel newStyleModel = new();

                    if (!int.TryParse(GetAttribute(theLoadORElement, "start"), out int Start))
                    {
                        ErrorInfo t = new("Bad Style Range Start")
                        {
                            { "XML data", xmlData.ToString() }
                        };

                        MyNotifications.NotifyError(t);
                    };
                    newStyleModel.Start = Start;

                    if (!int.TryParse(GetAttribute(theLoadORElement, "end"), out int End))
                    {
                        ErrorInfo t = new("Bad Style Range End")
                        {
                            { "XML data", xmlData.ToString() }
                        };

                        MyNotifications.NotifyError(t);
                    };
                    newStyleModel.End = End;

                    returnValue.Add(newStyleModel);
                }
            }

            return returnValue;
        }

        private HLinkSurnameModelCollection GetSurnameCollection(XElement xmlData)
        {
            HLinkSurnameModelCollection t = new()
            {
                Title = "Surname Collection"
            };

            IEnumerable<XElement> theERElement = from _ORElementEl in xmlData.Elements(ns + "surname")
                                                 select _ORElementEl;

            if (theERElement.Any())
            {
                // load repository references
                foreach (XElement theLoadORElement in theERElement)
                {
                    SurnameModel t2 = new()
                    {
                        GText = GetElement(theLoadORElement),
                    };

                    HLinkSurnameModel newHLink = new()
                    {
                        DeRef = t2,
                    };

                    t.Add(newHLink);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.ToString());

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
            HLinkTagModelCollection t = new()
            {
                Title = "Tag Collection"
            };

            IEnumerable<XElement> theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "tagref")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load tag references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkTagModel t2 = new()
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement),
                    };

                    t.Add(t2);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.ToString());

            return t;
        }

        /// <summary>
        /// Load zero or more url content xml elements into alist of url models.
        /// </summary>
        /// <param name="xmlData">
        /// the xElement containing the url references.
        /// </param>
        private HLinkURLModelCollection GetURLCollection(XElement xmlData)
        {
            HLinkURLModelCollection t = new();

            // Run query
            IEnumerable<XElement> theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "url")
                    select orElementEl;

            if (theERElement.Any())
            {
                foreach (XElement theLoadORElement in theERElement)
                {
                    URLModel tt = new()
                    {
                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GHRef = GetUri(GetAttribute(theLoadORElement.Attribute("href"))),

                        GDescription = GetAttribute(theLoadORElement.Attribute("description")),
                    };

                    tt.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                    tt.ModelItemGlyph.Symbol = Constants.IconURL;

                    tt.HLinkKey = HLinkKey.NewAsGUID();

                    HLinkURLModel ttt = new()
                    {
                        DeRef = tt
                    };

                    t.Add(ttt);
                }
            }

            return t;
        }
    }
}