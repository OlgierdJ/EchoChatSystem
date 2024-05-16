namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public class DeviceSessionDTO //used for displaying current sessions, granting sessionid such that you have interaction with devicesessions on api.
    {
        public ulong Id { get; set; }
        //public string SessionToken { get; set; } //Token used for verifying calls to the api on the accounts behalf //not needed on client probably 
        /*
         * login flow would be:
         * Startup ->
         * retrieve token from storage ->
         * send token to api ->
         * verify token on api not expired or revoked ->
         * return jwt with userid (used for identity, permission verification on api futherrequests)
         */
        public string DeviceId { get; set; } //PC, Windows, Desktop-JHLDPH1235, etc
        public string Location { get; set; } //Denmark, Ballerup, maybe ip, etc
        public DateTime TimeStarted { get; set; } //when session is created //actually should also have time last logged in and display that instead but fk it this is only semiuseful date.

        //probably not needed on client aswell as server will just say sessiontoken stopped working and client will delete device locally
        //default expiration time when session doesnt work and will logout client (null = permanent session unless revoked)
        //public DateTime? ExpirationTime { get; set; }

    }
}
