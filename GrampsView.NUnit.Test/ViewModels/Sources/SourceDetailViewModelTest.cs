namespace GrampsViewUnitTest
{
    using GrampsView.Common;
    using GrampsView.ViewModels;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Prism.Events;
    using Prism.Navigation;

    [TestClass]
    public class SourceDetailViewModelTest
    {
        private Mock<ICommonLogging> MockIocCommonLogging;

        private Mock<IEventAggregator> MockIocEventAggregator;

        private Mock<INavigationService> MockIocNavigationService;

        private SourceDetailViewModel MockSourceDetailViewModel;

        [TestCleanup]
        public void Cleanup()
        {
            MockIocCommonLogging = null;

            MockIocEventAggregator = null;

            MockIocNavigationService = null;

            MockSourceDetailViewModel = null;
        }

        [TestInitialize]
        public void Init()
        {
            MockIocCommonLogging = new Mock<ICommonLogging>();

            MockIocEventAggregator = new Mock<IEventAggregator>();

            MockIocNavigationService = new Mock<INavigationService>();

            MockSourceDetailViewModel = new SourceDetailViewModel(MockIocCommonLogging.Object, MockIocEventAggregator.Object, MockIocNavigationService.Object);
        }

        [TestMethod]
        public void PopulateModelViewTest()
        {
            // arrrange

            // act
            //MockSourceDetailViewModel.PopulateViewModel();

            // assert
            //HLinkMediaModel SourceMediaModel = MockSourceDetailViewModel.BaseDetail[0] as HLinkMediaModel;
            //Assert.IsFalse(SourceMediaModel == null);

            //Assert.IsTrue(SourceMediaModel.CardType == DisplayFormat.MediaCardLarge);
        }
    }
}