using AutoMapper;
using CoreLib.DTO.EchoCore.UserCore;
using CoreLib.DTO.RequestCore.FriendCore;
using CoreLib.DTO.RequestCore.RelationCore;
using CoreLib.DTO.RequestCore.UserCore;
using CoreLib.Entities.EchoCore.AccountCore;
using CoreLib.Entities.EchoCore.UserCore;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositorys;
using CoreLib.Interfaces.Services;
using DomainCoreApi.Handlers;
using DomainCoreApi.Services.Bases;

namespace DomainCoreApi.Services
{
    public class UserService : BaseEntityService<User, ulong> ,IUserService //need the interface so the user can login 
    {

        private readonly IPasswordHandler _pwdHandler;
        private readonly IAccountService _accountService;
        private readonly ILanguageRepository _languageRepository;
        private readonly CreateUserHandler _createUserHandler = new();

        public UserService(IUserRepository repository, IPasswordHandler pwdHandler, IAccountService accountService, ILanguageRepository languageRepository) : base(repository)
        {
            _pwdHandler = pwdHandler;
            _accountService = accountService;
            _languageRepository = languageRepository;
        }

        public async Task<User> Register(RegisterRequestDTO requestDTO)
        {
            try
            {
                var data = await _createUserHandler.CreateHandler(requestDTO);
                var result = await _repository.AddAsync(data.Item1);
                data.Item2.UserId = result.Id;
                await _pwdHandler.CreatePassword(requestDTO.Password, data.Item1.Id);
                await _accountService.AddAsync(data.Item2);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            };
        }

        public async Task<User> CreateUserAsync(RegisterRequestDTO input)
        {
            try
            {
                Console.WriteLine(input);
                var data = await _createUserHandler.CreateHandler(input);
                var result = await _repository.AddAsync(data.Item1);
                data.Item2.UserId = result.Id;
                await _pwdHandler.CreatePassword(input.Password, data.Item1.Id);
                await _accountService.AddAsync(data.Item2);
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<string> LoginUserAsync(LoginRequestDTO attempt)
        {
            var user = await _repository.GetSingleWithIncludeAsync(e => e.Email == attempt.Email);
            if (user is not null && await _pwdHandler.CheckPassword(attempt.Password, user.Id))
            {
                //send a jwt back to the website
                TokenHandler tokenHandler = new();
                return tokenHandler.CreateToken<Account>(user.Account);
            }

            throw new Exception("You suck at hacking bruv");
        }

        public async Task<bool> UpdatePassword(ulong id, string password)
        {
            try
            {
                var result = await _pwdHandler.UpdatePassword(password, id);
                return result is not null;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task<UserFullDTO> GetFullDTOAsync(ulong id)
        {
            try
            {
                var result = await _repository.GetSingleWithIncludeAsync(e => e.Id == id);
                if(result != null)
                {
                    UserFullDTO user = new UserFullDTO()
                    {
                        BlockedUsers = null,
                        Friends = null,
                        Requests = null,
                        UserProfile = new UserProfileDTO()
                        {
                            AboutMe = null,
                            BannerColour = "",
                            Note = "",
                            User = new UserDTO()
                            {
                                ActiveStatus = new()
                                {
                                    DisplayedContent = "Busy",
                                    Icon = "",
                                    IconColor = "Warning",
                                    Name = "Busy"
                                },
                                //Profile not included
                                //DisplayName = result.Account.Profile.DisplayName,
                                //ImageIconURL = result.Account.Profile.AvatarFileURL,
                                Name = result.Account.Name
                            },
                        },
                    };
                    return user;
                }
                return null;
                
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public Task<bool> DeafenSelf(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAccount(ulong id, DeleteAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableAccount(ulong id, DisableAccountRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AcceptFriendRequest(ulong id, AcceptFriendRequestRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddFriend(ulong id, SendFriendRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserConnection(ulong id, AddUserConnectionRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockUser(ulong id, BlockUserRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MuteSelf(ulong id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MuteUser(ulong id, MuteRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }



        public Task<bool> RemoveFriend(ulong id, RemoveFriendRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetCustomStatus(ulong id, SetStatusRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetNickname(ulong id, SetNicknameUserRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetNote(ulong id, SetNoteUserRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetPhoneNumber(ulong id, EditPhoneNumberRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetStatus(ulong id, SetStatusRequestDTO requestDTO)
        {
            throw new NotImplementedException();
        }


    }
}
