#nullable disable
namespace Saaya.Web.Common.Extensions
{
    public static class ISessionExtensions
    {
        public static bool IsAuthenticated(this ISession session)
            => session.GetInt32("UserId") > 0;

        public static bool HasRole(this ISession session, string role)
            => session.GetString("UserRole").Split(",").Contains(role);
    }
}