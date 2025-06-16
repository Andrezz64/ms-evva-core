using ms_evva_core.Base;
using ms_evva_core.Repos.Interfaces;
using ms_evva_core.Services.Interfaces;

namespace ms_evva_core.Services.Classes;

public class HostControllerService : GenericControllerService<Models.Host>,IHostControllerService
{
    private readonly IHostRepository _hostRepository;

    public HostControllerService(IHostRepository hostRepository) 
        : base(hostRepository)
    {
        _hostRepository = hostRepository;
    }

    // Métodos adicionais podem ser adicionados aqui, se necessário
    // Exemplo:
    // public async Task<IActionResult> GetByDomainNameAsync(string domainName)
    // {
    //     try
    //     {
    //         var host = await _hostRepository.GetByDomainNameAsync(domainName);
    //         return host != null ? Ok(host) : NotFound();
    //     }
    //     catch (Exception ex)
    //     {
    //         return ErrorHandler.InvokeError(ex);
    //     }
    // }
}