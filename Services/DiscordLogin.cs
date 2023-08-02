#nullable disable
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Saaya.Web.Db;
using Saaya.Web.Db.Models;

namespace Saaya.Web.Services
{
    public class DiscordLogin
    {
        private readonly SaayaWebContext _db;
        private readonly RestClient rest;

        public ulong ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public const string CallbackUrl = "https://saaya.dev/admin/callback";

        public DiscordLogin(SaayaWebContext db)
        {
            _db = db;

            rest = new RestClient("https://discord.com/api/");

            if (!File.Exists("config.json"))
                return;

            var json = JsonConvert.DeserializeObject<JObject>(File.ReadAllText("config.json"));
            ClientId = ulong.Parse(json["Id"].ToString());
            ClientSecret = json["Secret"].ToString();
        }

        public string GetAuthURL()
        {
            return new string($"https://discord.com/api/oauth2/authorize?client_id={ClientId}&redirect_uri={Uri.EscapeDataString(CallbackUrl)}&response_type=code&scope=identify%20email");
        }

        public async Task<string> GetAccessToken(string code)
        {
            var request = new RestRequest("oauth2/token", Method.Post);
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", CallbackUrl);
            request.AddParameter("scope", "identify");

            var response = await rest.ExecuteAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"[{response.StatusCode}] {response.ErrorMessage}");
            }

            var parsed = JsonConvert.DeserializeObject<JObject>(response.Content);

            return parsed["access_token"].ToString();
        }

        public async Task<SaayaUser> AuthenticateUser(string token)
        {
            var request = new RestRequest("users/@me", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");

            var response = await rest.ExecuteAsync(request);

            string data = response.Content;
            var deserialized = JsonConvert.DeserializeObject<JObject>(data);

            string id = deserialized["id"].ToString();
            string username = deserialized["global_name"].ToString();

            var user = _db.Users.FirstOrDefault(x => x.Snowflake == ulong.Parse(id));
            if (user == null)
                return null;

            return user;
        }
    }
}