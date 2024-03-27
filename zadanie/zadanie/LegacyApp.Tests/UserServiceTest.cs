using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{
    [Fact]
    public void AddUser_Should_Return_True_When_AllData_Is_Correct()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_When_Client_Is_Under21()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2020-04-21"), 1);
        //Assert
        Assert.False(addResult);
    }
    
    
    [Fact]
    public void AddUser_Should_Return_False_When_FirstName_Is_Missing()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Surname_Is_Missing()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Email_Is_Missing_AtSign_Or_Dot()
    {
        //Arrange
        var userService = new UserService();
        //Act
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        //Assert
        Assert.False(addResult);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Is_Not_VeryImportant()
    {
        // Arrange
        var client = new Client { Type = "ImportantClient" };

        // Act
        bool result = UserService.IsClientVeryImportant(client);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_Client_Is_Not_Important()
    {
        // Arrange
        var client = new Client { Type = "VeryImportantClient" };

        // Act
        bool result = UserService.IsClientImportant(client);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void SettingCreditLimit_WhenClientIsVeryImportant_ShouldUserCreditLimitBeFalse()
    {
        // Arrange
        var client = new Client { Type = "VeryImportantClient" };
        var user = UserService.UserCreator("John", "Doe", "john.doe@gmail.com", DateTime.Parse("1982-03-21"), client);

        // Act
        UserService.SettingCreditLimit(client, user);

        // Assert
        Assert.False(user.HasCreditLimit);
    }

    [Fact]
    public void SettingCreditLimit_WhenClientIsImportant_ShouldSetUserCreditLimitTwoTimeHigherThanNormalClient()
    {
        // Arrange
        var client = new Client { Type = "ImportantClient" };
        var user = UserService.UserCreator("John", "Doe", "john.doe@gmail.com", DateTime.Parse("1982-03-21"), client);
        UserService.ChangeCreditLimitToNormalClient(user);
        var limitBefore = user.CreditLimit;


        // Act
        UserService.SettingCreditLimit(client, user);

        // Assert
        Assert.Equal(limitBefore * 2, user.CreditLimit);
    }

    [Fact]
    public void SettingCreditLimit_WhenClientIsNoramal_ShouldUserCreditLimitBeTrue()
    {
        // Arrange
        var client = new Client { Type = "NormalClient" };
        var user = UserService.UserCreator("John", "Doe", "john.doe@gmail.com", DateTime.Parse("1982-03-21"), client);
        
        // Act
        UserService.SettingCreditLimit(client, user);

        // Assert
        Assert.True(user.HasCreditLimit);
    }

    [Fact]
    public void AddUser_Should_Return_False_When_User_Limit_Is_Lower_Than_500()
    {
        //Arrange
        var userService = new UserService();
        //Act - Kowalski ma limit 200
        var addResult = userService.AddUser("John", "Kowalski", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        //Assert 
        Assert.False(addResult);
    }
    
    
}