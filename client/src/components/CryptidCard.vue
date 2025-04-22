<script setup>
import { Cryptid } from '@/models/Cryptid.js';

defineProps({
  cryptid: { type: Cryptid, required: true }
})
</script>


<template>
  <RouterLink :to="{ name: 'Cryptid Details', params: { cryptidId: cryptid.id } }"
    :title="'See details for the ' + cryptid.name">
    <div class="ibm-plex-mono-font fw-bold cryptid-card text-light text-shadow mb-5"
      :style="{ backgroundImage: `url(${cryptid.imgUrl})` }">
      <div class="d-flex flex-column justify-content-between inner-content p-2">
        <div class="text-end">
          <span :title="`The ${cryptid.name} has been encountered ${cryptid.encounterCount} times`">
            <span class="mdi mdi-ufo-outline me-1"></span>
            <span>{{ cryptid.encounterCount }}</span>
          </span>
        </div>
        <div>
          <span> {{ cryptid.id < 10 ? '0' + cryptid.id : cryptid.id }} </span>
              <hr>
              <span>{{ cryptid.name }}</span>
        </div>
      </div>
    </div>
  </RouterLink>
</template>


<style lang="scss" scoped>
.cryptid-card {
  background-size: cover;
}

.inner-content {
  min-height: 45.6dvh;
  backdrop-filter: blur(3px);
}
</style>