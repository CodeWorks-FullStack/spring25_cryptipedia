<script setup>
import { AppState } from '@/AppState.js';
import { cryptidsService } from '@/services/CryptidsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

const cryptids = computed(() => AppState.cryptids)

onMounted(() => {
  getCryptids()
})

async function getCryptids() {
  try {
    await cryptidsService.getCryptids()
  } catch (error) {
    Pop.error(error, 'Could not get cryptids')
    logger.error('COULD NOT GET CRYPTIDS', error)
  }
}

</script>

<template>
  <div>{{ cryptids }}</div>
</template>

<style scoped lang="scss"></style>
