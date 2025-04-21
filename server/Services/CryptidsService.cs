
namespace cryptipedia.Services;

public class CryptidsService
{
  public CryptidsService(CryptidsRepository repository)
  {
    _repository = repository;
  }
  private readonly CryptidsRepository _repository;

  internal List<Cryptid> GetCryptids()
  {
    List<Cryptid> cryptids = _repository.GetCryptids();
    return cryptids;
  }
}