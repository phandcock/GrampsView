//-----------------------------------------------------------------------
//
// Various routines used by the App class that are put here to keep the App class cleaner
//
// <copyright file="CardGroupCollection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

/// <summary>
/// </summary>
namespace GrampsView.Common
{
    using GrampsView.Data.Model;

    using System;
    using System.Diagnostics.Contracts;

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

        public void Add(CardGroupBase<AddressModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupBase<AttributeModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupBase<LdsOrdModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupBase<URLModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        public void Add(CardGroupBase<PersonNameModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }

        //public void Add(CardGroupBase<PersonRefModel> argCardGroup)
        //{
        //    Contract.Requires(argCardGroup != null);

        //    if (argCardGroup.Count > 0)
        //    {
        //        base.Add(argCardGroup);
        //    }
        //}

        public void Add(CardGroupBase<SurnameModel> argCardGroup)
        {
            Contract.Requires(argCardGroup != null);

            if (argCardGroup.Count > 0)
            {
                base.Add(argCardGroup);
            }
        }
    }
}