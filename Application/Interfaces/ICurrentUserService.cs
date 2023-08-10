using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid UserId { get; }
        public string Name { get; }
        public string SurName { get; }
        public string Email { get; }
        public List<Claim> Roles { get; }
        public List<Claim> Permissions { get; }
    }
}
