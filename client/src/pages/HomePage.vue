<script setup>
import { cryptidsService } from '@/services/CryptidsService.js';
import { logger } from '@/utils/Logger.js';
import { Pop } from '@/utils/Pop.js';
import { onMounted } from 'vue';

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
  <div>sup</div>
</template>

<style scoped lang="scss"></style>
