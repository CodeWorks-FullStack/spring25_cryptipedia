

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

  internal Cryptid GetCryptidById(int cryptidId)
  {
    Cryptid cryptid = _repository.GetCryptidById(cryptidId);

    if (cryptid == null)
    {
      throw new Exception("No cryptid found with the id of " + cryptidId);
    }

    return cryptid;
  }
}