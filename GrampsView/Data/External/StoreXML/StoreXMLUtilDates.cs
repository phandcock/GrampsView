using GrampsView.Common;
using GrampsView.Data.External.StoreXML;
using GrampsView.Models.DataModels.Date;

using SharedSharp.Errors;
using SharedSharp.Errors.Interfaces;

using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace GrampsView.Data.ExternalStorage
{
    /// <summary> Private Storage Routines. </summary>
    //
    // TODO XML 1.71 check
    public partial class StoreXML : ObservableObject, IStoreXML
    {
        /// <summary>
        /// Sets the date.
        /// </summary>
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
            catch (Exception ex)
            {
                // TODO
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Error in SetDate", ex);

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
                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Date Quality") { { "Element", argCurrentElement.ToString() }, });
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
            catch (Exception ex)
            {
                // TODO
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Error in SetDate", ex);
                throw;
            }

            return new DateObjectModelSpan(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
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
            string aVal = string.Empty;

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
            catch (Exception ex)
            {
                // TODO
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Error", ex);
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
                        ErrorInfo t = new("Bad Date Quality")
                        {
                            { "Current Element",  argCurrentElement.ToString()}
                        };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                    }
                }

                // type CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "type");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    if (!Enum.TryParse(stringFound, out aValType))
                    {
                        ErrorInfo t = new("Bad Date Value")
                        {
                            { "Current Element",  argCurrentElement.ToString()}
                        };

                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(t);
                    }
                }

                // val CDATA #REQUIRED
                stringFound = GetAttribute(argCurrentElement, "val");
                if (!string.IsNullOrEmpty(stringFound))
                {
                    aVal = stringFound;
                }
            }
            catch (Exception ex)
            {
                // TODO
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException(ex.Message, ex);
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
                        Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyError(new ErrorInfo("Bad Date Quality") { { "Element", argCurrentElement.ToString() }, });
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
            catch (Exception ex)
            {
                // TODO
                Ioc.Default.GetRequiredService<IErrorNotifications>().NotifyException("Exception in SetDateRange", ex);
                throw;
            }

            return new DateObjectModelRange(aStart, aStop, aCFormat, aDualDated, aNewYear, aQuality);
        }
    }
}