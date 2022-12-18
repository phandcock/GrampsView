using System;
using System.Collections;
using System.ComponentModel;

namespace GrampsView.Data.Model
{
    /// <summary>
    /// </summary>
    /// 
    public interface IAttributeModel : IModelBase, IComparable<AttributeModel>, INotifyPropertyChanged, IComparable, IComparer
    {
    }
}