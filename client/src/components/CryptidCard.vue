<script setup>
import { Cryptid } from '@/models/Cryptid.js';

defineProps({
  cryptid: { type: Cryptid, required: true }
})
</script>


<template>
  <div class="ibm-plex-mono-font fw-bold cryptid-card text-light text-shadow"
    :style="{ backgroundImage: `url(${cryptid.imgUrl})` }">
    <div class="d-flex flex-column justify-content-between inner-content p-2">
      <div class="text-end">
        <span class="mdi mdi-ufo-outline me-1"></span>
        <span>0</span>
      </div>
      <div>
        <span>{{ cryptid.id }}</span>
        <hr>
        <span>{{ cryptid.name }}</span>
      </div>
    </div>
  </div>
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