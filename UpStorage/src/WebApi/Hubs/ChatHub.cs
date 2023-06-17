using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class ChatHub : Hub
    {
        private static List<string> users = new List<string>();

        public List<string> GetAllUsers()
        {
            return users;
        }

        public Task UserConnected(string userName)
        {
            users.Add(userName);

            return Clients.AllExcept(Context.ConnectionId).SendAsync("NewUserConnected", userName);
        }

        public bool CheckName(string name)
        {

            if (users.Contains(name))
            {
                return false; // İsim daha önce girilmişse false döndür
            }

            users.Add(name);
            Clients.Caller.SendAsync("NameCheckResult", true); // İsim doğruysa true değerini React/TypeScript tarafına iletmek için olayı tetikle
            return true; // İsim daha önce girilmemişse true döndür

        }  
    }
}
