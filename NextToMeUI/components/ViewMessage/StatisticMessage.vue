<template>
  <div class="d-flex justify-space-between mt-2">
    <div class="ml-4 d-flex">
      <Bomb :deleteTime="time" />
    </div>
    <div class="mr-4 d-flex">
      <eye :views="view"/>
    </div>
  </div>
</template>

<script>
import Bomb from "@/components/Bomb/Bomb";
export default {
  props: {
    time: String,
    view: Number,
  }
};
</script>