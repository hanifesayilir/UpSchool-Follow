using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Helpers
{
    public static class MessagesHelper
    {

        public static class Email
        {

            public static class Confirmation
            {
                public static string Subject => "Thank you for signing up to Upstorage!";

                public static string ButtonText => "Activate My Account";

                public static string Name(string firstName) => $"Hi {firstName}";

                public static string ButtonLink(string email, string emailToken) => 
                    
                    $"https://upstorage.app/account-activation?email={email}&token={emailToken}";

                public static string ActivationMessage => "Thank you for signing up to Upstorage! In order to activate your account click teh activation button given below.";
            }

            
        }
    }
}
