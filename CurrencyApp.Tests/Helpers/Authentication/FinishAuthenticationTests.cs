using System;
using CurrencyApp.Core;
using CurrencyApp.Helpers;
using CurrencyApp.Model;
using CurrencyApp.Model.Enums;
using CurrencyApp.Tests.Utils;
using Moq;
using Xunit;

namespace CurrencyApp.Tests.Helpers.Authentication
{
	public class FinishAuthenticationTests
	{
		[Theory, AutoMoqData]
		public void GetFormToRedirect_WhenUserIsGuest_ThenReturnGuestForm(
			FinishAuthentication finishAuthentication)
		{
			var formType = finishAuthentication.GetFormToRedirect("guest", It.IsAny<string>());

			Assert.Equal(FormType.GuestForm, formType);
		}

		[Theory, AutoMoqData]
		public void GetFormToRedirect_WhenUserIsBank_ThenReturnBankForm(
			FinishAuthentication finishAuthentication, 
			string userName)
		{
			CurrentUser user = CurrentUser.GetInstance();
			user.IsBankUser = true;

			var formType = finishAuthentication.GetFormToRedirect(userName, It.IsAny<string>());

			Assert.Equal(FormType.BankUserForm, formType);
		}

		[Theory, AutoMoqData]
		public void GetFormToRedirect_WhenUserIsAdmin_ThenReturnAdminForm(
			FinishAuthentication finishAuthentication)
		{
			var formType = finishAuthentication.GetFormToRedirect("admin", It.IsAny<string>());

			Assert.Equal(FormType.AdminForm, formType);
		}
	}
}