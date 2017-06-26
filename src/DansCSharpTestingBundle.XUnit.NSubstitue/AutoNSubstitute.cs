using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit2;

namespace DansCSharpTestingBundle.XUnit.NSubstitue
{
	public class AutoNSubstitute
	{
		public class AutoNSubstituteDataAttribute : AutoDataAttribute
		{
			public AutoNSubstituteDataAttribute()
				: base(new Fixture().Customize(new AutoNSubstituteCustomization()))
			{
			}
		}
	}
}
