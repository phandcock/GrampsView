namespace GrampsView.Common
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;

    public interface IDataLog
    {
        ObservableCollection<DataLogEntry> DataLoadLog
        {
            get;
        }

        bool DismissFlag
        {
            get; set;
        }

        Task<bool> Add(string entry);

        void Clear();

        Task<bool> Remove();

        Task<bool> Replace(string entry);
    }
}