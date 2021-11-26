namespace GrampsView.UserControls
{
    using Microsoft.Extensions.DependencyInjection;

    using SharedSharp.Errors;

    using System.IO;
    using System.Reflection;

    using Xamarin.Forms;

    public partial class FileViewCardLarge : Grid
    {
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
                            App.Current.Services.GetService<IErrorNotifications>().NotifyError(new ErrorInfo("Error trying to open resource") { { "Resource Name", resourceName }, });
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    App.Current.Services.GetService<IErrorNotifications>().NotifyException("File not Found Exception trying to open " + resourceName, ex);
                }
            }
        }
    };
}