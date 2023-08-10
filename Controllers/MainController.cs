using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanCodeArchitecture.Controllers
{
    public class MainController : BaseApiController<MainController>
    {
        private IMongoContext<E_Test> entitesTest { get; set; }
        public MainController()
        {

        }
    }
}