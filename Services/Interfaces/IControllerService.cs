using Microsoft.AspNetCore.Mvc;

namespace ms_evva_core.Services.Interfaces;

public interface IControllerService<T> where T : class
{
    Task<IActionResult> GetAllAsync();
    Task<IActionResult> GetByIdAsync(int id);
    Task<IActionResult> AddAsync(T entity);
    Task<IActionResult> UpdateAsync(T entity);
    Task<IActionResult> DeleteAsync(int id);
}