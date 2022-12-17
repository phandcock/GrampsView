using GrampsView.Models.DataModels.Date.Interfaces;

namespace GrampsView.Data.Model
{
    public interface IDateObjectModelStr : IDateObjectModel
    {
        string GVal { get; }
    }
}