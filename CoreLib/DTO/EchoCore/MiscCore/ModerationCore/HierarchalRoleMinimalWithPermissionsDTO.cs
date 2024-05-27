using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.DTO.EchoCore.MiscCore.ModerationCore
{
    public interface IHierarchalRoleMinimalWithPermissions<TPermission> : IHierarchalRole
    {
        //ulong Id { get; set; }
        //int Importance { get; set; }
        //string Name { get; set; }
        ICollection<TPermission> Permissions { get; set; }
    }
    public interface IMentionableRole : IRoleMinimal
    {
        bool AllowAnyoneToMention { get; set; }
    }
    public interface ISeggregatableRole : IRoleMinimal
    {
        public bool DisplaySeperatelyFromOnlineMembers { get; set; }
    }
    public interface ICustomizableRole : IRoleMinimal, IMentionableRole, ISeggregatableRole
    {
       
        public string Colour { get; set; }
        public string IconURL { get; set; }
        
    }

    public class HierarchalRoleMinimalWithPermissionsDTO : IHierarchalRole, IHierarchalRoleMinimalWithPermissions<StatefulPermissionExtendedDTO>, ICustomizableRole
    {
        //if a permission is granted it is true
        //if a permission is denied it is false
        //if the permission is null then you get default from everyone role.
        //permissions are overridden by other roles by x/or-ing them together.
        public ICollection<StatefulPermissionExtendedDTO> Permissions { get; set; } 
        public ulong Id { get; set; }
        public int Importance { get; set; }
        public string Name { get; set; }
        public bool AllowAnyoneToMention { get; set; }
        public bool IsAdmin { get; set; }
        public string Colour { get; set; }
        public string IconURL { get; set; }
        public bool DisplaySeperatelyFromOnlineMembers { get; set; }
    }
}
