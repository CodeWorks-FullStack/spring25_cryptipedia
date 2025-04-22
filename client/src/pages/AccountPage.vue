<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import FancyHeader from '@/components/FancyHeader.vue';

const account = computed(() => AppState.account)

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
