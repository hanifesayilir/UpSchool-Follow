using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpSchool.Domain.Dtos;
using UpSchool.Domain.Utilities;

namespace Upschool.Domain.Tests.Utilities
{
    public class PasswordGeneratorTests
    {
        [Fact]
        public void Generate_ShouldReturnEmptyString_WhenNoCharactersAreIncluded()
        {
           
            // Arrange
            var passwordGenerator = new PasswordGenerator();

            var generatePasswordDto = new GeneratePasswordDto()
            {
                Length = 20,
                IncludeLowercaseCharacters = false,
                IncludeUppercaseCharacters = false,
                IncludeNumbers = false,
                IncludeSpecialCharacters = false,
            };

            // Act
            var password = passwordGenerator.Generate(generatePasswordDto);

            Assert.Equal(string.Empty, password);
        }
         
        [Fact]
        public void Generate_ShouldReturnPasswordWithGivenLength()
        {
            // Arrange
            var ipHelper = A.Fake<IIPHelper>();
            A.CallTo(() => ipHelper.GetCurrentIPAdress()).Returns("195.142.70.227");

            var localDbMock = A.Fake<ILocalDB>();

            A.CallTo(() => localDbMock.IPs).Returns(new List<string>()
            {
                 "192.168.1.11",
                "180.22.22.22",
                "155.166.177.188"
            });

           // var localDb = new LocalDb();

            var passwordGenerator = new PasswordGenerator(ipHelper);

            var generatePasswordDto = new GeneratePasswordDto()
            {
                Length = 20,
                IncludeLowercaseCharacters = true,
                IncludeUppercaseCharacters = false,
                IncludeNumbers = false,
                IncludeSpecialCharacters = false,
            };

            // Act
            var password = passwordGenerator.Generate(generatePasswordDto);

            Assert.Equal(generatePasswordDto.Length, password.Length);

        }


        [Fact]
        public void Generate_ShouldIncludeNumbers_WhenIncludeNumbersIsTrue()
        {
            // Arrange
            var passwordGenerator = new PasswordGenerator();

            var generatePasswordDto = new GeneratePasswordDto()
            {
                Length = 20,
                IncludeLowercaseCharacters = false,
                IncludeUppercaseCharacters = false,
                IncludeNumbers = true,
                IncludeSpecialCharacters = false,
            };

            // Act
            var password = passwordGenerator.Generate(generatePasswordDto);

            Assert.Contains(password, x => char.IsDigit(x));

           // Assert.True(password.Any(Char.IsDigit), password);
        }

        [Fact]
        public void Generate_ShouldIncludeSpecialCharacters_WhenIncludeSpecialCharactersIsTrue()
        {
            var passwordGenerator = new PasswordGenerator();
            var generatePasswordDto = new GeneratePasswordDto()
            {
                Length = 20,
                IncludeLowercaseCharacters = false,
                IncludeUppercaseCharacters = false,
                IncludeNumbers = false,
                IncludeSpecialCharacters = true,
            };

            var specialCharacters = "!@#$%^&*()"; 

            // Act
            var password = passwordGenerator.Generate(generatePasswordDto);

            Assert.Contains(password, x => specialCharacters.Contains(x));
        }
    }
}
