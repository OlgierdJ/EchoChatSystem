using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echo.Domain.Shared.Interfaces.Services;
public interface IDomainServerService
{
    public Task<bool> RegisterAsync(RegisterResourceApplicationRequestDTO requestDTO);
    public Task<bool> RegisterAsync(RegisterClientApplicationRequestDTO requestDTO);
}

public class RegisterResourceApplicationRequestDTO
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public class RegisterClientApplicationRequestDTO
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}
