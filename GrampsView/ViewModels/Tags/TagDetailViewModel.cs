namespace GrampsView.ViewModels
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;
    using GrampsView.Data.Model;

    using Prism.Events;

    using System;

    /// <summary>
    /// Defines the Tag Detail Page View ViewModel.
    /// </summary>
    public class TagDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDetailViewModel"/> class.
        /// </summary>
        /// <param name="iocCommonLogging">
        /// The ioc common logging.
        /// </param>
        /// <param name="iocEventAggregator">
        /// The ioc event aggregator.
        /// </param>
        public TagDetailViewModel(ICommonLogging iocCommonLogging, IEventAggregator iocEventAggregator)
            : base(iocCommonLogging, iocEventAggregator)
        {
        }

        /// <summary>
        /// Gets or sets the tag object.
        /// </summary>
        /// <value>
        /// The tag object.
        /// </value>

        public TagModel TagObject
        {
            get; set;
        }

        /// <summary>
        /// Handles navigation in wards and sets up the event model parameter.
        /// </summary>
        /// <param name="e">
        /// The <see cref="NavigatedToEventArgs"/> instance containing the event data.
        /// </param>
        /// <param name="viewModelState">
        /// The parameter is not used.
        /// </param>
        public override void BaseHandleLoadEvent()
        {
            HLinkTagModel HLinkObject = CommonRoutines.DeserialiseObject<HLinkTagModel>(Uri.UnescapeDataString(BaseParamsHLink));

            TagObject = HLinkObject.DeRef;

            if (!(TagObject is null))
            {
                BaseTitle = "Tag Detail";
                BaseTitleIcon = CommonConstants.IconTag;

                // Get Headers
                //CardGroup t = new CardGroup { Title = "Header Details" };

                BaseDetail.Add(new CardListLineCollection("Tag Detail")
                {
                        new CardListLine("Name:", TagObject.GName),
                        new CardListLine("Priority:", TagObject.GPriority.ToString(System.Globalization.CultureInfo.CurrentCulture)),
                        new CardListLine("Private:", TagObject.Priv.ToString()),
                });

                BaseDetail.Add(DV.TagDV.GetModelInfoFormatted(TagObject));
            }

            return;
        }

        //private int numColumns = 3;

        //public GridItemsLayout GL
        //{
        //    get

        // { GridItemsLayout t = new GridItemsLayout(orientation: ItemsLayoutOrientation.Vertical) {
        // HorizontalItemSpacing = 2, VerticalItemSpacing = 2, Span = numColumns, };

        //        return t;
        //    }
        //}

        //private void TagDetailPageRoot_SizeChanged(object sender, EventArgs e)
        //{
        //    Contract.Requires(sender != null);

        // TagDetailPage tt = sender as TagDetailPage;

        // this.numColumns = (Int32)(tt.Width / CardSizes.Current.CardSmallWidth + 1); // +1 for padding

        //    tt.theCollectionView.ItemsLayout = GL;
        //}
    }
}