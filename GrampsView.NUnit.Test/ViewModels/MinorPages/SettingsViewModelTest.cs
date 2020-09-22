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
        private Mock<ICommonLogging> MockIocCommonLogging;

        private Mock<IEventAggregator> MockIocEventAggregator;

        private Mock<INavigationService> MockIocNavigationService;

        private SettingsViewModel MockSettingsViewModel;

        [TestCleanup]
        public void Cleanup()
        {
            MockIocCommonLogging = null;

            MockIocEventAggregator = null;

            MockIocNavigationService = null;

            MockSettingsViewModel = null;
        }

        [TestInitialize]
        public void Init()
        {
            MockIocCommonLogging = new Mock<ICommonLogging>();

            MockIocEventAggregator = new Mock<IEventAggregator>();

            MockIocNavigationService = new Mock<INavigationService>();

            MockSettingsViewModel = new SettingsViewModel(MockIocCommonLogging.Object, MockIocEventAggregator.Object, MockIocNavigationService.Object);
        }

        [TestMethod]
        public void SetThemeLight()
        {
            // arrrange

            // act
            //MockSettingsViewModel.ThemeButtonLightChecked = true;

            // assert
            //Assert.IsTrue(MockSettingsViewModel.ThemeButtonDarkChecked == false);
            //Assert.IsTrue(MockSettingsViewModel.ThemeButtonLightChecked == true);
            //Assert.IsTrue(MockSettingsViewModel.ThemeButtonSystemChecked == false);
        }
    }
}