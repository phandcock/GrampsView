// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Models.DataModels;
using GrampsView.Models.DataModels.Date;
using GrampsView.Models.DBModels.Interfaces;
using GrampsView.Models.HLinks.Models;

namespace GrampsView.Models.DBModels
{
    public class EventDBModel : DBModel<EventModel, HLinkEventModel>, IEventDBModel<EventModel, HLinkEventModel>
    {
        public EventDBModel()
        {
        }

        public EventDBModel(EventModel argEventModel)
        {
            Serialise(argEventModel);

            GDate = new DateDBModelBase(argEventModel.GDate);

            EventDBModel t = this;
        }

        public DateDBModelBase GDate
        {
            get;

            set;
        } = new DateDBModelBase();
    }
}