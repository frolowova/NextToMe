<template>
  <div class="d-flex justify-space-between mt-2">
    <div class="ml-4 d-flex">
      <bomb />
      <p class="ml-2">{{ time }}</p>
    </div>
    <div class="mr-4 d-flex">
      <eye />
      <p class="ml-2">{{ view }}</p>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    time: String,
    view: String,
  },
};
</script>