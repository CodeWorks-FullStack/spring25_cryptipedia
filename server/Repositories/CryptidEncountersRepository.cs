




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

  internal List<CryptidEncounterCryptid> GetCryptidEncountersByAccountId(string accountId)
  {
    // string sql = @"
    // SELECT 
    // cryptid_encounters.*,
    // cryptids.*,
    // accounts.*
    // FROM cryptid_encounters 
    // INNER JOIN cryptids ON cryptids.id = cryptid_encounters.cryptid_id
    // INNER JOIN accounts ON accounts.id = cryptids.discoverer_id
    // WHERE account_id = @accountId;";

    // List<CryptidEncounterCryptid> cryptids = _db.Query(sql,
    // (CryptidEncounter cryptidEncounter, CryptidEncounterCryptid cryptid, Profile account) =>
    // {
    //   cryptid.CryptidEncounterId = cryptidEncounter.Id;
    //   cryptid.Discoverer = account;
    //   return cryptid;
    // }, new { accountId }).ToList();

    // string sql = @"
    // SELECT 
    // cryptids.*,
    // cryptid_encounters.id AS cryptid_encounter_id,
    // accounts.*
    // FROM cryptid_encounters 
    // INNER JOIN cryptids ON cryptids.id = cryptid_encounters.cryptid_id
    // INNER JOIN accounts ON accounts.id = cryptids.discoverer_id
    // WHERE account_id = @accountId;";

    string sql = @"
    SELECT 
    cryptids_with_encounter_count_view.*,
    cryptid_encounters.id AS cryptid_encounter_id,
    accounts.*
    FROM cryptid_encounters 
    INNER JOIN cryptids_with_encounter_count_view ON cryptids_with_encounter_count_view.id = cryptid_encounters.cryptid_id
    INNER JOIN accounts ON accounts.id = cryptids_with_encounter_count_view.discoverer_id
    WHERE account_id = @accountId;";

    List<CryptidEncounterCryptid> cryptids = _db.Query(sql,
    (CryptidEncounterCryptid cryptid, Profile account) =>
    {
      cryptid.Discoverer = account;
      return cryptid;
    }, new { accountId }).ToList();

    return cryptids;
  }

  internal CryptidEncounter GetCryptidEncounterById(int cryptidEncounterId)
  {
    string sql = "SELECT * FROM cryptid_encounters WHERE id = @cryptidEncounterId;";

    CryptidEncounter cryptidEncounter = _db.Query<CryptidEncounter>(sql, new { cryptidEncounterId }).SingleOrDefault();

    return cryptidEncounter;
  }

  internal void DeleteCryptidEncounter(int cryptidEncounterId)
  {
    string sql = "DELETE FROM cryptid_encounters WHERE id = @cryptidEncounterId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { cryptidEncounterId });

    if (rowsAffected == 1) return;

    throw new Exception(rowsAffected + " ROWS HAVE BEEN DELETED, AND THAT IS BAD");
  }
}