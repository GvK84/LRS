using BackEnd;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace UnitTesting
{

    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
           
        }



        [TestMethod]
        public async Task GetUserByIdTest()
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
            //var validateresult = await service.ValidateUser(newuser);
            //Assert.IsNotNull(validateresult);
            //Assert.AreEqual(validateresult, true);
            var createresult = await service.CreateUser(newuser);
            titlerepo.Verify(r => r.GetMaxId());
            typerepo.Verify(r => r.GetMaxId());
            userrepo.Verify(r => r.Create(newuser));
            Assert.AreEqual(createresult, true);

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

        [TestMethod]
        public async Task UpdateUserTest()
        {
            var userrepo = new Mock<IUserRepository>();
            var titlerepo = new Mock<ITitleRepository>();
            var typerepo = new Mock<ITypeRepository>();
            var olduser = new User { Id = 1, Name = "Gigi", UserTitleId = 1, UserTypeId = 1 };
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
    }
}
