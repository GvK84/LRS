using BackEnd;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace UnitTesting
{

    [TestClass]
    public class TestService
    {
        public TestService()
        {
           
        }

        [TestMethod]
        public async Task GetUsersTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var listusers = new List<User> { new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 },
                                        new User { Name = "Gigi2", UserTitleId = 2, UserTypeId = 1 },
                                        new User { Name = "Gigi3", UserTitleId = 1, UserTypeId = 2 }};
            userrepo.Setup(r => r.GetAll()).ReturnsAsync(listusers);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var users = await service.GetUsers();
            userrepo.Verify(r => r.GetAll());
            var element = users.Where(r => r.Name == "Gigi3").First();
            Assert.AreEqual(element.UserTypeId, 2);
        }
        
        [TestMethod]
        public async Task GetActiveUsersTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var listactiveusers = new List<User> { new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1, IsActive=true },
                                        new User { Name = "Gigi3", UserTitleId = 1, UserTypeId = 2, IsActive=true }};
            userrepo.Setup(r => r.GetAllActive()).ReturnsAsync(listactiveusers);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var users = await service.GetActiveUsers();
            userrepo.Verify(r => r.GetAllActive());
            Assert.AreEqual(users.Count(), 2);
        }

        [TestMethod]
        public async Task GetUserTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newuser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };
            userrepo.Setup(r => r.GetById(1)).ReturnsAsync(newuser);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var user = await service.GetUser(1);
            userrepo.Verify(r => r.GetById(1));
            Assert.AreEqual(user.Name, "Gigi");
        }

        [TestMethod]
        public async Task CreateUserTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newuser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };

            userrepo.Setup(r => r.Create(It.IsAny<User>())).Returns(Task.CompletedTask);
            titlerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            typerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);

            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var createresult = await service.CreateUser(newuser);
            titlerepo.Verify(r => r.GetMaxId());
            typerepo.Verify(r => r.GetMaxId());
            userrepo.Verify(r => r.Create(newuser));
            Assert.AreEqual(createresult, true);

        }

        [TestMethod]
        public async Task UpdateUserTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newuser = new User { Id = 1, Name = "GigiNew", UserTitleId = 2, UserTypeId = 1 };
            titlerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            typerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            userrepo.Setup(r => r.Update(It.IsAny<User>())).Returns(Task.CompletedTask);

            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var updateresult = await service.UpdateUser(1, newuser);
            titlerepo.Verify(r => r.GetMaxId());
            typerepo.Verify(r => r.GetMaxId());
            userrepo.Verify(r => r.Update(newuser));
            Assert.AreEqual(updateresult, true);
        }
        [TestMethod]

        public async Task DeleteUserTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newuser = new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };
            userrepo.Setup(r => r.GetById(1)).ReturnsAsync(newuser);
            userrepo.Setup(r => r.Delete(It.IsAny<User>())).Returns(Task.CompletedTask);
            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var createresult = await service.DeleteUser(1);
            userrepo.Verify(r => r.GetById(1));
            userrepo.Verify(r => r.Delete(newuser));
            Assert.AreEqual(createresult, true);

        }

       

        [DataTestMethod]
        [DynamicData(nameof(UserTestingData), DynamicDataSourceType.Method)]      
        public async Task ValidateUserTest(User user, bool expectedvalue)
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();

            titlerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);
            typerepo.Setup(r => r.GetMaxId()).ReturnsAsync(2);

            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var result = await service.ValidateUser(user);
            titlerepo.Verify(r => r.GetMaxId());
            typerepo.Verify(r => r.GetMaxId());
            Assert.AreEqual(result, expectedvalue);

        }
        public static IEnumerable<object[]> UserTestingData()
        {
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 1, UserTypeId = 1 }, true }; //ok
            yield return new object[] { new User { UserTitleId = 1, UserTypeId = 1 }, false }; //name fail
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 0, UserTypeId = 1 }, false }; //title fail
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 1 }, false }; //type fail
            yield return new object[] { new User { Name = "Gigi", UserTitleId = 10, UserTypeId = 1 }, false }; //title fail
        }

        [TestMethod]
        public async Task GetTitlesTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var listtitles = new List<UserTitle> { new UserTitle { Id= 1, Description = "Title1" },
                                        new UserTitle { Id= 2, Description = "Title2" } };
            titlerepo.Setup(r => r.GetAll()).ReturnsAsync(listtitles);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var titles = await service.GetTitles();
            titlerepo.Verify(r => r.GetAll());
            var element = titles.Where(r => r.Id == 2).First();
            Assert.AreEqual(element.Description, "Title2");
        }

        [TestMethod]
        public async Task GetTitleTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newtitle = new UserTitle { Id = 1, Description = "Title1" };
            titlerepo.Setup(r => r.GetById(1)).ReturnsAsync(newtitle);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var title = await service.GetTitle(1);
            titlerepo.Verify(r => r.GetById(1));
            Assert.AreEqual(title.Description, "Title1");
        }

        [TestMethod]
        public async Task GetTypesTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var listtypes = new List<UserType> { new UserType { Id= 1, Description = "Type1" },
                                        new UserType { Id= 2, Description = "Type2", Code="33" } };
            typerepo.Setup(r => r.GetAll()).ReturnsAsync(listtypes);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var types = await service.GetTypes();
            typerepo.Verify(r => r.GetAll());
            var element = types.Where(r => r.Id == 2).First();
            Assert.AreEqual(element.Code, "33");
        }
        [TestMethod]
        public async Task GetTypeTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var newtype = new UserType { Id = 1, Description = "Type1" };
            typerepo.Setup(r => r.GetById(1)).ReturnsAsync(newtype);


            var service = new UserService(userrepo.Object, titlerepo.Object, typerepo.Object);
            var type = await service.GetType(1);
            typerepo.Verify(r => r.GetById(1));
            Assert.AreEqual(type.Description, "Type1");
        }
    }
}
