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

    public partial class FileViewCardLarge : Grid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeLogCardLarge"/> class.
        /// </summary>
        public FileViewCardLarge()
        {
            InitializeComponent();
        }

        private void ChangeLogCardLargeRoot_BindingContextChanged(object sender, System.EventArgs e)
        {
            string resourceName = BindingContext as string;

            if (!string.IsNullOrEmpty(resourceName))
            {
                try
                {
                    // Load Resource
                    var assemblyExec = Assembly.GetExecutingAssembly();

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
                            DataStore.Instance.CN.NotifyError("Error trying to open " + resourceName);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    DataStore.Instance.CN.NotifyException("File not Found Exception trying to open " + resourceName, ex);
                }
            }
        }
    };
}