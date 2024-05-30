using CoreLib.Entities.Base;
using CoreLib.Entities.EchoCore.AccountCore;

namespace CoreLib.Entities.EchoCore.ApplicationCore
{
    /// <summary>
    /// application-wide account used for allowing the user to access specific parts of the program.
    /// <para>
    /// example: a new user would have the role "EchoUser" which in turn would allow the user to use the permissions which is bundled with the role.
    /// </para>
    /// <para>
    ///     note: roles are used for permission bundle grants and therefore the users total collection of permissions
    ///     should be checked when making decisions regarding allowing the user something or not.
    /// </para>
    /// </summary>
    public class Role : BaseRole<ulong, AccountRole, Permission>
    {
    }
}
