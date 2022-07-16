using System.Reflection;
using CurrencyApp.Core;
using Unity;

namespace CurrencyApp.Tests.Utils
{
	public static class ServiceLocatorHelper
	{
		public static IUnityContainer CreateUnityContainer()
		{
			IUnityContainer container = new UnityContainer();
			MethodInfo dynMethod = typeof(ServiceLocator).GetMethod("Initialize",
				BindingFlags.NonPublic | BindingFlags.Static);
			if (dynMethod != null)
			{
				dynMethod.Invoke(null, new object[] { container });
			}

			ServiceLocator.UseThreadVisibility = true;
			return container;
		}
	}
}