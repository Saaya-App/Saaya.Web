#nullable disable
namespace Saaya.Web.Common.Extensions
{
    public static class ISessionExtensions
    {
        private const string UInt64KeyPrefix = "UInt64:";

        public static void SetUInt64(this ISession session, string key, ulong value)
        {
            session.SetString(UInt64KeyPrefix + key, value.ToString());
        }

        public static ulong GetUInt64(this ISession session, string key)
        {
            string valueString = session.GetString(UInt64KeyPrefix + key);

            if (ulong.TryParse(valueString, out ulong value))
            {
                return value;
            }

            return 0;
        }

        public static bool IsAuthenticated(this ISession session)
            => session.GetUInt64("UserId") > 0;

        public static bool HasRole(this ISession session, string role)
            => session.GetString("UserRole").Split(",").Contains(role);
    }
}