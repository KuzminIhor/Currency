/*using System;
using System.Security.Authentication;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Tests.Utils;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Helpers.Authentication
{
	public class AuthenticationFieldsValidatorTests
	{
		[Theory, AutoMoqData]
		public void Validate_WhenUserNameIsEmpty_ThenThrowAnException
		(
			AuthenticationFieldsValidator authenticationFieldsValidator
		)
		{
			Assert.Throws<AuthenticationException>(() =>
				authenticationFieldsValidator.Validate(string.Empty, It.IsAny<string>()));
		}

		[Theory, AutoMoqData]
		public void Validate_WhenPasswordIsEmpty_ThenThrowAnException
		(
			AuthenticationFieldsValidator authenticationFieldsValidator
		)
		{
			Assert.Throws<AuthenticationException>(() =>
				authenticationFieldsValidator.Validate(It.IsAny<string>(), string.Empty));
		}

		[Theory, AutoMoqData]
		public void Validate_WhenAllFieldsAreFilled_ThenDoesntThrowAnyException
		(
			AuthenticationFieldsValidator authenticationFieldsValidator,
			string userName,
			string password
		)
		{
			var exception =
				Record.Exception(() => authenticationFieldsValidator.Validate(userName, password));

			Assert.Null(exception);
		}
	}
}*/