using Microsoft.AspNetCore.Mvc;
using ms_evva_core.Base;
using ms_evva_core.Models;
using ms_evva_core.Services.Interfaces;

namespace ms_evva_core.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : GenericController<User>
{
    public UserController(IUserControllerService service) : base(service)
    {
    }
} 