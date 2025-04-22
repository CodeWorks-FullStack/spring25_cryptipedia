<script setup>

</script>


<template>
  <div class="italiana-font my-4 position-relative">
    <div>
      <span class="text-warning bg-text">
        <slot></slot>
      </span>
    </div>
    <h1 class="position-absolute">
      <slot></slot>
    </h1>
  </div>
</template>


<style lang="scss" scoped>
.bg-text {
  font-size: 6rem;
  user-select: none;
}

.position-relative {
  overflow-x: hidden;
}

h1 {
  top: 3.25rem;
  left: .4rem;
}
</style>