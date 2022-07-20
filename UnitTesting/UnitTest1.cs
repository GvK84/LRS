using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting
{
    // TODO Naming convention of file and project to match the project in test
    [TestClass]
    public class TestService
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<ITitleRepository> _mockTitleRepository;
        private Mock<ITypeRepository> _mockTypeRepository;

        private MainService _testService;

        /// <summary>Initializes a new instance of the <see cref="TestService" /> class.</summary>
        public TestService()
        {
        }

        // TODO use TestInitialize
        [TestInitialize]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockTitleRepository = new Mock<ITitleRepository>();
            _mockTypeRepository = new Mock<ITypeRepository>();

            _testService = new MainService(_mockUserRepository.Object, _mockTitleRepository.Object,
                _mockTypeRepository.Object);
        }

        [TestMethod]
        public async Task GetAll_Success()
        {
            // arrange
            var userList = new List<User>
            {
                new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 },
                new User { Name = "Gigi2", UserTitleId = 2, UserTypeId = 1 },
                new User { Name = "Gigi3", UserTitleId = 1, UserTypeId = 2 }
            };
            _mockUserRepository.Setup(r => r.GetAll()).ReturnsAsync(userList);

            // act
            var users = await _testService.GetUsersAsync();

            // assert
            _mockUserRepository.Verify(r => r.GetAll());
            var element = users.First(r => r.Name == "Gigi3");
            Assert.AreEqual(element.UserTypeId, 2);
            Assert.AreEqual(users.Count(), 3);
        }

        /// <summary>Test the GetActiveUsers method</summary>
        [TestMethod]
        public async Task GetActiveUsersTest()
        {
            var listactiveusers = new List<User>
            {
                new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1, IsActive = true },
                new User { Name = "Gigi3", UserTitleId = 1, UserTypeId = 2, IsActive = true }
            };
            _mockUserRepository.Setup(r => r.GetAllActive()).ReturnsAsync(listactiveusers);

            var users = await _testService.GetActiveUsers();

            _mockUserRepository.Verify(r => r.GetAllActive());
            Assert.AreEqual(users.Count(), 2);
        }

        /// <summary>Test the GetUser method</summary>
        [TestMethod]
        public async Task GetUserTest()
        {
            var newUser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };
            _mockUserRepository.Setup(r => r.GetById(1)).ReturnsAsync(newUser);

            var user = await _testService.GetUser(1);

            _mockUserRepository.Verify(r => r.GetById(1));
            Assert.AreEqual(user.Name, "Gigi");
        }

        /// <summary>Test the CreateUser method</summary>
        [TestMethod]
        public async Task CreateUserTest()
        {
            var newuser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };

            _mockUserRepository.Setup(r => r.Create(It.IsAny<User>())).Returns(Task.CompletedTask);

            var createresult = await _testService.CreateUser(newuser);

            _mockUserRepository.Verify(r => r.Create(newuser));
            Assert.AreEqual(createresult, true);
        }

        /// <summary>Test the UpdateUser method</summary>
        [TestMethod]
        public async Task UpdateUserTest()
        {
            var newuser = new User { Id = 1, Name = "GigiNew", UserTitleId = 2, UserTypeId = 1 };
            _mockTitleRepository.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            _mockTypeRepository.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            _mockUserRepository.Setup(r => r.Update(It.IsAny<User>())).Returns(Task.CompletedTask);

            var updateresult = await _testService.UpdateUser(1, newuser);

            _mockUserRepository.Verify(r => r.Update(newuser));
            Assert.AreEqual(updateresult, true);
        }

        /// <summary>Test the DeleteUser method</summary>
        [TestMethod]
        public async Task DeleteUserTest()
        {
            var newuser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };
            _mockUserRepository.Setup(r => r.GetById(1)).ReturnsAsync(newuser);
            _mockUserRepository.Setup(r => r.Delete(It.IsAny<User>())).Returns(Task.CompletedTask);

            var createresult = await _testService.DeleteUser(1);

            _mockUserRepository.Verify(r => r.GetById(1));
            _mockUserRepository.Verify(r => r.Delete(newuser));
            Assert.AreEqual(createresult, true);
        }

        /// <summary>Test the ValidateUser method</summary>
        /// <param name="user">The user to validate.</param>
        /// <param name="expectedValue">The expected value of the test
        /// result</param>
        [DataTestMethod]
        [DynamicData(nameof(UserTestingData), DynamicDataSourceType.Method)]
        public async Task ValidateUserTest(User user, bool expectedValue)
        {
            _mockTitleRepository.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            _mockTypeRepository.Setup(r => r.GetMaxId()).ReturnsAsync(2);

            var result = await _testService.ValidateUser(user);

            _mockTitleRepository.Verify(r => r.GetMaxId());
            _mockTypeRepository.Verify(r => r.GetMaxId());
            Assert.AreEqual(result, expectedValue);
        }

        /// <summary>Gets the Users to test and the expected value of the result</summary>
        /// <returns>Users and expected values</returns>
        public static IEnumerable<object[]> UserTestingData()
        {
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 }, true }; //ok
            yield return new object[] { new User { UserTitleId = 1, UserTypeId = 1 }, false }; //name fail
            yield return new object[]
                { new User { Name = "Gigi", UserTitleId = 0, UserTypeId = 1 }, false }; //title fail
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 1 }, false }; //type fail
            yield return new object[]
                { new User { Name = "Gigi", UserTitleId = 10, UserTypeId = 1 }, false }; //title fail
        }

        /// <summary>Test the GetTitles method</summary>
        [TestMethod]
        public async Task GetTitlesTest()
        {
            var listtitles = new List<UserTitle>
            {
                new UserTitle { Id = 1, Description = "Title1" },
                new UserTitle { Id = 2, Description = "Title2" }
            };
            _mockTitleRepository.Setup(r => r.GetAll()).ReturnsAsync(listtitles);

            var titles = await _testService.GetTitles();

            _mockTitleRepository.Verify(r => r.GetAll());
            var element = titles.First(r => r.Id == 2);
            Assert.AreEqual(element.Description, "Title2");
        }

        /// <summary>Test the GetTitle method</summary>
        [TestMethod]
        public async Task GetTitleTest()
        {
            var newtitle = new UserTitle { Id = 1, Description = "Title1" };
            _mockTitleRepository.Setup(r => r.GetById(1)).ReturnsAsync(newtitle);

            var title = await _testService.GetTitle(1);

            _mockTitleRepository.Verify(r => r.GetById(1));
            Assert.AreEqual(title.Description, "Title1");
        }

        /// <summary>Test the GetTypes method</summary>
        [TestMethod]
        public async Task GetTypesTest()
        {
            var listtypes = new List<UserType>
            {
                new UserType { Id = 1, Description = "Type1" },
                new UserType { Id = 2, Description = "Type2", Code = "33" }
            };
            _mockTypeRepository.Setup(r => r.GetAll()).ReturnsAsync(listtypes);

            var types = await _testService.GetTypes();

            _mockTypeRepository.Verify(r => r.GetAll());
            var element = types.First(r => r.Id == 2);
            Assert.AreEqual(element.Code, "33");
        }

        /// <summary>Test the GetType method</summary>
        [TestMethod]
        public async Task GetTypeTest()
        {
            var newtype = new UserType { Id = 1, Description = "Type1" };
            _mockTypeRepository.Setup(r => r.GetById(1)).ReturnsAsync(newtype);


            var type = await _testService.GetType(1);
            _mockTypeRepository.Verify(r => r.GetById(1));
            Assert.AreEqual(type.Description, "Type1");
        }
    }
}