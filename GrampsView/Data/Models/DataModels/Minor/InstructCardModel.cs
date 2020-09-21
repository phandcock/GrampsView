namespace GrampsView.Data.Model
{
    using System.Runtime.Serialization;

   
    public class InstructCardModel : ModelBase, IInstructCardModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructCardModel"/> class.
        /// </summary>
        /// TODO Update fields as per Schema
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