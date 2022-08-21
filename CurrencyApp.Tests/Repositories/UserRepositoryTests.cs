using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AutoFixture.Xunit2;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Model;
using CurrencyApp.Model.Enums;
using CurrencyApp.Repositories;
using CurrencyApp.Repositories.Interfaces;
using CurrencyApp.Tests.Utils;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Repositories
{
	public class UserRepositoryTests
	{
		private readonly DBAppContext _context;
		private readonly UserRepository userRepository;

		public UserRepositoryTests()
		{
			ServiceLocatorHelper.CreateUnityContainer();
			
			_context = DatabaseInMemoryManagementService.Create();
			DatabaseRecordsAddingService.AddTestUsers(_context);

			ServiceLocator.RegisterSingleton(_context);
			userRepository = new UserRepository(_context);
		}

		[Theory, AutoMoqData]
		public void GetByUserName_WhenDataNotExists_ReturnNull()
		{
			var result = userRepository.GetByUserName(It.IsAny<string>());

			Assert.Null(result);
		}

		[Theory, AutoMoqData]
		public void GetByUserName_WhenDataExists_ThenReturnUser()
		{
			var result = userRepository.GetByUserName("test2test2");

			Assert.NotNull(result);
		}

		[Theory, AutoMoqData]
		public void IsCorrectPassword_WhenPasswordIsIncorrect_ThenReturnFalse()
		{
			var result = userRepository.IsCorrectPassword(5, It.IsAny<string>());

			Assert.False(result);
		}

		[Theory, AutoMoqData]
		public void IsCorrectPassword_WhenPasswordIsCorrect_ThenReturnTrue()
		{
			var result = userRepository.IsCorrectPassword(5, "12345");

			Assert.True(result);
		}
	}
}