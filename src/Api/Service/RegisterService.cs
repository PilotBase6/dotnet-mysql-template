using Infrastructure.Entities;
using Infrastructure.Repositories;
using Api.Core;

namespace Api.Service
{
    public class RegisterService : IRegisterService
    {
        private readonly UserRepository UserRepository;
        public RegisterService(UserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async ValueTask<IRegisterServiceResponse> ExecuteAsync(IRegisterServiceRequest request)
        {
            var validations = await ValidateAsync(request);
            if (validations.Any())
            {
                return new RegisterServiceResponse
                {
                    Success = false,
                    Errors = validations
                };
            }

            var user = new User(request.Name, request.Email, request.Password);
            await UserRepository.Create(user);

            return new RegisterServiceResponse
            {
                Success = true,
                Message = "User created successfully"
            };
        }
        public async ValueTask<IEnumerable<string>> ValidateAsync(IRegisterServiceRequest request)
        {
            var errors = new List<string>();
            var user = await UserRepository.GetByEmailAsync(request.Email);

            if (string.IsNullOrEmpty(request.Email))
            {
                errors.Add("Email is required");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                errors.Add("Password is required");
            }
            if (user != null)
            {
                errors.Add("User already exists");
            }
            else
            {
                try
                {
                    var tempUser = new User(request.Name, request.Email, request.Password);
                }
                catch (ArgumentException e)
                {
                    errors.Add(e.Message);
                }

            }

            return errors;
        }
    }
    public class RegisterServiceRequest : IRegisterServiceRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterServiceResponse : IRegisterServiceResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public IRegisterServiceRow Data { get; set; }
    }
    public class RegisterServiceRow : IRegisterServiceRow
    {
        public string? Message { get; set; }
    }
}