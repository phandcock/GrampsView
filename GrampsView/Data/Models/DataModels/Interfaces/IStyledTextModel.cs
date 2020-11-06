

using GrampsView.Data.Collections;
using System.Collections.ObjectModel;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.IModelBase"/>
    public interface IStyledTextModel 
    {
        ObservableCollection<IGrampsStyle> Styles { get;  }

        string GText { get; set; }
    }
}