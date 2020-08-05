namespace GrampsViewUnitTest
{
    using GrampsView.Common;
    using GrampsView.ViewModels;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Prism.Events;
    using Prism.Navigation;

    [TestClass]
    public class SettingsViewModelTest
    {
        private Mock<ICommonLogging> _iocCommonLogging;

        private Mock<IEventAggregator> _iocEventAggregator;

        private Mock<INavigationService> _iocNavigationService;

        private SettingsViewModel _SettingsViewModel;

        [TestCleanup]
        public void Cleanup()
        {
            _iocCommonLogging = null;

            _iocEventAggregator = null;

            _iocNavigationService = null;

            _SettingsViewModel = null;
        }

        [TestInitialize]
        public void Init()
        {
            _iocCommonLogging = new Mock<ICommonLogging>();

            _iocEventAggregator = new Mock<IEventAggregator>();

            _iocNavigationService = new Mock<INavigationService>();

            _SettingsViewModel = new SettingsViewModel(_iocCommonLogging.Object, _iocEventAggregator.Object, _iocNavigationService.Object);
        }

        [TestMethod]
        public void NavigateShouldCallNavigate()
        {
        }
    }
}
