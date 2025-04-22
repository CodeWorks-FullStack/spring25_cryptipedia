

namespace cryptipedia.Repositories;

public class CryptidsRepository
{
  public CryptidsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal List<Cryptid> GetCryptids()
  {
    // REVIEW all of this commented out code will run just fine

    // string sql = @"
    // SELECT
    // cryptids.*,
    // COUNT(cryptid_encounters.id) AS encounter_count,
    // accounts.*
    // FROM cryptids
    // LEFT OUTER JOIN cryptid_encounters ON cryptid_encounters.cryptid_id = cryptids.id
    // INNER JOIN accounts ON accounts.id = cryptids.discoverer_id
    // GROUP BY cryptids.id
    // ORDER BY cryptids.id ASC;";

    // NOTE selecting from view 
    string sql = @"
    SELECT
    cryptids_with_encounter_count_view.*,
    accounts.*
    FROM cryptids_with_encounter_count_view
    INNER JOIN accounts ON accounts.id = cryptids_with_encounter_count_view.discoverer_id
    ORDER BY cryptids_with_encounter_count_view.id ASC;";

    List<Cryptid> cryptids = _db.Query(sql, (Cryptid cryptid, Profile account) =>
    {
      cryptid.Discoverer = account;
      return cryptid;
    }).ToList();
    return cryptids;
  }

  internal Cryptid GetCryptidById(int cryptidId)
  {
    string sql = @"
    SELECT
    cryptids.*,
    accounts.*
    FROM cryptids
    INNER JOIN accounts ON accounts.id = cryptids.discoverer_id
    WHERE cryptids.id = @cryptidId;";

    Cryptid foundCryptid = _db.Query(sql, (Cryptid cryptid, Profile account) =>
    {
      cryptid.Discoverer = account;
      return cryptid;
    }, new { cryptidId }).SingleOrDefault();

    return foundCryptid;
  }
}