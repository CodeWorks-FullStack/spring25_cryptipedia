<script setup>
import { AppState } from '@/AppState.js';
import { cryptidEncountersService } from '@/services/CryptidEncountersService.js';
import { cryptidsService } from '@/services/CryptidsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const cryptid = computed(() => AppState.activeCryptid)
const humans = computed(() => AppState.cryptidEncounterProfiles)
const account = computed(() => AppState.account)

const route = useRoute()

onMounted(() => {
  getCryptidById()
  getCryptidEncounterProfilesByCryptidId()
})

async function getCryptidById() {
  try {
    await cryptidsService.getCryptidById(route.params.cryptidId)
  } catch (error) {
    Pop.error(error, 'COULD NOT GET CRYPTID')
    logger.error('COULD NOT GET CRYPTID', error)
  }
}

async function getCryptidEncounterProfilesByCryptidId() {
  try {
    await cryptidEncountersService.getCryptidEncounterProfilesByCryptidId(route.params.cryptidId)
  } catch (error) {
    Pop.error(error, 'COULD NOT GET CRYPTID ENCOUNTERS')
    logger.error('COULD NOT GET CRYPTID ENCOUNTERS', error)
  }
}

async function createCryptidEncounter() {
  try {
    const cryptidEncounterData = {
      cryptidId: route.params.cryptidId
    }
    await cryptidEncountersService.createCryptidEncounter(cryptidEncounterData)
  } catch (error) {
    Pop.error(error, 'COULD NOT CREATE CRYPTID ENCOUNTER')
    logger.error('COULD NOT CREATE CRYPTID ENCOUNTER', error)
  }
}
</script>


<template>
  <div v-if="cryptid" class="container-fluid bg-dark">
    <div class="row">
      <div class="col-md-9">
        <div class="p-2 text-light italiana-font">
          <span class="text-capitalize text-warning fs-1">{{ cryptid.origin }} Cryptid</span>
          <h1 class="display-1">{{ cryptid.name.toUpperCase() }}</h1>
          <p class="ibm-plex-mono-font">{{ cryptid.description }}</p>
          <div>
            <span class="fs-2">Size</span>
            <div :title="`Size is ${cryptid.size}/10`" class="stats">
              <span v-for="num in cryptid.size" :key="num + ' size'" class="fs-2 mdi mdi-circle"></span>
              <span v-for="num in 10 - cryptid.size" :key="num + ' undersize'"
                class="fs-2 mdi mdi-circle-outline"></span>
            </div>
          </div>
          <div>
            <span class="fs-2">Threat Level</span>
            <div :title="`Threat level is ${cryptid.threatLevel}/10`" class="stats">
              <span v-for="num in cryptid.threatLevel" :key="num + ' threatLevel'" class="fs-2 mdi mdi-circle"></span>
              <span v-for="num in 10 - cryptid.threatLevel" :key="num + ' underthreatLevel'"
                class="fs-2 mdi mdi-circle-outline"></span>
            </div>
          </div>
          <div>
            <div class="d-flex flex-wrap gap-3 mb-3">
              <h2 class="text-warning mt-2">Encountered By {{ humans.length }} Humans</h2>
              <button @click="createCryptidEncounter()" v-if="account" class="btn btn-warning ibm-plex-mono-font">
                I've encountered the {{ cryptid.name }}
              </button>
            </div>
            <div class="d-flex gap-1 flex-wrap">
              <div v-for="human in humans" :key="human.cryptidEncounterId">
                <img :src="human.picture" :alt="human.name" :title="`${human.name} has encountered the ${cryptid.name}`"
                  class="human-picture">
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-3">
        <img :src="cryptid.imgUrl" :alt="'Blurry picture of the ' + cryptid.name"
          class="h-100 w-100 object-fit-cover cryptid-img">
      </div>
    </div>
  </div>
  <div v-else class="container-fluid bg-dark">
    <div class="row">
      <div class="col-12">
        <h1 class="text-light">Loading...</h1>
        <marquee direction="right" scrollamount="30">
          <span class="mdi mdi-pac-man fs-1 me-3 text-warning"></span>
          <span class="mdi mdi-ghost fs-1 text-primary"></span>
          <span class="mdi mdi-ghost fs-1 text-primary"></span>
          <span class="mdi mdi-ghost fs-1 text-primary"></span>
        </marquee>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.container-fluid {
  flex-grow: 1;
}

.cryptid-img {
  filter: blur(5px);
  transition: filter 3s ease-in-out;
}

.cryptid-img:hover {
  cursor: help;
  filter: blur(0);
}

.stats {
  width: fit-content;
}

.human-picture {
  width: 5em;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}
</style>