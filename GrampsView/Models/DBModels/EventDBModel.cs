// Copyright (c) phandcock.  All rights reserved.

using GrampsView.Data.Model;
using GrampsView.Models.DataModels;
using GrampsView.Models.HLinks.Models;

namespace GrampsView.Models.DBModels
{
    public class EventDBModel : DBModel<EventModel, HLinkEventModel>, IDBModel<EventModel, HLinkEventModel>
    {
        public EventDBModel()
        {
        }

        public EventDBModel(EventModel argEventModel)
        {
            Serialise(argEventModel);
        }
    }
}