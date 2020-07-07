//-----------------------------------------------------------------------
//
// Common routines for the CommonRoutines
//
// <copyright file="CommonRoutines.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Common
{
    using System.Text.RegularExpressions;

    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// Various common routines.
    /// </summary>

    public static class CommonRoutines
    {
        public static Color GetResourceColour(string argColourResourceName)
        {
            // Get colour
            Application.Current.Resources.TryGetValue(argColourResourceName, out var varCardColour);
            return (Color)varCardColour;
        }

        public static bool IsEmulator()
        {
            if (DeviceInfo.DeviceType == DeviceType.Virtual)
            {
                return true;
            }

            return false;
        }

        public static string ReplaceLineSeperators(string argString)
        {
            return Regex.Replace(argString, @"[\u000A\u000B\u000C\u000D\u2028\u2029\u0085]+", "");
        }

        public static Color ResourceColourGet(string keyName)
        {
            var t = ResourceValueGet(keyName);

            if (t is null)
            {
                return Color.White;
            }

            return (Color)t;
        }

        public static object ResourceValueGet(string keyName)
        {
            // Search all dictionaries
            if (Application.Current.Resources.TryGetValue(keyName, out var retVal)) { }
            return retVal;
        }
    }
}