// TODO Needs XML 1.71 check

namespace GrampsView.Data.Model
{
    using GrampsView.Common.CustomClasses;
    using GrampsView.Data.Collections;
    using GrampsView.Models.DataModels;

    using System;
    using System.Collections;

    /// <summary>
    /// TODO Update fields as per Schema
    /// </summary>
    /// <seealso cref="GrampsView.Data.ViewModel.ModelBase"/>
    /// ///
    /// <seealso cref="System.IComparable"/>
    /// ///
    /// <seealso cref="System.Collections.IComparer"/>

    public sealed class PeopleGraphNode : IPeopleGraphNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleGraphNode"/> class.
        /// </summary>
        public PeopleGraphNode()
        {
        }

        public HLinkBackLinkModelCollection BackHLinkReferenceCollection => throw new NotImplementedException();

        public DateTime Change { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string DefaultTextShort => throw new NotImplementedException();

        public string Handle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public HLinkKey HLinkKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ItemGlyph ModelItemGlyph { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// The node h link.
        /// </summary>
        public HLinkBackLink NodeHLink
        {
            get; set;
        }
            = new HLinkBackLink();

        public bool Priv { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Valid => throw new NotImplementedException();

        /// <summary>
        /// The x start.
        /// </summary>
        public int XStart
        {
            get; set;
        }

        /// <summary>
        /// The y start.
        /// </summary>
        public int YStart
        {
            get; set;
        }

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

        public int Compare(object a, object b)
        {
            throw new NotImplementedException();
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
            int testFlag = Compare(firstEvent.NodeHLink.HLinkKey, secondEvent.NodeHLink.HLinkKey);

            return testFlag;
        }

        public int CompareTo(object argObj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(PeopleGraphNode other)
        {
            throw new NotImplementedException();
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
            int testFlag = Compare(NodeHLink.HLinkKey, secondEvent.NodeHLink.HLinkKey);

            return testFlag;
        }

        public void LoadBasics(ModelBase argBasics)
        {
            throw new NotImplementedException();
        }
    }
}