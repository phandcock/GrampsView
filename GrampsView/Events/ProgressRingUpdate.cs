//-----------------------------------------------------------------------
//
// Major message event
//
// <copyright file="GVProgressMajorTextUpdate.cs" company="MeMySelfandI">
//     GPL Copyright
// </copyright>
//-----------------------------------------------------------------------

using Prism.Events;

namespace GrampsView.Events
{
    /// <summary>
    /// update the progress ring text
    /// </summary>
    public class ProgressRingUpdate : PubSubEvent<bool>
    {
    }
}