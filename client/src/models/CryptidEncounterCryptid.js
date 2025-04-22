import { Cryptid } from "./Cryptid.js";

export class CryptidEncounterCryptid extends Cryptid {
  constructor(data) {
    super(data)
    this.cryptidEncounterId = data.cryptidEncounterId
  }
}