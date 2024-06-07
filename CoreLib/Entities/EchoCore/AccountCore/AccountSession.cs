using CoreLib.Entities.Base;

namespace CoreLib.Entities.EchoCore.AccountCore
{
    public class AccountSession : BaseEntity<ulong>
    {
        public ulong AccountId { get; set; }
        public string AccessToken { get; set; } //Token used for verifying calls to the api on the accounts behalf (short lifetime)
        public string RefreshToken { get; set; } //Token used for getting new access tokens for a session.
        public string DeviceId { get; set; } //PC, Windows, Desktop-JHLDPH1235, etc
        public string Location { get; set; } //Denmark, Ballerup, maybe ip, etc
        public DateTime TimeStarted { get; set; } //when session is created
        public DateTime? ExpirationTime { get; set; } //default expiration time when session doesnt work and will logout client (null = permanent session unless revoked)
        public DateTime? TimeStopped { get; set; } //time session has been revoked

        public Account Account { get; set; }
    }
}
