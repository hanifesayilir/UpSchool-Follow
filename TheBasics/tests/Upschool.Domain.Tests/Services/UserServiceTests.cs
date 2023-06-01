using FakeItEasy;
using System;
using System.Linq.Expressions;
using System.Threading;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace Upschool.Domain.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancelationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId,
            };

            A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancelationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancelationSource.Token);

            Assert.Equal(expectedUser, user);

        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancelationSource = new CancellationTokenSource();

         

            var userEmpty = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Han",
                LastName = "Den",
                Email = string.Empty,
                Age = 1,

            };

            var userNull = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Han",
                LastName = "Den",
                Email = null,
                Age = 1,

            };


            IUserService userService = new UserManager(userRepositoryMock);
            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddAsync(userEmpty.FirstName, userEmpty.LastName, userEmpty.Age, userEmpty.Email,cancelationSource.Token));
            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddAsync(userNull.FirstName, userNull.LastName, userNull.Age, userNull.Email, cancelationSource.Token));
        }



        [Fact]
        public async Task AddAsync_ShouldReturn_CorrectUserId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancelationSource = new CancellationTokenSource();

             

            var expectedUser = new User()
            {
              
                FirstName = "Han",
                LastName = "Den",
                Email = "hanife@gmail.com",
                Age = 1,

            };
 
            IUserService userService = new UserManager(userRepositoryMock);

            var actualUserId = await userService.AddAsync(expectedUser.FirstName, expectedUser.LastName, expectedUser.Age, expectedUser.Email, cancelationSource.Token);
    
            Assert.IsType<Guid>(actualUserId);
        }

         

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = Guid.Empty;

            var user = new User()
            {
                Id = userId,
                FirstName = "RRRR",
                LastName = "TRTRT",
                Email = "hanife@gmail",
                Age = 15,
            };

            var cancelationSource = new CancellationTokenSource();
            IUserService userService = new UserManager(userRepositoryMock);

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.UpdateAsync(user, cancelationSource.Token));

        }



        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

        

            var userEmpty = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Hanife",
                LastName = "SAY",
                Email = string.Empty,
                Age = 15,
            };

            var userNull = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Hanife",
                LastName = "SAY",
                Email = null,
                Age = 15,
            };


            var cancelationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.UpdateAsync(userNull, cancelationSource.Token));

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.UpdateAsync(userEmpty, cancelationSource.Token));
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExist()
        {

            var userRepositoryMock = A.Fake<IUserRepository>();

           

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Hanife",
                LastName = "SAY",
                Email = null,
                Age = 15,
            };


            var cancelationSource = new CancellationTokenSource();
            IUserService userService = new UserManager(userRepositoryMock);
/*
            A.CallTo(() => userRepositoryMock.DeleteAsync(id, cancellationSource.Token))
                .Returns(Task.FromResult(1));*/

            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.DeleteAsync(user.Id, cancelationSource.Token));
          
        }


        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {

            var userRepositoryMock = A.Fake<IUserRepository>();
            var cancelationSource = new CancellationTokenSource();
           


            var user = new User()
            {
     
                Id = Guid.NewGuid(),
                FirstName = "Hanife",
                LastName = "SAY",
                Email = null,
                Age = 15,

            };

            IUserService userService = new UserManager(userRepositoryMock);
           // await userService.AddAsync(user.FirstName, user.LastName, user.Age, user.Email, cancelationSource.Token);





           // A.CallTo(async () => await userRepositoryMock.DeleteAsync(user =>user.Id, cancellationSource.Token))
              //  .Returns(Task.FromResult(1));
            /*
                        A.CallTo(() => userRepositoryMock.DeleteAsync(id, cancellationSource.Token))
                            .Returns(Task.FromResult(1));*/



           // await Assert.ThrowsAsync(() => userService.DeleteAsync(user.Id, cancellationSource.Token))

        }


        [Fact]
        public async Task GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancelationSource = new CancellationTokenSource();


            var userFirst = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Hanife",
                LastName = "SAY",
                Email = "hanife.say@gmail.com",
                Age = 35,
            };

            var userSecond = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Yunus Emre",
                LastName = "SAY",
                Email = "yunus@gmail.com",
                Age = 17,
            };

            IUserService userService = new UserManager(userRepositoryMock);

            var d = await userService.AddAsync(userFirst.FirstName, userFirst.LastName, userFirst.Age, userFirst.Email, cancelationSource.Token);
            await userService.AddAsync(userSecond.FirstName, userSecond.LastName, userSecond.Age, userSecond.Email, cancelationSource.Token);

            //   A.CallTo(() => userRepositoryMock.GetAllAsync(cancelationSource.Token)).Returns(Task.FromResult(List<User>));


            var g = await userService.GetByIdAsync(d, cancelationSource.Token);

            List<User> userList = await userService.GetAllAsync(cancelationSource.Token);


            Console.WriteLine("USERLIST "+userList.Count);

           Assert.True(userList.Count > 0);

        }


    }





}
