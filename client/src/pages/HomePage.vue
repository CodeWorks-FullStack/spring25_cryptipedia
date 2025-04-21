<script setup>
import { AppState } from '@/AppState.js';
import CryptidCard from '@/components/CryptidCard.vue';
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
  <div class="container-fluid hero">
    <div class="row hero-row">
      <div class="col-md-8 align-self-center">
        <div class="text-light text-shadow ps-5">
          <h2>Terrestrials</h2>
          <p>
            A terrestrial cryptid is a creature that exists on land but has not been “scientifically” proven. These
            creatures often stem from folklore, mythology, or anecdotal evidence. Unlike aquatic cryptids, like the Loch
            Ness Monster, terrestrial cryptids inhabit forests, mountains, or other land-based environments.
          </p>
        </div>
      </div>
      <div class="col-md-4 px-0 align-self-end">
        <img
          src="https://assets.petco.com/petco/image/upload/f_auto,q_auto:best/catgrooming-pricing-longhair-bath-cut-2.png"
          alt="emerald entity" class="img-fluid emerald-entity">
      </div>
    </div>
  </div>
  <div class="container">
    <div class="row">
      <div class="col-12">
        <div class="italiana-font my-4 position-relative">
          <div>
            <span class="text-warning bg-text">CRYPTIDS</span>
          </div>
          <h1 class="position-absolute">CRYPTIDS</h1>
        </div>
      </div>
    </div>
    <div class="row">
      <div v-for="cryptid in cryptids" :key="cryptid.id" class="col-6 col-md-4 col-lg-3 px-0">
        <CryptidCard :cryptid="cryptid" />
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.hero {
  background-image: url(https://images.unsplash.com/photo-1574558452538-7477c4a40b83?q=80&w=2340&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D);
  background-size: cover;


  .hero-row {
    min-height: 70dvh;
    background: #ababab;
    background: linear-gradient(0deg, rgba(171, 171, 171, 0.80) 0%, rgba(19, 17, 20, 1) 100%);
  }
}

.emerald-entity {
  filter: hue-rotate(45deg);
}

.bg-text {
  font-size: 6rem;
  user-select: none;
}

h1 {
  top: 3.25rem;
  left: .4rem;
}
</style>
