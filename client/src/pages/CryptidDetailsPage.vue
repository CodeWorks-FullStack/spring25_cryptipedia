<script setup>
import { AppState } from '@/AppState.js';
import { cryptidsService } from '@/services/CryptidsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';
import { useRoute } from 'vue-router';

const cryptid = computed(() => AppState.activeCryptid)

const route = useRoute()

onMounted(() => {
  getCryptidById()
})

async function getCryptidById() {
  try {
    await cryptidsService.getCryptidById(route.params.cryptidId)
  } catch (error) {
    Pop.error(error, 'COULD NOT GET CRYPTID')
    logger.error('COULD NOT GET CRYPTID', error)
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
            <div :title="`Size is ${cryptid.size}/10`">
              <span v-for="num in cryptid.size" :key="num + ' size'" class="fs-2 mdi mdi-circle"></span>
              <span v-for="num in 10 - cryptid.size" :key="num + ' undersize'"
                class="fs-2 mdi mdi-circle-outline"></span>
            </div>
          </div>
          <div>
            <span class="fs-2">Threat Level</span>
            <div :title="`Threat level is ${cryptid.threatLevel}/10`">
              <span v-for="num in cryptid.threatLevel" :key="num + ' threatLevel'" class="fs-2 mdi mdi-circle"></span>
              <span v-for="num in 10 - cryptid.threatLevel" :key="num + ' underthreatLevel'"
                class="fs-2 mdi mdi-circle-outline"></span>
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
  cursor: crosshair;
  filter: blur(0);
}
</style>