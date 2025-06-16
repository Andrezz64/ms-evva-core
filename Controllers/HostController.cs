using Microsoft.AspNetCore.Mvc;
using ms_evva_core.Services.Interfaces;
using ms_evva_core.Base;
using ms_evva_core.Models;

namespace ms_evva_core.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class HostController : GenericController<Models.Host>
{
    private readonly IHostControllerService _hostService;

    public HostController(IHostControllerService hostService) 
        : base(hostService)
    {
        _hostService = hostService;
    }

}