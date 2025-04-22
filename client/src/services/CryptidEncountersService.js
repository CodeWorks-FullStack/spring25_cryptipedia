import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { CryptidEncounterProfile } from "@/models/CryptidEncounterProfile.js"
import { AppState } from "@/AppState.js"

class CryptidEncountersService {
  async getCryptidEncounterProfilesByCryptidId(cryptidId) {
    AppState.cryptidEncounterProfiles.length = 0
    const response = await api.get(`api/cryptids/${cryptidId}/cryptidEncounters`)
    logger.log('GOT CRYPTID ENCOUNTERS', response.data)
    const profiles = response.data.map(pojo => new CryptidEncounterProfile(pojo))
    AppState.cryptidEncounterProfiles = profiles
  }
}

export const cryptidEncountersService = new CryptidEncountersService()