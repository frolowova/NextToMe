<template>
  <div class="d-flex justify-space-between mt-2">
    <div class="ml-4 d-flex">
      <Bomb :timeUpload="new Date()" />
      <p class="ml-2">{{ time }}</p>
    </div>
    <div class="mr-4 d-flex">
      <eye />
      <p class="ml-2">{{ view }}</p>
    </div>
  </div>
</template>

<script>
import Bomb from "@/components/Bomb/Bomb";
export default {
  props: {
    time: String,
    view: String,
  },
};
</script>