namespace GrampsView.Common
{
    using System.Collections.ObjectModel;

    public interface IDataLog
    {
        ObservableCollection<DataLogEntry> DataLoadLog
        {
            get;
        }

        bool PopupDismissFlag
        {
            get; set;
        }

        void Add(string entry);

        void Clear();

        void Remove();

        void Replace(string entry);
    }
}