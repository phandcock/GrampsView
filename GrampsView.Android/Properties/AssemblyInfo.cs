using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

using Android.App;

using Xamarin.Forms;

// General Information about an assembly is controlled through the following set of attributes.
// Change these attribute values to modify the information associated with an assembly.
[assembly: AssemblyTitle("GrampsView.Android")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("GrampsView.Android")]
[assembly: AssemblyCopyright("Copyright ©  2010 - 2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: NeutralResourcesLanguage("en")]

// Version information for an assembly consists of the following four values:
//
// Major Version Minor Version Build Number Revision
//
// You can specify all the values or you can default the Build and Revision Numbers by using the '*'
// as shown below: [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.5.71.67")]
[assembly: AssemblyFileVersion("1.5.71.67")]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: AssemblyInformationalVersion("1.5.61")]
//[assembly: ExportFont("materialdesignicons-webfont.ttf", Alias = "MaterialFontFamily")]