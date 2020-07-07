using GrampsView.Events;

using Prism.Events;
using Prism.Navigation;
using System;

namespace GrampsView.Common
{
    public class NavCmd
    {
        private readonly IEventAggregator localEventAggregator;

        public NavCmd()
        {
        }

        public NavCmd(IEventAggregator iocEventAggregator)
        {
            localEventAggregator = iocEventAggregator;
        }

        public void Nav(string Target)
        {
            INavigationParameters t = new NavigationParameters();

            Nav(t, Target);
        }

        public void Nav(INavigationParameters parameters, string Target)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            parameters.Add("Target", Target);

            localEventAggregator.GetEvent<PageNavigateParmsEvent>().Publish(parameters);
        }
    }
}