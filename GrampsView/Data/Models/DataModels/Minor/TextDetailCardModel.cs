// <copyright file="InstructCardModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class InstructCardModel : ModelBase, IInstructCardModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructCardModel" /> class.
        /// </summary>
        public InstructCardModel()
        {
        }

        /// <summary>
        /// Gets or sets the text details.
        /// </summary>
        /// <value>
        /// The text details.
        /// </value>
        public string TextDetails
        {
            get; set;
        }

        = string.Empty;
    }
}