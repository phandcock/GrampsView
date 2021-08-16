namespace GrampsView.Data.ExternalStorage
{
    using GrampsView.Common;
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;
    using System.Diagnostics.Contracts;
    using System.Xml.Linq;

    using Xamarin.CommunityToolkit.ObjectModel;

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
    public partial class StoreXML : ObservableObject, IStoreXML
    {
        /// <summary>
        /// Sets the date.
        /// </summary>
        /// <param name="currentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// Date object ViewModel.
        /// </returns>
        public static DateObjectModel SetDate(XElement argCurrentElement)
        {
            XElement tempElement;

            if (argCurrentElement is null)
            {
                // TODO fix this to return invalid date
                return new DateObjectModelStr(string.Empty);
            }

            // check for date range
            try
            {
                tempElement = argCurrentElement.Element(ns + "daterange");
                if (tempElement != null)
                {
                    return SetDateRange(tempElement);
                }

                // check for date span
                tempElement = argCurrentElement.Element(ns + "datespan");
                if (tempElement != null)
                {
                    return SetDateSpan(tempElement);
                }

                // check for date val
                tempElement = argCurrentElement.Element(ns + "dateval");
                if (tempElement != null)
                {
                    return SetDateVal(tempElement);
                }

                // check for datestr
                tempElement = argCurrentElement.Element(ns + "datestr");
                if (tempElement != null)
                {
                    DateObjectModelStr t = SetDateStr(tempElement);

                    return t;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.Instance.CN.NotifyException("Error in SetDate", e);

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
        public static DateObjectModel SetDateSpan(XElement argCurrentElement)
        {
            if (argCurrentElement is null)
            {
                throw new ArgumentNullException(nameof(argCurrentElement));
            }

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = string.Empty;
            string aStop = string.Empty;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                string stringFound = GetAttribute(argCurrentElement, "cformat");
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
                stringFound = GetAttribute(argCurrentElement, "newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    if (!Enum.TryParse(stringFound, out aQuality))
                    {
                        DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Date Quality") { { "Element", argCurrentElement.ToString() }, });
                    }
                }

                // start CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "start");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStart = stringFound;
                }

                // stop CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "stop");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStop = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.Instance.CN.NotifyException("Error in SetDate", e);
                throw;
            }

            return new DateObjectModelSpan(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }

        /// <summary>
        /// Sets the date string.
        /// </summary>
        /// <param name="argCurrentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        public static DateObjectModelStr SetDateStr(XElement argCurrentElement)
        {
            Contract.Assert(argCurrentElement != null);

            string aCFormat = string.Empty;
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
                string stringFound = GetAttribute(argCurrentElement, "val");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aVal = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.Instance.CN.NotifyException("Error", e);
                throw;
            }

            return new DateObjectModelStr(aVal);
        }

        /// <summary>
        /// Sets the date value.
        /// </summary>
        /// <param name="argCurrentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        public static DateObjectModel SetDateVal(XElement argCurrentElement)
        {
            Contract.Requires(argCurrentElement != null);

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aVal = string.Empty;
            CommonEnums.DateValType aValType = CommonEnums.DateValType.unknown;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                string stringFound = GetAttribute(argCurrentElement, "cformat");
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
                stringFound = GetAttribute(argCurrentElement, "newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    if (!Enum.TryParse(stringFound, out aQuality))
                    {
                        ErrorInfo t = new ErrorInfo("Bad Date Quality")
                        {
                            { "Current Element",  argCurrentElement.ToString()}
                        };

                        DataStore.Instance.CN.NotifyError(t);
                    }
                }

                // type CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "type");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    if (!Enum.TryParse(stringFound, out aValType))
                    {
                        ErrorInfo t = new ErrorInfo("Bad Date Value")
                        {
                            { "Current Element",  argCurrentElement.ToString()}
                        };

                        DataStore.Instance.CN.NotifyError(t);
                    }
                }

                // val CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "val");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aVal = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.Instance.CN.NotifyException(e.Message, e);
                throw;
            }

            return new DateObjectModelVal(aVal, aCFormat, aDualDated, aNewYear, aQuality, aValType);
        }

        /// <summary>
        /// Sets the date range.
        /// </summary>
        /// <param name="argCurrentElement">
        /// The current element.
        /// </param>
        /// <returns>
        /// </returns>
        private static DateObjectModel SetDateRange(XElement argCurrentElement)
        {
            string stringFound;

            string aCFormat = string.Empty;
            bool aDualDated = false;
            string aNewYear = string.Empty;
            CommonEnums.DateQuality aQuality = CommonEnums.DateQuality.unknown;
            string aStart = string.Empty;
            string aStop = string.Empty;

            // check for date range
            try
            {
                bool boolFound = false;

                // cformat CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "cformat");
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
                stringFound = GetAttribute(argCurrentElement, "newyear");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aNewYear = stringFound;
                }

                // type CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "quality");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    if (!Enum.TryParse(stringFound, out aQuality))
                    {
                        DataStore.Instance.CN.NotifyError(new ErrorInfo("Bad Date Quality") { { "Element", argCurrentElement.ToString() }, });
                    }
                }

                // start CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "start");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStart = stringFound;
                }

                // stop CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "stop");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aStop = stringFound;
                }
            }
            catch (Exception e)
            {
                // TODO
                DataStore.Instance.CN.NotifyException("Exception in SetDateRange", e);
                throw;
            }

            return new DateObjectModelRange(aCFormat, aDualDated, aNewYear, aQuality, aStart, aStop);
        }
    }
}