<script setup>
import Navbar from './components/Navbar.vue';
import { RouterView } from 'vue-router';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <RouterView />
  </main>
  <footer class="text-center bg-dark text-light">
    Made with <i class="mdi mdi-heart text-warning"></i> by CodeWorks
  </footer>
</template>

<style lang="scss"></style>
