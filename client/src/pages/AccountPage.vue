<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import FancyHeader from '@/components/FancyHeader.vue';
import { Pop } from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { cryptidEncountersService } from '@/services/CryptidEncountersService.js';

const account = computed(() => AppState.account)

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
  </div>
  <div v-else>
    <h1>Loading... <i class="mdi mdi-skull mdi-spin"></i></h1>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}
</style>
