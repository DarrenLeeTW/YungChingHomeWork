using Reqnroll;
using Moq;
using Xunit;
using YungChingHomeWork.Models;
using YungChingHomeWork.Services;
using YungChingHomeWork.Repositories;

namespace YungChingHomeWork.Tests.StepDefinitions
{
    [Binding]
    public class 房屋列表管理Steps
    {
        private readonly Mock<IHouseListingRepository> _mockRepository;
        private readonly HouseListingService _service;
        private HouseListing? _houseListing;
        private HouseListing? _result;
        private bool _operationResult;

        public 房屋列表管理Steps()
        {
            _mockRepository = new Mock<IHouseListingRepository>();
            _service = new HouseListingService(_mockRepository.Object);
        }

        [Given(@"我有以下房屋資訊")]
        public void Given我有以下房屋資訊(Table table)
        {
            var row = table.Rows[0];
            _houseListing = new HouseListing
            {
                Name = row["名稱"],
                Address = row["地址"],
                Price = decimal.Parse(row["價格"])
            };
        }

        [Given(@"系統中存在ID為(.*)的房屋列表")]
        public void Given系統中存在ID為的房屋列表(int id)
        {
            _houseListing = new HouseListing
            {
                Id = id,
                Name = "現代公寓",
                Address = "台北市信義區101號",
                Price = 1000000
            };

            _mockRepository.Setup(r => r.GetById(id))
                .Returns(_houseListing);
        }

        [When(@"我建立新的房屋列表")]
        public void When我建立新的房屋列表()
        {
            Assert.NotNull(_houseListing);
            _mockRepository.Setup(r => r.Create(It.IsAny<HouseListing>()))
                .Returns((HouseListing hl) =>
                {
                    hl.Id = 1;
                    return hl;
                });

            _result = _service.CreateHouseListing(_houseListing);
        }

        [When(@"我查詢該房屋列表")]
        public void When我查詢該房屋列表()
        {
            Assert.NotNull(_houseListing);
            _result = _service.GetHouseListingById(_houseListing.Id);
        }

        [When(@"我更新以下房屋資訊")]
        public void When我更新以下房屋資訊(Table table)
        {
            Assert.NotNull(_houseListing);
            var row = table.Rows[0];
            var updatedListing = new HouseListing
            {
                Id = _houseListing.Id,
                Name = row["名稱"],
                Address = row["地址"],
                Price = decimal.Parse(row["價格"])
            };

            _mockRepository.Setup(r => r.Update(_houseListing.Id, It.IsAny<HouseListing>()))
                .Returns(true);

            _operationResult = _service.UpdateHouseListing(_houseListing.Id, updatedListing);
        }

        [When(@"我刪除該房屋列表")]
        public void When我刪除該房屋列表()
        {
            Assert.NotNull(_houseListing);
            _mockRepository.Setup(r => r.Delete(_houseListing.Id))
                .Returns(true);

            _operationResult = _service.DeleteHouseListing(_houseListing.Id);
        }

        [Then(@"系統應該成功保存房屋資訊")]
        public void Then系統應該成功保存房屋資訊()
        {
            Assert.NotNull(_result);
            _mockRepository.Verify(r => r.Create(It.IsAny<HouseListing>()), Times.Once);
        }

        [Then(@"我應該收到包含ID的房屋資訊")]
        public void Then我應該收到包含ID的房屋資訊()
        {
            Assert.NotNull(_result);
            Assert.NotEqual(0, _result.Id);
        }

        [Then(@"我應該收到房屋的詳細資訊")]
        public void Then我應該收到房屋的詳細資訊()
        {
            Assert.NotNull(_result);
        }

        [Then(@"房屋資訊應該包含以下內容")]
        public void Then房屋資訊應該包含以下內容(Table table)
        {
            Assert.NotNull(_result);
            var row = table.Rows[0];
            Assert.Equal(row["名稱"], _result.Name);
            Assert.Equal(row["地址"], _result.Address);
            Assert.Equal(decimal.Parse(row["價格"]), _result.Price);
        }

        [Then(@"系統應該成功更新房屋資訊")]
        public void Then系統應該成功更新房屋資訊()
        {
            Assert.True(_operationResult);
            _mockRepository.Verify(r => r.Update(It.IsAny<int>(), It.IsAny<HouseListing>()), Times.Once);
        }

        [Then(@"系統應該成功刪除該房屋資訊")]
        public void Then系統應該成功刪除該房屋資訊()
        {
            Assert.True(_operationResult);
            _mockRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Once);
        }

        [Then(@"我應該無法查詢到該房屋列表")]
        public void Then我應該無法查詢到該房屋列表()
        {
            Assert.NotNull(_houseListing);
            _mockRepository.Setup(r => r.GetById(_houseListing.Id))
                .Returns((HouseListing?)null);

            var deletedListing = _service.GetHouseListingById(_houseListing.Id);
            Assert.Null(deletedListing);
        }
    }
}
