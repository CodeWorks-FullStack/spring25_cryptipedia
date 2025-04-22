

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
    string sql = @"
    SELECT
    cryptids.*,
    COUNT(cryptid_encounters.id) AS encounter_count,
    accounts.*
    FROM cryptids
    LEFT OUTER JOIN cryptid_encounters ON cryptid_encounters.cryptid_id = cryptids.id
    INNER JOIN accounts ON accounts.id = cryptids.discoverer_id
    GROUP BY cryptids.id
    ORDER BY cryptids.id ASC;";

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