using System;
using System.Collections;

namespace GrampsView.Data.Model
{
    public interface IEventModel : IModelBase, IComparable, IComparer
    {
        HLinkEventModel HLink
        {
            get;
        }
    }
}