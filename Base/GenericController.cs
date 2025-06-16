using Microsoft.AspNetCore.Mvc;
using ms_evva_core.Services.Interfaces;


namespace ms_evva_core.Base;

[Route("/api/v1/[controller]")]
[ApiController]
public class GenericController<T> : ControllerBase where T : class
{
    protected readonly IControllerService<T> Service;

    public GenericController(IControllerService<T> service)
    {
        Service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [HttpGet]
    public virtual Task<IActionResult> GetAll() => Service.GetAllAsync();

    [HttpGet("{id}")]
    public virtual Task<IActionResult> GetById(int id) => Service.GetByIdAsync(id);

    [HttpPost]
    public virtual Task<IActionResult> Add([FromBody] T entity) => Service.AddAsync(entity);

    [HttpPut]
    public virtual Task<IActionResult> Update([FromBody] T entity) => Service.UpdateAsync(entity);

    [HttpDelete("{id}")]
    public virtual Task<IActionResult> Delete(int id) => Service.DeleteAsync(id);
}