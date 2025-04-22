

namespace cryptipedia.Repositories;

public class CryptidEncountersRepository
{
  public CryptidEncountersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal CryptidEncounterProfile CreateCryptidEncounter(CryptidEncounter cryptidEncounterData)
  {
    // string sql = @"
    // INSERT INTO
    // cryptid_encounters(account_id, cryptid_id)
    // VALUES(@AccountId, @CryptidId);

    // SELECT
    // cryptid_encounters.*,
    // accounts.*,
    // FROM cryptid_encounters 
    // INNER JOIN accounts ON accounts.id = cryptid_encounters.account_id
    // WHERE cryptid_encounters.id = LAST_INSERT_ID();";

    // CryptidEncounterProfile cryptidEncounterProfile = _db.Query(sql,
    // (CryptidEncounter cryptidEncounter, CryptidEncounterProfile account) =>
    // {
    //   account.CryptidEncounterId = cryptidEncounter.Id;
    //   return account;
    // }, cryptidEncounterData).SingleOrDefault();

    string sql = @"
    INSERT INTO
    cryptid_encounters(account_id, cryptid_id)
    VALUES(@AccountId, @CryptidId);
    
    SELECT
    accounts.*,
    cryptid_encounters.id AS cryptid_encounter_id
    FROM cryptid_encounters 
    INNER JOIN accounts ON accounts.id = cryptid_encounters.account_id
    WHERE cryptid_encounters.id = LAST_INSERT_ID();";

    CryptidEncounterProfile cryptidEncounterProfile = _db.Query<CryptidEncounterProfile>(sql, cryptidEncounterData).SingleOrDefault();

    return cryptidEncounterProfile;
  }

  internal List<CryptidEncounterProfile> GetCryptidEncounterProfilesByCryptidId(int cryptidId)
  {
    string sql = @"
    SELECT
    accounts.*,
    cryptid_encounters.id AS cryptid_encounter_id
    FROM cryptid_encounters 
    INNER JOIN accounts ON accounts.id = cryptid_encounters.account_id
    WHERE cryptid_encounters.cryptid_id = @cryptidId;";

    List<CryptidEncounterProfile> cryptidEncounterProfiles = _db.Query<CryptidEncounterProfile>(sql, new { cryptidId }).ToList();

    return cryptidEncounterProfiles;
  }
}