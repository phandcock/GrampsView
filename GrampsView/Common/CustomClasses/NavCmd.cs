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

        public NavigationParameters TargetNavParams { get; set; } = new NavigationParameters();

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

            parameters.Add(CommonConstants.NavigationParameterTargetView, Target);

            Nav(parameters);
        }

        public void Nav(INavigationParameters parameters)
        {
            if (parameters is null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            localEventAggregator.GetEvent<PageNavigateParmsEvent>().Publish(parameters);
        }
    }
}