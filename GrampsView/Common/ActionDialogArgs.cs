using System.Collections.Generic;

namespace GrampsView.Common
{
    public class ActionDialogArgs
    {
        public Dictionary<string, string> ItemDetails { get; set; }
            = new Dictionary<string, string>();

        public string Name { get; set; }
                            = string.Empty;

        public string Text { get; set; }
                    = string.Empty;
    }
}