<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import FancyHeader from '@/components/FancyHeader.vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { cryptidEncountersService } from '@/services/CryptidEncountersService.js';
import CryptidCard from '@/components/CryptidCard.vue';

const account = computed(() => AppState.account)
const cryptids = computed(() => AppState.cryptidEncounterCryptids)

onMounted(() => {
  getMyCryptidEncounters()
})

async function getMyCryptidEncounters() {
  try {
    await cryptidEncountersService.getMyCryptidEncounters()
  } catch (error) {
    Pop.error(error, 'COULD NOT GET CRYPTID ENCOUNTERS')
    logger.error('COULD NOT GET CRYPTID ENCOUNTERS', error)
  }
}

async function deleteCryptidEncounter(cryptidEncounterId) {
  const confirmed = await Pop.confirm('Are you sure?')

  if (!confirmed) return

  try {
    await cryptidEncountersService.deleteCryptidEncounter(cryptidEncounterId)
  } catch (error) {
    Pop.error(error, 'COULD NOT DELETE')
    logger.error('COULD NOT DELETE', error)
  }
}

</script>

<template>
  <div v-if="account" class="container">
    <div class="row">
      <div class="col-12">
        <FancyHeader>
          Cryptids Encountered
        </FancyHeader>
      </div>
    </div>
    <div class="row">
      <div v-for="cryptid in cryptids" :key="cryptid.cryptidEncounterId" class="col-6 col-md-4 col-lg-3 px-0">
        <div class="position-relative">
          <CryptidCard :cryptid="cryptid" />
          <button @click="deleteCryptidEncounter(cryptid.cryptidEncounterId)"
            class="btn btn-outline-danger ibm-plex-mono-font position-absolute m-1">
            Un-encounter
          </button>
        </div>
      </div>
    </div>
  </div>
  <div v-else>
    <h1>Loading... <i class="mdi mdi-skull mdi-spin"></i></h1>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

button {
  top: 0
}
</style>
