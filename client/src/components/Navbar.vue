<script setup>
import { ref, watch } from 'vue';
import { loadState, saveState } from '../utils/Store.js';
import Login from './Login.vue';

const theme = ref(loadState('theme') || 'light')

function toggleTheme() {
  theme.value = theme.value == 'light' ? 'dark' : 'light'
}

watch(theme, () => {
  document.documentElement.setAttribute('data-bs-theme', theme.value)
  saveState('theme', theme.value)
}, { immediate: true })

</script>

<template>
  <nav class="navbar navbar-expand-md bg-dark">
    <div class="container gap-2">
      <RouterLink :to="{ name: 'Home' }" class="d-flex align-items-center text-light italiana-font fs-1">
        <span>Cryptipedia</span>
      </RouterLink>
      <!-- collapse button -->
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbar-links"
        aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
        <span class="mdi mdi-menu text-light"></span>
      </button>
      <!-- collapsing menu -->
      <div class="collapse navbar-collapse " id="navbar-links">
        <ul class="navbar-nav">
        </ul>
        <!-- LOGIN COMPONENT HERE -->
        <div class="ms-auto">

        </div>
        <Login />
      </div>
    </div>
  </nav>
</template>

<style lang="scss" scoped>
a {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
</style>
