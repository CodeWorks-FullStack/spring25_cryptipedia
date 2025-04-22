namespace cryptipedia.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptidsController : ControllerBase
{

  public CryptidsController(CryptidsService cryptidsService, CryptidEncountersService cryptidEncountersService)
  {
    _cryptidsService = cryptidsService;
    _cryptidEncountersService = cryptidEncountersService;
  }
  private readonly CryptidsService _cryptidsService;
  private readonly CryptidEncountersService _cryptidEncountersService;

  [HttpGet]
  public ActionResult<List<Cryptid>> GetAllCryptids()
  {
    try
    {
      List<Cryptid> cryptids = _cryptidsService.GetCryptids();
      return Ok(cryptids);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{cryptidId}")]
  public ActionResult<Cryptid> GetCryptidById(int cryptidId)
  {
    try
    {
      Cryptid cryptid = _cryptidsService.GetCryptidById(cryptidId);
      return Ok(cryptid);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{cryptidId}/cryptidEncounters")]
  public ActionResult<List<CryptidEncounterProfile>> GetCryptidEncounterProfilesByCryptidId(int cryptidId)
  {
    try
    {
      List<CryptidEncounterProfile> cryptidEncounterProfiles = _cryptidEncountersService.GetCryptidEncounterProfilesByCryptidId(cryptidId);
      return Ok(cryptidEncounterProfiles);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}