using Infrastructure.Repositories;
using Api.Core;
using Infrastructure.Entities;

namespace Api.Service
{
    public class LoginService : ILoginService
    {
        private readonly UserRepository UserRepository;
        public LoginService(UserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        public async ValueTask<ILoginServiceResponse> ExecuteAsync(ILoginServiceRequest request)
        {
            var validations = await ValidateAsync(request);
            if (validations.Any())
            {
                return new LoginServiceResponse
                {
                    Success = false,
                    Errors = validations
                };
            }

            var user = await UserRepository.GetByEmailAsync(request.Email);

            return new LoginServiceResponse
            {
                Success = true,
                Message = "User created successfully"
            };
        }
        public async ValueTask<IEnumerable<string>> ValidateAsync(ILoginServiceRequest request)
        {
            var errors = new List<string>();
            var user = await UserRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                errors.Add("User not found");
            }
            if (user != null && !user.VerifyPassword(request.Password))
            {
                errors.Add("Invalid password");
            }
            if (string.IsNullOrEmpty(request.Email))
            {
                errors.Add("Email is required");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                errors.Add("Password is required");
            }
            try
            {
                User.ValidateEmail(request.Email);
            }
            catch (ArgumentException e)
            {
                errors.Add(e.Message);
            }
            try
            {
                User.ValidateEmail(request.Email);

            }
            catch (ArgumentException e)
            {
                errors.Add(e.Message);
            }

            return errors;
        }
    }
    public class LoginServiceRequest : ILoginServiceRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginServiceResponse : ILoginServiceResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public ILoginServiceRow Data { get; set; }
    }
    public class LoginServiceRow : ILoginServiceRow
    {
        public string? Message { get; set; }
    }
}