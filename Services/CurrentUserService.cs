using Application.Interfaces;
using System.Security.Claims;

namespace CleanCodeArchitecture.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;

            UserId = new Guid(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            Name = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
            SurName = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);
            Roles = httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            Permissions = httpContextAccessor.HttpContext?.User?.Claims
                .Where(x => x.Type == ClaimTypes.AuthenticationMethod).ToList();
        }

        private IHttpContextAccessor httpContextAccessor { get; set; }

        public Guid UserId { get; }
        public string Name { get; }
        public string SurName { get; }
        public List<Claim> Roles { get; }
        public List<Claim> Permissions { get; }

        public string Email { get; }
    }
}
