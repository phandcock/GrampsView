﻿// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Common.CustomClasses;
using GrampsView.Data.DataView;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrampsView.Common
{
    // TODO: Bodgy up our own handler until Shell Search for UWP can handle multiple types in the display
    public class GrampsViewSearchHandler : SearchHandler
    {
        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);
            await Task.Delay(1000);

            await (item as SearcHandlerItem).HLink.UCNavigate();
        }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue) || (newValue.Length < 5))
            {
                ItemsSource = null;
            }
            else
            {
                List<SearcHandlerItem> temp = new List<SearcHandlerItem>();

                // Add people
                foreach (SearcHandlerItem item in DV.PersonDV.SearchShell(newValue))
                {
                    temp.Add(item);
                }

                // Add notes
                foreach (SearcHandlerItem item in DL.NoteDL.SearchShell(newValue))
                {
                    temp.Add(item);
                }

                temp.Sort();

                // add to the display
                ItemsSource = temp;
            }
        }
    }
}