/// <summary>
/// </summary>
namespace GrampsView.Data.Collections
{
    using GrampsView.Common;
    using GrampsView.Data.Model;

    using System.Collections.ObjectModel;
    using System.Runtime.Serialization;

    /// <summary>
    /// Surname model collection.
    /// </summary>
    /// <seealso cref="Collections.ObjectViewModel.ObservableCollection%7BData.ViewModel.AttributeModel%7D">
    /// Collections.ObjectViewModel.ObservableCollection{Data.ViewModel.AttributeModel}
    /// </seealso>
    [CollectionDataContract]
    [KnownType(typeof(ObservableCollection<SurnameModel>))]
    public class SurnameModelCollection : CardGroupBase<SurnameModel>
    {
        public SurnameModelCollection()
        {
            Title = "Surname Model Collection";
        }

        public string GetPrimarySurname
        {
            get
            {
                // TODO Handle multiple surnames

                if ((Items.Count > 0) && (Items[0].Valid))
                {
                    return Items[0].GText;
                }

                return "Unknown";
            }
        }
    }
}