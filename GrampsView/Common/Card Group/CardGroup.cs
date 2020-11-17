/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System;
    using System.Diagnostics.Contracts;

    using static GrampsView.Common.CommonEnums;

    /// <summary>
    /// </summary>
    public class CardGroup : CardGroupBase<object>
    {
        public CardGroup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CardGroup"/> class with the ModelBase for a
        /// single card.
        /// </summary>
        /// <param name="argCard">
        /// The argument card.
        /// </param>
        public CardGroup(ModelBase argCard)
        {
            CardGroup t = new CardGroup
            {
                argCard
            };

            base.Add(t);
        }

        public void Add(HLinkBase argCard, DisplayFormat argDisplayFormat = DisplayFormat.Default)
        {
            Contract.Assert(argCard != null);

            argCard.CardType = argDisplayFormat;

            base.Add(argCard);
        }

        public void Add(CardGroup argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupBase<SrcAttributeModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroup argCardGroup, string argTitle)
        {
            if (argCardGroup is null)
            {
                throw new ArgumentNullException(nameof(argCardGroup));
            }

            if (argTitle is null)
            {
                throw new ArgumentNullException(nameof(argTitle));
            }

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);

                this.Title = argTitle;
            }
        }
    }
}