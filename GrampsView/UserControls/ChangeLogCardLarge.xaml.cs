// <copyright file="PersonCardLarge.xaml.cs" company="MeMyselfAndI">
//     Copyright (c) MeMyselfAndI. All rights reserved.
// </copyright>

namespace GrampsView.UserControls
{
    using GrampsView.Data.Repository;

    using System;
    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public partial class ChangeLogCardLarge : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderCardLarge"/> class.
        /// </summary>
        public ChangeLogCardLarge()
        {
            InitializeComponent();

            try
            {
                // Load Resource
                var assemblyExec = Assembly.GetExecutingAssembly();
                var resourceName = "GrampsView.CHANGELOG.md";

                using (Stream stream = assemblyExec.GetManifestResourceStream(resourceName))
                {
                    if (!(stream is null))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            this.mdview.Markdown = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        DataStore.CN.NotifyError("Error trying to open GrampsView.CHANGELOG.md");
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                DataStore.CN.NotifyException("File not Found Exception trying to open GrampsView.CHANGELOG.md", ex);
            }
        }
    };
}