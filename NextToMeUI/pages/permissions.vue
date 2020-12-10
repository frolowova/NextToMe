<template>
  <div class="permisson-page d-flex align-center">
    <geoAttemptForm />
  </div>
</template>

<script>
import geoAttemptForm from "@/components/Geolocation/GeoAttemptForm.vue";
export default {
  middleware: ["auth", "geoNotGranted"],
  layout: "auth",
  components: { geoAttemptForm }
};
</script>

<style scoped>
.permisson-page {
  position: fixed;
  top: 0;
  height: 100%;
}
</style>
