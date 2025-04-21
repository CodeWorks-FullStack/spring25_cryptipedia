import { Profile } from "./Account.js";
import { DatabaseItem } from "./DatabaseItem.js";

export class Cryptid extends DatabaseItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.threatLevel = data.threatLevel
    this.size = data.size
    this.imgUrl = data.imgUrl
    this.origin = data.origin
    this.description = data.description
    this.discovererId = data.discovererId
    this.discoverer = new Profile(data.discoverer)
  }
}