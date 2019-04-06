using VocabularyPracticeWeb.Domain.Users;
using Xunit;

namespace VocabularyPracticeWeb.Tests.Domain.Users
{
	public class ApplicationUserTest
	{
		private ApplicationUser _underTest;

		public ApplicationUserTest()
		{
			_underTest = new ApplicationUser();
		}

		[Fact]
		public void CanLockUser()
		{
			_underTest.Lock();

			Assert.True(_underTest.IsLocked());
		}

		[Fact]
		public void CanUnlockUser()
		{
			_underTest.Lock();
			_underTest.Unlock();

			Assert.False(_underTest.IsLocked());
		}
	}
}
