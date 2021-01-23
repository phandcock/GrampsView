﻿// XML 171 - Not in definition so created this for use with BackLink functionality

// TODO fix Deref caching

namespace GrampsView.Data.Model
    {
    using GrampsView.Data.DataView;
    using GrampsView.Views;

    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using Xamarin.CommunityToolkit.ObjectModel;

    [DataContract]
    public class HLinkPersonNameModel : HLinkBase, IHLinkPersonNameModel
    {
        private PersonNameModel _Deref = new PersonNameModel();


        public IAsyncCommand<HLinkPersonNameModel> UCNavigateCommand { get; set; }


        public HLinkPersonNameModel()
        {
            UCNavigateCommand = new AsyncCommand<HLinkPersonNameModel>(navPage => UCNavigate(navPage));
        }

        public PersonNameModel DeRef
        {
            get
            {
                if (Valid & (!_Deref.Valid))
                {
                    _Deref = DV.PersonNameDV.GetModelFromHLinkString(HLinkKey);
                }

                return _Deref;
            }
        }

        /// <summary>
        /// Compares to. Bases it on the HLinkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public new int CompareTo(object obj)
        {
            HLinkPersonNameModel arg = obj as HLinkPersonNameModel;

            // Null objects go first
            if (arg is null) { return 1; }

            // Can only comapre if they are the same type so assume equal
            if (arg.GetType() != typeof(HLinkPersonNameModel))
            {
                return 0;
            }

            return DeRef.CompareTo(arg.DeRef);
        }

        public async Task UCNavigate(HLinkPersonNameModel argHLink)
        {
            await UCNavigateBase(argHLink, nameof(PersonNameDetailPage));
        }
    }
}