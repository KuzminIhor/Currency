using System;
using System.Linq;
using System.Security.Authentication;
using AutoFixture.Xunit2;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Model;
using CurrencyApp.Repositories.Interfaces;
using CurrencyApp.Tests.Utils;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Helpers.Authentication
{
	public class AuthenticationProcessTests
	{
		[Theory, AutoMoqData]
		public void Authenticate_WhenUserDoesNotExist_ThenThrowAnException(
			[Frozen] Mock<IUserRepository> userRepositoryMock,
			AuthenticationProcess authenticationProcess)
		{
			userRepositoryMock.Setup(u => u.GetUser(It.IsAny<string>())).Returns(null as User);

			Assert.Throws<AuthenticationException>(() => authenticationProcess.Authenticate(It.IsAny<string>(), It.IsAny<string>()));
		}

		[Theory, AutoMoqData]
		public void Authenticate_WhenUserExistsButPasswordIsWrong_ThenThrowAnException(
			[Frozen] Mock<IUserRepository> userRepositoryMock,
			AuthenticationProcess authenticationProcess)
		{
			User user = new User();

			userRepositoryMock.Setup(u => u.GetUser(It.IsAny<string>())).Returns(user);
			userRepositoryMock.Setup(u => u.IsCorrectPassword(user.Id, It.IsAny<string>())).Returns(false);

			Assert.Throws<AuthenticationException>(() => authenticationProcess.Authenticate(It.IsAny<string>(), It.IsAny<string>()));
		}

		[Theory, AutoMoqData]
		public void Authenticate_WhenUserExistsAndPasswordIsCorrect_ThenCopyUserDataIntoCurrentUserObject(
			[Frozen] Mock<IUserRepository> userRepositoryMock,
			AuthenticationProcess authenticationProcess)
		{
			User user = new User();

			userRepositoryMock.Setup(u => u.GetUser(It.IsAny<string>())).Returns(user);
			userRepositoryMock.Setup(u => u.IsCorrectPassword(user.Id, It.IsAny<string>())).Returns(true);

			authenticationProcess.Authenticate(It.IsAny<string>(), It.IsAny<string>());

			CurrentUser currentUser = CurrentUser.GetInstance();
			
			Assert.Equal(currentUser.Id, user.Id);
		}
	}
}