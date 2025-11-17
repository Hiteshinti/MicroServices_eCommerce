using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.IRepository;
using eCommerce.Core.IServices;
using eCommerce.Core.Services;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UserServiceTest
{
    [TestFixture]
    public class AuthControllerTest
    {

        private Mock<IUserRepository> _userRepositoryMock;
        private IUserService _userservice;
        private Mock<IConfiguration> _configuration;  
        private Mock<IMapper> _mapper;


        [SetUp]
        public void Setup()
        {
            _userRepositoryMock =new Mock<IUserRepository>();   
            _mapper = new Mock<IMapper>();
            _configuration =  new Mock<IConfiguration>();   
            _userservice = new UserService(_userRepositoryMock.Object,_mapper.Object,_configuration.Object); 
        }

        [Test]
        public void Add_Login_InValidCredentials_Test()
        {
            _userRepositoryMock.Setup(r => r.GetUserByEmailAndPassword("hiteashinti@gmail.com", "Pass@123"))
               .ReturnsAsync(new ApplicationUser { Email = "hiteashinti@gmail.com", Password = "Pass@123" });

            var user =  _userservice.Login( new LoginDto { Email="hiteshinti@gmail.com", Password="Pass@123" }).Result;

            Assert.Null(user);
        }
        [Test]
        public void Add_Login_ValidCredentials_Test()
        {
            var appuser = new ApplicationUser
            {
                Email = "hiteashinti@gmail.com",
                UserId = Guid.NewGuid(),
                Gender = "Male",
                UserName = "Hitesh_inti",
                Password = "Pass@123"
            };
            _userRepositoryMock.Setup(r => r.GetUserByEmailAndPassword("hiteashinti@gmail.com", "Pass@123"))
             .ReturnsAsync(appuser);

            _mapper.Setup(r => r.Map<AuthenticationDto>(appuser)).Returns(new AuthenticationDto
            {
                 Email= "hiteashinti@gmail.com"
            });
            var user = _userservice.Login(new LoginDto { Email = "hiteashinti@gmail.com", Password = "Pass@123" }).Result;

            Assert.NotNull(user);
            Assert.AreEqual("hiteashinti@gmail.com", user.Email);
        }


        [TearDown]
        public void ClearInstances()
        {
           
        }
    }
}