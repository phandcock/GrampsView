namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
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
    /// <seealso cref="GrampsView.Data.ExternalStorage.IStoreXML"/>
    /// <seealso cref="IStoreXML"/>
    public partial class StoreXML : IStoreXML
    {
        private static List<GrampsStyleRangeModel> GetStyledTextRangeCollection(XElement xmlData)
        {
            List<GrampsStyleRangeModel> returnValue = new List<GrampsStyleRangeModel>();

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "range")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    GrampsStyleRangeModel newStyleModel = new GrampsStyleRangeModel();

                    if (!int.TryParse(GetAttribute(theLoadORElement, "start"), out int Start))
                    {
                        ErrorInfo t = new ErrorInfo("Bad Style Range Start")
                        {
                            { "XML data", xmlData.ToString() }
                        };

                        DataStore.Instance.CN.NotifyError(t);
                    };
                    newStyleModel.Start = Start;

                    if (!int.TryParse(GetAttribute(theLoadORElement, "end"), out int End))
                    {
                        ErrorInfo t = new ErrorInfo("Bad Style Range End")
                        {
                            { "XML data", xmlData.ToString() }
                        };

                        DataStore.Instance.CN.NotifyError(t);
                    };
                    newStyleModel.End = End;

                    returnValue.Add(newStyleModel);
                }
            }

            return returnValue;
        }

        private static SurnameModelCollection GetSurnameCollection(XElement xmlData)
        {
            SurnameModelCollection t = new SurnameModelCollection
            {
                Title = "Surname Collection"
            };

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

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private HLinkOCAddressModelCollection GetAddressCollection(XElement xmlData)
        {
            HLinkOCAddressModelCollection t = new HLinkOCAddressModelCollection
            {
                Title = "Address Collection"
            };

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

                        Priv = GetPrivateObject(theLoadORElement),

                        GNoteRefCollection = GetNoteCollection(theLoadORElement),
                    };

                    // Set model hlinkkey etc
                    string newGuid = Guid.NewGuid().ToString();
                    newAddressModel.HLinkKey = new HLinkKey(newGuid);
                    newAddressModel.Id = newGuid;
                    newAddressModel.Handle = newGuid;
                    DataStore.Instance.DS.AddressData.Add(newAddressModel);

                    // Create a HLink to the model
                    HLinkAdressModel newHlink = new HLinkAdressModel
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
        private OCAttributeModelCollection GetAttributeCollection(XElement xmlData)
        {
            OCAttributeModelCollection t = new OCAttributeModelCollection
            {
                Title = "Attribute Collection"
            };
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
                        GCitationReferenceCollection = GetCitationCollection(theLoadORElement),

                        GNoteModelReferenceCollection = GetNoteCollection(theLoadORElement),

                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(newAttributeModel);
                }
            }

            // TODO Need to sort Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private ChildRefCollectionCollection GetChildRefCollection(XElement xmlData)
        {
            ChildRefCollectionCollection t = new ChildRefCollectionCollection
            {
                Title = "Child Reference Collection"
            };

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "childref")
                    select orElementEl;

            if (theERElement.Any())
            {
                // Load attribute object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    HLinkChildRefModel newChildRefModel = new HLinkChildRefModel
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),

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
            HLinkCitationModelCollection t = new HLinkCitationModelCollection
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
                    HLinkCitationModel t2 = new HLinkCitationModel
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),
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
            HLinkEventModelCollection t = new HLinkEventModelCollection
            {
                Title = "Event Collection"
            };

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
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),
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

        private OCLdsOrdModelCollection GetLDSOrdCollection(XElement xmlData)
        {
            //TODO Fix

            OCLdsOrdModelCollection t = new OCLdsOrdModelCollection
            {
                Title = "LDS Ordination Collection"
            };

            var theERElement =
                    from _ORElementEl in xmlData.Elements(ns + "ldsord")
                    select _ORElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    LdsOrdModel t2 = new LdsOrdModel
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),
                    };

                    t.Add(t2);
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
            HLinkNoteModelCollection t = new HLinkNoteModelCollection
            {
                Title = "Note Collection"
            };

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
                        HLinkKey = GetHLinkKey(loadNoteElement.Attribute("hlink")),
                    };

                    // save the object
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
            HLinkMediaModelCollection t = new HLinkMediaModelCollection
            {
                Title = "Media Collection"
            };

            var theORElement = from _ORElementEl in xmlData.Elements(ns + "objref")
                               select _ORElementEl;

            if (theORElement.Any())
            {
                // load media object references
                foreach (XElement theLoadORElement in theORElement)
                {
                    // save the MediaObject reference
                    HLinkMediaModel outHLMediaModel = new HLinkMediaModel
                    {
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),
                    };

                    if (outHLMediaModel.HLinkKey.Value == "_ea97612787a7a61ff4c3177b8b0")
                    {
                    }

                    // Get region
                    XElement regionDetails = theLoadORElement.Element(ns + "region");
                    if (regionDetails != null)
                    {
                        HLinkLoadImageModel tempHLMediaModel = new HLinkLoadImageModel
                        {
                            HLinkKey = outHLMediaModel.HLinkKey,
                            ImageType = CommonEnums.HLinkGlyphType.Image,

                            GCorner1X = (int)regionDetails.Attribute("corner1_x"),
                            GCorner1Y = (int)regionDetails.Attribute("corner1_y"),
                            GCorner2X = (int)regionDetails.Attribute("corner2_x"),
                            GCorner2Y = (int)regionDetails.Attribute("corner2_y"),
                        };

                        outHLMediaModel = await CreateClippedMediaModel(tempHLMediaModel).ConfigureAwait(false);
                    }

                    // Get remaining fields
                    outHLMediaModel.GAttributeRefCollection = GetAttributeCollection(theLoadORElement);
                    outHLMediaModel.GCitationRefCollection = GetCitationCollection(theLoadORElement);
                    outHLMediaModel.GNoteRefCollection = GetNoteCollection(theLoadORElement);

                    // TODO !ELEMENT objref (region?, attribute*, citationref*, noteref*)&gt;
                    // !ATTLIST objref hlink IDREF #REQUIRED priv (0|1) #IMPLIED
                    t.Add(outHLMediaModel);
                }
            }

            // Return sorted by the default text t.SortAndSetFirst();

            return t;
        }

        private HLinkPersonNameModelCollection GetPersonNameCollection(XElement xmlData)
        {
            HLinkPersonNameModelCollection t = new HLinkPersonNameModelCollection
            {
                Title = "Person Name Collection"
            };

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

                        Priv = GetPrivateObject(theLoadORElement),

                        GSort = GetElement(theLoadORElement, "sort"),

                        GSuffix = GetElement(theLoadORElement, "suffix"),

                        GSurName = GetSurnameCollection(theLoadORElement),

                        GTitle = GetElement(theLoadORElement, "title"),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GNoteReferenceCollection = GetNoteCollection(theLoadORElement),
                    };

                    newPersonNameModel.GAlt = new AltModel(GetAttribute(theLoadORElement, "alt"));

                    // Set model hlinkkey
                    newPersonNameModel.HLinkKey = new HLinkKey(Guid.NewGuid().ToString());

                    DataStore.Instance.DS.PersonNameData.Add(newPersonNameModel);

                    //var tt = (DataStore.Instance.DS.PersonNameData.Where(x => x.Value.GSurName.GetPrimarySurname == "Ainger"));
                    //if (tt.Count() > 0)
                    //{
                    //}

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
            HLinkPersonRefModelCollection t = new HLinkPersonRefModelCollection
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
                    HLinkPersonRefModel t2 = new HLinkPersonRefModel
                    {
                        GCitationCollection = GetCitationCollection(theLoadORElement),
                        GNoteCollection = GetNoteCollection(theLoadORElement),
                        GRelationship = GetAttribute(theLoadORElement, "rel"),
                    };

                    t2.HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink"));

                    t.Add(t2);
                }
            }

            return t;
        }

        private PlaceLocationCollection GetPlaceLocationModelCollection(XElement xmlData)
        {
            PlaceLocationCollection t = new PlaceLocationCollection
            {
                Title = "Place Location Collection"
            };
            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "location")
                    select orElementEl;
            if (theERElement.Any())
            {
                IEnumerable<XAttribute> attributeElements = theERElement.Attributes();

                // Load attribute object references
                foreach (XAttribute theLoadORElement in attributeElements)
                {
                    PlaceLocationModel newAttributeModel = new PlaceLocationModel();

                    switch (theLoadORElement.Name.LocalName)
                    {
                        case "city":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.city;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "country":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.country;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "county":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.county;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "locality":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.locality;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "parish":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.parish;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "phone":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.phone;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "postal":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.postal;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }
                        case "street":
                            {
                                newAttributeModel.GPlaceLocation = CommonEnums.PlaceLocation.street;
                                newAttributeModel.GLocationName = theLoadORElement.Value;
                                break;
                            }

                        default:
                            {
                                break;
                            }
                    }

                    newAttributeModel.ModelItemGlyph.Symbol = CommonConstants.IconPlace;
                    t.Add(newAttributeModel);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);
            return t;
        }

        private PlaceNameModelCollection GetPlaceNameModelCollection(XElement xmlData)
        {
            PlaceNameModelCollection t = new PlaceNameModelCollection
            {
                Title = "Place Name Collection"
            };

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

                    newAttributeModel.ModelItemGlyph.Symbol = CommonConstants.IconPlace;

                    t.Add(newAttributeModel);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private HLinkPlaceModelCollection GetPlaceRefCollection(XElement xmlData)
        {
            HLinkPlaceModelCollection t = new HLinkPlaceModelCollection
            {
                Title = "Place Collection"
            };

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
                        HLinkKey = GetHLinkKey(loadPlaceElement.Attribute("hlink")),
                    };

                    // save the object
                    t.Add(noteHLink);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

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
            HLinkRepositoryModelCollection t = new HLinkRepositoryModelCollection
            {
                Title = "Repository Collection"
            };

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
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),

                        Priv = GetPrivateObject(theLoadORElement),

                        GCallNo = GetAttribute(theLoadORElement.Attribute("callno")),

                        GMedium = GetAttribute(theLoadORElement.Attribute("medium")),

                        GNoteRef = GetNoteCollection(theLoadORElement),
                    };
                    t.Add(t2);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

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
            OCSrcAttributeModelCollection t = new OCSrcAttributeModelCollection
            {
                Title = "Source Attribute Collection"
            };

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
                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),
                        GValue = GetAttribute(theLoadORElement.Attribute("value")),
                    };

                    t.Add(tt);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }

        private StyledTextModel GetStyledTextCollection(XElement xmlData)
        {
            StyledTextModel t = new StyledTextModel
            {
                GText = (string)xmlData.Element(ns + "text")
            };

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
                    GrampsStyle newStyleModel = new GrampsStyle
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
            HLinkTagModelCollection t = new HLinkTagModelCollection
            {
                Title = "Tag Collection"
            };

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
                        HLinkKey = GetHLinkKey(theLoadORElement.Attribute("hlink")),
                    };

                    t.Add(t2);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

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
            OCURLModelCollection t = new OCURLModelCollection
            {
                Title = "URL Collection"
            };

            // Run query
            var theERElement =
                    from orElementEl
                    in xmlData.Elements(ns + "url")
                    select orElementEl;

            if (theERElement.Any())
            {
                // load event object references
                foreach (XElement theLoadORElement in theERElement)
                {
                    URLModel tt = new URLModel
                    {
                        Handle = "URL Collection",

                        Priv = GetPrivateObject(theLoadORElement),

                        GType = GetAttribute(theLoadORElement.Attribute("type")),

                        GHRef = GetUri(GetAttribute(theLoadORElement.Attribute("href"))),

                        GDescription = GetAttribute(theLoadORElement.Attribute("description")),
                    };

                    tt.ModelItemGlyph.ImageType = CommonEnums.HLinkGlyphType.Symbol;

                    tt.ModelItemGlyph.Symbol = CommonConstants.IconURL;

                    t.Add(tt);
                }
            }

            // Return sorted by the default text t.Sort(T => T.DeRef.GetDefaultText);

            return t;
        }
    }
}