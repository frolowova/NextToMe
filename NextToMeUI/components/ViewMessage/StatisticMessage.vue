<template>
  <div class="d-flex justify-space-between mt-2">
    <div class="ml-4 d-flex">
      <Bomb :time="time" />
      <p class="ml-2">{{ timeView(time) }}</p>
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
  },
  methods: {
    timeView() {
      const time = Date.parse(this.time) - Date.now();
      const seconds = Math.floor(time / 1000);
      if (seconds < 60) {
        return `${seconds} секунд`;
      }
      const minutes = Math.floor(seconds / 60);
      if (minutes < 60) {
        return `${minutes} минут`;
      }
      const hours = Math.floor(minutes / 60);
      if (hours < 24) {
        return `${hours} часов`;
      }
      const days = Math.floor(hours / 24);
      return `${days} деней`;
    },
  },
};
</script>