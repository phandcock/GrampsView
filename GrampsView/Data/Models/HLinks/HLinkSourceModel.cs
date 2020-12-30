// - XML 1.71 complete

namespace GrampsView.Data.Model
{
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    [DataContract]
    public class HLinkSourceModel : HLinkBase, IHLinkSourceModel
    {
        private SourceModel _Deref = new SourceModel();

        public HLinkSourceModel()
        {
            UCNavigateCommand = new Command<HLinkSourceModel>(UCNavigate);
        }

        /// <summary>
        /// Gets the model from the HLink.
        /// </summary>
        /// <value>
        /// The HLink reference.
        /// </value>
        public SourceModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.SourceDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argOobj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(HLinkSourceModel argOobj) => DeRef.CompareTo(argOobj);

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="argObj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object argObj)
        {
            // Null objects go first
            if (argObj is null) { return 1; }

            // Can only compare if they are the same type so assume equal
            if (argObj.GetType() != typeof(HLinkSourceModel))
            {
                return 0;
            }

            return DeRef.CompareTo((argObj as HLinkSourceModel).DeRef);
        }

        public async void UCNavigate(HLinkSourceModel argHLink)
        {
            string jason = await Task.Run(() => CommonRoutines.SerialiseObject<HLinkSourceModel>(argHLink));

            await Shell.Current.GoToAsync(string.Format("SourceDetailPage?BaseParamsHLink={0}", jason));
        }
    }
}