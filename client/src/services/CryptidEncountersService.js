import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { CryptidEncounterProfile } from "@/models/CryptidEncounterProfile.js"
import { AppState } from "@/AppState.js"
import { CryptidEncounterCryptid } from "@/models/CryptidEncounterCryptid.js"

class CryptidEncountersService {
  async getCryptidEncounterProfilesByCryptidId(cryptidId) {
    AppState.cryptidEncounterProfiles.length = 0
    const response = await api.get(`api/cryptids/${cryptidId}/cryptidEncounters`)
    logger.log('GOT CRYPTID ENCOUNTERS', response.data)
    const profiles = response.data.map(pojo => new CryptidEncounterProfile(pojo))
    AppState.cryptidEncounterProfiles = profiles
  }

  async getMyCryptidEncounters() {
    const response = await api.get('account/cryptidEncounters')
    logger.log('GOT MY CRYPTID ENCOUNTERS', response.data)
    const cryptids = response.data.map(pojo => new CryptidEncounterCryptid(pojo))
    AppState.cryptidEncounterCryptids = cryptids
  }

  async createCryptidEncounter(cryptidEncounterData) {
    const response = await api.post('api/cryptidEncounters', cryptidEncounterData)
    logger.log('ENCOUNTERED CRYPTID', response.data)
    const profile = new CryptidEncounterProfile(response.data)
    AppState.cryptidEncounterProfiles.push(profile)
  }


  async deleteCryptidEncounter(cryptidEncounterId) {
    const response = await api.delete(`api/cryptidEncounters/${cryptidEncounterId}`)
    logger.log('DELETED!', response.data)
    const cryptidEncounters = AppState.cryptidEncounterCryptids
    const index = cryptidEncounters.findIndex(cryptid => cryptid.cryptidEncounterId == cryptidEncounterId)

    const cryptidEncounter = cryptidEncounters[index]

    cryptidEncounters.forEach(cryptid => {
      if (cryptid.id != cryptidEncounter.id) return
      cryptid.encounterCount--
    });

    cryptidEncounters.splice(index, 1)
  }

}

export const cryptidEncountersService = new CryptidEncountersService()