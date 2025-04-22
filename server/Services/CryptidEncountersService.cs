



namespace cryptipedia.Services;

public class CryptidEncountersService
{
  public CryptidEncountersService(CryptidEncountersRepository repository)
  {
    _repository = repository;
  }
  private readonly CryptidEncountersRepository _repository;

  internal CryptidEncounterProfile CreateCryptidEncounter(CryptidEncounter cryptidEncounterData)
  {
    CryptidEncounterProfile cryptidEncounterProfile = _repository.CreateCryptidEncounter(cryptidEncounterData);
    return cryptidEncounterProfile;
  }

  internal List<CryptidEncounterProfile> GetCryptidEncounterProfilesByCryptidId(int cryptidId)
  {
    List<CryptidEncounterProfile> cryptidEncounterProfiles = _repository.GetCryptidEncounterProfilesByCryptidId(cryptidId);
    return cryptidEncounterProfiles;
  }

  internal List<CryptidEncounterCryptid> GetCryptidEncountersByAccountId(string accountId)
  {
    List<CryptidEncounterCryptid> cryptids = _repository.GetCryptidEncountersByAccountId(accountId);
    return cryptids;
  }

  internal void DeleteCryptidEncounter(int cryptidEncounterId, Account userInfo)
  {
    CryptidEncounter cryptidEncounter = GetCryptidEncounterById(cryptidEncounterId);

    if (cryptidEncounter.AccountId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE ANOTHER USER'S ENCOUNTER, {userInfo.Name.ToUpper()}!!!");
    }

    _repository.DeleteCryptidEncounter(cryptidEncounterId);
  }

  private CryptidEncounter GetCryptidEncounterById(int cryptidEncounterId)
  {
    CryptidEncounter cryptidEncounter = _repository.GetCryptidEncounterById(cryptidEncounterId);

    if (cryptidEncounter == null)
    {
      throw new Exception("Invalid id: " + cryptidEncounterId);
    }

    return cryptidEncounter;
  }
}