using System.ComponentModel.DataAnnotations;

namespace cryptipedia.Models;

public class Cryptid : RepoItem<int>
{
  public string Name { get; set; }
  [Range(0, 10)] public int ThreatLevel { get; set; }
  [Range(0, 10)] public int Size { get; set; }
  [Url, MaxLength(2000)] public string ImgUrl { get; set; }
  public string Origin { get; set; }
  public string Description { get; set; }
  public string DiscovererId { get; set; }
  public Profile Discoverer { get; set; }
  public int EncounterCount { get; set; }
}