using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace CurrencyApp.Tests.Utils
{
	public class AutoMoqDataAttribute : AutoDataAttribute
	{
		public AutoMoqDataAttribute()
			: base(() => new Fixture().Customize(new AutoMoqCustomization()))
		{
		}

		public AutoMoqDataAttribute(Func<IFixture> factory)
			: base(() => factory().Customize(new AutoMoqCustomization()))
		{
		}
    }
}