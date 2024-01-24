using Microsoft.AspNetCore.Mvc;
using PRTest.Repository.UnitOfWork;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected IUnitOfWork Uow { get; set; }
	}
}
