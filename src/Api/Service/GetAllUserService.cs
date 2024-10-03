using Infrastructure.Repositories;

using Api.Core;

namespace Api.Service
{
    public class GetAllUserService : IGetAllUserService
    {
        private UserRepository UserRepository;
        public GetAllUserService(UserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async ValueTask<IGetAllUserServiceResponse> ExecuteAsync()
        {
            var validations = await ValidateAsync();
            if (validations.Any())
            {
                return new GetAllUserServiceResponse
                {
                    Success = false,
                    Errors = validations
                };
            }

            var users = await UserRepository.GetAll();
            var userInfoList = users.Select(u => new UserInfo
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Password = u.Password,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();
            
            return new GetAllUserServiceResponse
            {
                Success = true,
                Data = new GetAllUserServiceRow
                {
                    Users = userInfoList
                } as IGetAllUserServiceRow
            };


        }
        public async ValueTask<IEnumerable<string>> ValidateAsync()
        {
            var errors = new List<string>();

            var users = await UserRepository.GetAll();
            if (users == null)
            {
                errors.Add("There are no registered users.");
            }

            return errors;
        }
    }

    public class GetAllUserServiceResponse : IGetAllUserServiceResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public IGetAllUserServiceRow? Data { get; set; }
    }
    public class GetAllUserServiceRow : IGetAllUserServiceRow
    {
        public IEnumerable<IUsersInfo> Users { get; set; }
    }

    public class UserInfo : IUsersInfo
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}