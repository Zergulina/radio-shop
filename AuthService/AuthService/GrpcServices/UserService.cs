using AuthService.BLL.Interfaces.Services;
using Grpc.Core;

namespace AuthService.GrpcServices
{
    public class UserService : UserGrpc.UserGrpcBase
    {
        private readonly IUserService _userService;
        public UserService(IUserService userService) { 
            _userService = userService;
        }
        public async override Task<UserGrpcResponse> GetUser(UserGrpcRequest request, ServerCallContext context)
        {
            var id = request.Id;

            try
            {
                var user = await _userService.GetByIdAsync(id);
                return new UserGrpcResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Email = user.Email,
                };
            }
            catch (Exception ex)
            {
                return new UserGrpcResponse { };
            }
        }

        public async override Task<UserGrpcResponseList> GetUserList (UserGrpcListRequest request, ServerCallContext context)
        {
            var ids = request.Ids.ToList();

            try
            {
                var users = await _userService.GetByIdsAsync(ids);
                var response = new UserGrpcResponseList();

                users.ForEach(user => response.Users.Add(
                new UserGrpcResponse
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Email = user.Email,
                }));

                return response;
            }
            catch (Exception ex)
            {
                return new UserGrpcResponseList();
            }
        }

        public async override Task<UserExistsGrpcResponse> UserExists(UserGrpcRequest request, ServerCallContext context)
        {
            var id = request.Id;

            try
            {
                var exists = await _userService.ExistsAsync(id);
                return new UserExistsGrpcResponse
                {
                    Exists = exists
                };
            }
            catch (Exception ex)
            {
                return new UserExistsGrpcResponse { Exists = false };
            }
        }

        public async override Task<UserExistsGrpcResponseList> UserExistsList(UserGrpcListRequest request, ServerCallContext context)
        {
            var ids = request.Ids.ToList();

            try
            {
                var notExistsIds = await _userService.ExistsByIdsAsync(ids);
                var response = new UserExistsGrpcResponseList();

                notExistsIds.ForEach(id => response.Ids.Add(id));

                return response;
            }
            catch (Exception ex)
            {
                return new UserExistsGrpcResponseList();
            }
        }
    }
}
