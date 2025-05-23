namespace cryptipedia.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly CryptidEncountersService _cryptidEncountersService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, CryptidEncountersService cryptidEncountersService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _cryptidEncountersService = cryptidEncountersService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("cryptidEncounters")]
  public async Task<ActionResult<List<CryptidEncounterCryptid>>> GetAllMyCryptidEncounters()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<CryptidEncounterCryptid> cryptids = _cryptidEncountersService.GetCryptidEncountersByAccountId(userInfo.Id);
      return cryptids;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
