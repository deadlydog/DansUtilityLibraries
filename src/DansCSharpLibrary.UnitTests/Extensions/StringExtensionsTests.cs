using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DansCSharpLibrary.Extensions;
using DansCSharpTestingBundle.XUnit.NSubstitue;
using FluentAssertions;
using NSubstitute;
using Ploeh.AutoFixture.Xunit2;
using Xunit;

namespace DansCSharpLibrary.UnitTests.Extensions
{
	public class StringExtensionsTests
	{
		public class WhenConvertingToAnInteger
		{
			[Theory, AutoData]
			[InlineAutoData(-12345)]	// Make sure special cases get tested.
			[InlineAutoData(-1)]
			[InlineAutoData(0)]
			[InlineAutoData(1)]
			[InlineAutoData(12345)]
			public void ItShouldConvertProperly(int number)
			{
				number.ToString().ConvertToOrDefault<int>().Should().Be(number);
			}

			[Theory, AutoData]
			[InlineAutoData("1234567890987654321")] // Number greater than can be handled.
			public void ItShouldReturnDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<int>().Should().Be(0);
			}

			[Theory, AutoData]
			public void ItShouldReturnTheSpecifiedDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<int>(-1).Should().Be(-1);
			}

			[Theory, AutoData]
			public void ItShouldThrowAnExceptionWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.Invoking(s => s.ConvertTo<int>()).ShouldThrow<Exception>();
			}
		}

		public class WhenConvertingToAFloat
		{
			[Theory, AutoData]
			[InlineAutoData(-12345)]    // Make sure special cases get tested.
			[InlineAutoData(-1)]
			[InlineAutoData(0)]
			[InlineAutoData(1)]
			[InlineAutoData(12345)]
			public void ItShouldConvertProperly(float number)
			{
				number.ToString().ConvertToOrDefault<float>().Should().Be(number);
			}

			[Theory, AutoData]
			[InlineAutoData("1234567890987654321012345678909876543210")] // Number greater than can be handled.
			public void ItShouldReturnDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<float>().Should().Be(0);
			}

			[Theory, AutoData]
			public void ItShouldReturnTheSpecifiedDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<float>(-1).Should().Be(-1);
			}

			[Theory, AutoData]
			public void ItShouldThrowAnExceptionWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.Invoking(s => s.ConvertTo<float>()).ShouldThrow<Exception>();
			}
		}

		public class WhenConvertingToABool
		{
			[Theory, AutoData]
			public void ItShouldConvertProperly(bool boolean)
			{
				boolean.ToString().ConvertToOrDefault<bool>().Should().Be(boolean);
			}

			[Theory, AutoData]
			[InlineAutoData("1234567890987654321012345678909876543210")] // Number greater than can be handled.
			public void ItShouldReturnDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<bool>().Should().Be(false);
			}

			[Theory, AutoData]
			public void ItShouldReturnTheSpecifiedDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<bool>(true).Should().Be(true);
			}

			[Theory, AutoData]
			public void ItShouldThrowAnExceptionWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.Invoking(s => s.ConvertTo<bool>()).ShouldThrow<Exception>();
			}

			[Theory]
			[InlineAutoData("TRUE", true)]
			[InlineAutoData("True", true)]
			[InlineAutoData("true", true)]
			[InlineAutoData("FALSE", false)]
			[InlineAutoData("False", false)]
			[InlineAutoData("false", false)]
			public void ItShouldConvertStringsProperly(string boolString, bool boolValue)
			{
				boolString.ConvertToOrDefault<bool>().Should().Be(boolValue);
			}
		}

		public class WhenConvertingToAGuid
		{
			[Theory, AutoData]
			public void ItShouldConvertProperly(Guid guid)
			{
				guid.ToString().ConvertToOrDefault<Guid>().Should().Be(guid);
			}

			[Theory, AutoData]
			public void ItShouldReturnDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<Guid>().Should().Be(Guid.Empty);
			}

			[Theory, AutoData]
			public void ItShouldReturnTheSpecifiedDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				var someGuid = Guid.NewGuid();
				invalidString.ConvertToOrDefault<Guid>(someGuid).Should().Be(someGuid);
			}

			[Theory, AutoData]
			public void ItShouldThrowAnExceptionWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.Invoking(s => s.ConvertTo<Guid>()).ShouldThrow<Exception>();
			}
		}

		public class WhenConvertingToADateTime
		{
			private const string DateTimeFullPrecisionFormat = "o";

			[Theory, AutoData]
			public void ItShouldConvertProperly(DateTime dateTime)
			{
				dateTime.ToString(DateTimeFullPrecisionFormat, CultureInfo.InvariantCulture).ConvertToOrDefault<DateTime>().Should().Be(dateTime);
			}

			[Theory, AutoData]
			public void ItShouldReturnDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.ConvertToOrDefault<DateTime>().Should().Be(DateTime.MinValue);
			}

			[Theory, AutoData]
			public void ItShouldReturnTheSpecifiedDefaultWhenAnInvalidStringIsUsed(string invalidString)
			{
				var now = DateTime.Now;
				invalidString.ConvertToOrDefault<DateTime>(now).Should().Be(now);
			}

			[Theory, AutoData]
			public void ItShouldThrowAnExceptionWhenAnInvalidStringIsUsed(string invalidString)
			{
				invalidString.Invoking(s => s.ConvertTo<DateTime>()).ShouldThrow<Exception>();
			}
		}
	}
}
