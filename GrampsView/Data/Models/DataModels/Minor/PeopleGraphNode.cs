// <copyright file="PeopleGraphNode.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    /// <summary>
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase" />
    /// ///
    /// <seealso cref="System.IComparable" />
    /// ///
    /// <seealso cref="System.Collections.IComparer" />
    [DataContract]
    public sealed class PeopleGraphNode : ModelBase, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleGraphNode" /> class.
        /// </summary>
        public PeopleGraphNode()
        {
        }

        /// <summary>
        /// The node h link.
        /// </summary>
        public HLinkBase NodeHLink
        {
            get; set;
        }
            = new HLinkBase();

        /// <summary>
        /// The x start.
        /// </summary>
        public int XStart { get; set; } = 0;

        /// <summary>
        /// The y start.
        /// </summary>
        public int YStart { get; set; } = 0;

        /// <summary>
        /// Sorts the by y start.
        /// </summary>
        /// <param name="a">
        /// a.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// </returns>
        public static int SortByYStart(PeopleGraphNode a, PeopleGraphNode b)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            if (b is null)
            {
                throw new ArgumentNullException(nameof(b));
            }

            return a.YStart.CompareTo(b.YStart);
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparer.Compare(object a, object b)
        {
            PeopleGraphNode firstEvent = (PeopleGraphNode)a;
            PeopleGraphNode secondEvent = (PeopleGraphNode)b;

            // compare on Priority first
            int testFlag = string.Compare(firstEvent.NodeHLink.HLinkKey, secondEvent.NodeHLink.HLinkKey, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparable.CompareTo(object obj)
        {
            PeopleGraphNode secondEvent = (PeopleGraphNode)obj;

            // compare on Target first
            int testFlag = string.Compare(NodeHLink.HLinkKey, secondEvent.NodeHLink.HLinkKey, StringComparison.CurrentCulture);

            return testFlag;
        }
    }
}