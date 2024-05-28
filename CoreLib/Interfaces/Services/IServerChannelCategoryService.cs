using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.ServerCore.ChannelCore;
using CoreLib.DTO.RequestCore.ServerCore.Role;

namespace CoreLib.Interfaces.Services
{
    public interface IServerChannelCategoryService
    {
        //category
        Task<bool> CreateCategory(ulong userid, ulong serverid, CreateChannelRequestDTO requestDTO);
        Task<bool> MarkAsRead(ulong userid, ulong serverid, ulong categoryId);
        Task<bool> MuteCategory(ulong userid, ulong serverid, ulong categoryId, MuteRequestDTO requestDTO);
        Task<bool> UnmuteCategory(ulong userid, ulong serverid, ulong categoryId);
        Task<bool> MoveCategory(ulong userid, ulong serverid, ulong categoryId, MoveCategoryRequestDTO requestDTO);
        Task<bool> DeleteCategory(ulong userid, ulong serverid, ulong categoryId);
        Task<bool> UpdateCategory(ulong userid, ulong serverid, ulong categoryId, EditCategoryRequestDTO requestDTO);

        //role
        Task<bool> AddCategoryRole(ulong userid, ulong serverid, ulong categoryId, ulong roleId);
        Task<bool> SetCategoryRolePermission(ulong userid, ulong serverid, ulong categoryId, ulong roleId, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetCategoryRolePermissions(ulong userid, ulong serverid, ulong categoryId, ulong roleId, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> RemoveCategoryRole(ulong userid, ulong serverid, ulong categoryId, ulong roleId);

        //member
        Task<bool> AddCategoryMember(ulong userid, ulong serverid, ulong categoryId, ulong memberId);
        Task<bool> SetCategoryMemberPermission(ulong userid, ulong serverid, ulong categoryId, ulong memberId, SetPermissionStateRequestDTO requestDTO);
        Task<bool> SetCategoryMemberPermissions(ulong userid, ulong serverid, ulong categoryId, ulong memberId, SetMultiplePermissionStateRequestDTO requestDTO);
        Task<bool> RemoveCategoryMember(ulong userid, ulong serverid, ulong categoryId, ulong memberId);
    }

}
