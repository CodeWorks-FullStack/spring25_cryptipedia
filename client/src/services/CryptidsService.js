import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Cryptid } from "@/models/Cryptid.js"
import { AppState } from "@/AppState.js"

class CryptidsService {
  async getCryptids() {
    const response = await api.get('api/cryptids')
    logger.log('GOT CRYPTIDS', response.data)
    const cryptids = response.data.map(pojo => new Cryptid(pojo))
    AppState.cryptids = cryptids
  }
}

// SINGLETON
export const cryptidsService = new CryptidsService()