
/* Unmerged change from project 'GrampsView (net7.0-windows10.0.19041.0)'
Before:
using GrampsView.Models.DataModels.Date;
After:
using GrampsView.Data.Model;
using GrampsView.Models.DataModels.Date;
*/
using GrampsView.Data.Model;

using SharedSharp.Models;

namespace GrampsView.Models.
/* Unmerged change from project 'GrampsView (net7.0-windows10.0.19041.0)'
Before:
namespace GrampsView.Data.Model
After:
namespace GrampsView.Models.DataModels.Date.Interfaces
*/
DataModels.Date.Interfaces
{
    /// <summary>
    /// Public interfaces for the DateObject elements.
    /// </summary>
    public interface IDateObjectModel : IModelBase, IComparable<DateObjectModel>, IComparer<DateObjectModel>
    {
        int? GetAge
        {
            get;
        }

        string GetDecade
        {
            get;
        }

        string GetMonthDay
        {
            get;
        }

        string GetYear
        {
            get;
        }

        string LongDate
        {
            get;
        }

        string ShortDate
        {
            get;
        }

        string ShortDateOrEmpty
        {
            get;
        }

        DateTime SingleDate
        {
            get;
        }

        DateTime SortDate
        {
            get;
        }

        new bool Valid
        {
            get; set;
        }

        bool ValidDay
        {
            get; set;
        }

        bool ValidMonth
        {
            get; set;
        }

        bool ValidYear
        {
            get; set;
        }

        CardListLineCollection AsCardListLine(string? argTitle = null);

        TimeSpan DateDifference(IDateObjectModel otherDate);

        string DateDifferenceDecoded(IDateObjectModel otherDate);
    }
}