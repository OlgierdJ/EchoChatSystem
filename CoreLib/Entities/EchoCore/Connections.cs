using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore
{
    public class Connection : BaseEntity<uint>
    {
        //platform connections like facebook or x or steam


        //pkce flow https://i.stack.imgur.com/8IJni.png


        public string PlatformName { get; set; } //Twitter

        public string PlatformIcon { get; set; } //Twitter icon
        /*
        * authorize example 
        * send authorization code and code verifier
        * verify auth code and verifier by hashing verifier by same method and matching code and hash
        * return access token
        * 
        * https://authorization-server.com/oauth/authorize
        * ?response_type=code <---- 
        * &client_id=mRkZGFjM <---- unique string representing the specific user (should be persisted safely)
        * &state=5ca75bd30    <---- should not be stored but randomly generated string on request (prevents CSRF attack)
        * &code_challenge=hKpKupTM391pE10xfQiorMxXarRKAHRhTfH  <---- Generate from Code_Verifier (a random generated string) by hashing it
       */
        public string AuthorizeEndpoint { get; set; }
        /* 
         * authentication example
         * generate verifier
         * hash it and send it to authentication endpoint with method
         * get authorization code in return
         * e.g
         * 
         * v3/authentication
         * ?.. &code_challenge=hKpKupTM391pE10xfQiorMxXarRKAHRhTfH
         * &code_challenge_method=s256
         * &redirect_uri
        */
        public string AuthenticationEndpoint { get; set; }
        /*
         * authorization example
         * take token and autho
         */
        public string AuthorizationEndpoint { get; set; }
        //check access_token https://www.googleapis.com/oauth2/v3/tokeninfo?access_token=$%7Byour_token%7D and follow normal flow returning new if access token is still alive.
        public string TokenCheckEndpoint { get; set; }
        //if access_token expired refresh https://www.googleapis.com/oauth2/v4/token?client_id=${CLIENT_ID}&client_secret=${CLIENT_SECRET}&refresh_token=${REFRESH_TOKEN}&grant_type=refresh_token
        public string TokenRefreshEndpoint { get; set; }

        public string ExchangeReturnEndpoint { get; set; }
        public ICollection<AccountConnection>? AccountConnections { get; set; }
    }
}
