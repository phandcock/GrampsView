//-----------------------------------------------------------------------
//
// Storage routines for the GrampsStoreXML
//
// <copyright file="GrampsStoreXMLDates.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Xml.Linq;

    /// <summary> Private Storage Routines. </summary> <code> <define name="date-content">
    // <choice> <element name = "daterange" > < attribute name="start"><text/></attribute>
    // <attribute name = "stop" >< text /></ attribute > < optional >< attribute
    // name="quality"><choice> <value>estimated</value> <value>calculated</value>
    // </choice></attribute></optional> <optional><attribute name = "cformat" >< text /></ attribute
    // ></ optional > < optional >< attribute name="dualdated">
    // <choice><value>0</value><value>1</value></choice> </attribute></optional>
    // <optional><attribute name = "newyear" >< text /></ attribute ></ optional > </ element > <
    // element name="datespan"> <attribute name = "start" >< text /></ attribute > < attribute
    // name="stop"><text/></attribute> <optional><attribute name = "quality" >< choice > < value >
    // estimated </ value > < value > calculated </ value > </ choice ></ attribute ></ optional > <
    // optional >< attribute name="cformat"><text/></attribute></optional> <optional><attribute name
    // = "dualdated" > < choice >< value > 0 </ value >< value > 1 </ value ></ choice > </ attribute
    // ></ optional > < optional >< attribute name="newyear"><text/></attribute></optional>
    // </element> <element name = "dateval" > < attribute name="val"><text/></attribute>
    // <optional><attribute name = "cformat" >< text /></ attribute ></ optional > < optional ><
    // attribute name="type"><choice> <value>before</value> <value>after</value>
    // <value>about</value> </choice></attribute></optional> <optional><attribute name = "quality"
    // >< choice > < value > estimated </ value > < value > calculated </ value > </ choice ></
    // attribute ></ optional > < optional >< attribute name="dualdated">
    // <choice><value>0</value><value>1</value></choice> </attribute></optional>
    // <optional><attribute name = "newyear" >< text /></ attribute ></ optional > </ element > <
    // element name="datestr"> <attribute name = "val" >< text /></ attribute > </ element > </
    // choice > </ define > </ code >
    public partial class GrampsStoreXML : CommonBindableBase, IGrampsStoreXML
    {
        /// <summary>
        /// Sets the date string.
        /// </summary>
        /// <param name="currentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        public static DateObjectModelStr SetDateStr(XElement currentElement)
        {
            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            string aQuality = string.Empty;
            string aStart = string.Empty;
            string aStop = string.Empty;
            string aVal = string.Empty;
            string aValType = string.Empty;

            // check for date range
            try
            {
                // val CDATA #REQUIRED
                string stringFound = (string)currentElement.Attribute("val");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aVal = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error", e);
                throw;
            }

            return new DateObjectModelStr(aVal);
        }

        /// <summary>
        /// Sets the date.
        /// </summary>
        /// <param name="currentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// Date object ViewModel.
        /// </returns>
        public DateObjectModel SetDate(XElement currentElement)
        {
            XElement tempElement;

            // check for date range
            try
            {
                tempElement = currentElement.Element(ns + "daterange");
                if (tempElement != null)
                {
                    return SetDateRange(tempElement);
                }

                // check for date span
                tempElement = currentElement.Element(ns + "datespan");
                if (tempElement != null)
                {
                    return SetDateSpan(tempElement);
                }

                // check for date val
                tempElement = currentElement.Element(ns + "dateval");
                if (tempElement != null)
                {
                    return SetDateVal(tempElement);
                }

                // check for datestr
                tempElement = currentElement.Element(ns + "datestr");
                if (tempElement != null)
                {
                    DateObjectModelStr t = SetDateStr(tempElement);

                    return t;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error in SetDate", e);

                throw;
            }

            // TODO fix this if (tempDate is typeof( DateObjectModel) ) { // no date found tempDate
            // = null; }
            return new DateObjectModelVal();
        }

        /// <summary>
        /// Sets the date span.
        /// </summary>
        /// <param name="argCurrentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        public DateObjectModel SetDateSpan(XElement argCurrentElement)
        {
            if (argCurrentElement is null)
            {
                throw new ArgumentNullException(nameof(argCurrentElement));
            }

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            string aQuality = string.Empty;
            string aStart = string.Empty;
            string aStop = string.Empty;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                string stringFound = (string)argCurrentElement.Attribute("cformat");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aCFormat = stringFound;
                }

                // dualdated value #REQUIRED
                boolFound = GetBool(argCurrentElement, "dualdated");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aDualDated = boolFound;
                }

                // newyear CDATA #IMPLIED
                stringFound = (string)argCurrentElement.Attribute("newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = (string)argCurrentElement.Attribute("quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aQuality = stringFound;
                }

                // start CDATA #REQUIRED
                stringFound = (string)argCurrentElement.Attribute("start");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStart = stringFound;
                }

                // stop CDATA #REQUIRED
                stringFound = (string)argCurrentElement.Attribute("stop");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStop = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Error in SetDate", e);
                throw;
            }

            return new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        /// <summary>
        /// Sets the date value.
        /// </summary>
        /// <param name="currentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        public DateObjectModel SetDateVal(XElement currentElement)
        {
            Contract.Requires(currentElement != null);

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            string aQuality = string.Empty;
            string aStart = string.Empty;
            string aStop = string.Empty;
            string aVal = string.Empty;
            string aValType = string.Empty;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                string stringFound = (string)currentElement.Attribute("cformat");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aCFormat = stringFound;
                }

                // dualdated value #REQUIRED
                boolFound = GetBool(currentElement, "dualdated");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aDualDated = boolFound;
                }

                // newyear CDATA #IMPLIED
                stringFound = (string)currentElement.Attribute("newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aQuality = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("type");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aValType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(stringFound);
                }

                // val CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("val");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aVal = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException(e.Message, e);
                throw;
            }

            return new DateObjectModelVal(aVal);
        }

        /// <summary>
        /// Sets the date range.
        /// </summary>
        /// <param name="currentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        private DateObjectModel SetDateRange(XElement currentElement)
        {
            string stringFound;

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            string aQuality = string.Empty;
            string aStart = string.Empty;
            string aStop = string.Empty;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("cformat");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aCFormat = stringFound;
                }

                // dualdated value #REQUIRED
                boolFound = GetBool(currentElement, "dualdated");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aDualDated = boolFound;
                }

                // newyear CDATA #IMPLIED
                stringFound = (string)currentElement.Attribute("newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aQuality = stringFound;
                }

                // start CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("start");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStart = stringFound;
                }

                // stop CDATA #REQUIRED
                stringFound = (string)currentElement.Attribute("stop");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStop = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.CN.NotifyException("Exception in SetDateRange", e);
                throw;
            }

            return new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }
    }
}