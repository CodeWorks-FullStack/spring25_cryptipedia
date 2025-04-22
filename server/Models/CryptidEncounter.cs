namespace cryptipedia.Models;

public class CryptidEncounter : RepoItem<int>
{
  public int CryptidId { get; set; }
  public string AccountId { get; set; }
}

public class CryptidEncounterProfile : Profile
{
  public int CryptidEncounterId { get; set; }
}

public class CryptidEncounterCryptid : Cryptid
{
  public int CryptidEncounterId { get; set; }
}