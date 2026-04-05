using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [ApiController]
    public class BaseController: ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
