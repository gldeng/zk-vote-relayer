using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;

namespace ZkVoteRelayer.Controllers;

[RemoteService]
[Area("app")]
[ControllerName("Authentication")]
public class AntiForgeryController : ZkVoteRelayerController
{
    private readonly IAbpAntiForgeryManager _antiForgeryManager;

    public AntiForgeryController(IAbpAntiForgeryManager antiForgeryManager)
    {
        _antiForgeryManager = antiForgeryManager;
    }

    [HttpPost("token")]
    [IgnoreAntiforgeryToken]
    public Task<string> GetTokenCookie()
    {
        var token = _antiForgeryManager.GenerateToken();

        return Task.FromResult(token);
    }
}