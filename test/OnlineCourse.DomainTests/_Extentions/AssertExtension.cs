using OnlineCourse.Domain._Base;
using Xunit;

namespace OnlineCourse.DomainTests._Extentions
{
    public static class AssertExtension
    {
        public static void WithMessage(this DomainException exception, string message)
        {
            if (exception.ListOfRules.Contains(message))
            {
                Assert.True(true);
            } else
            {
                Assert.False(true, $"Expected message {message}");
            }
        }
    }
}
