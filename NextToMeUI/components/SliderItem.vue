<template>
  <v-carousel-item>
    <v-container
      class="slider-item d-flex flex-column align-center justify-center"
    >
      <img :src="src" alt="slider-icon" />
      <v-card-title class="title">{{ title }}</v-card-title>
      <v-card-text class="text-center desc">
        <div>
          {{ text }}
        </div>
      </v-card-text>
    </v-container>
  </v-carousel-item>
</template>

<script>
export default {
  props: {
    title: String,
    text: String,
    src: String
  }
};
</script>

<style scoped>

.slider-item {
  height: 100%;
}
.slider-item img {
  width: 60%;
  height: auto;
}

@media only screen and (max-height: 600px) and (orientation: landscape) {
  .slider-item img {
    width: 20%;
  }
  .slider-item {
    height: auto;
  }
}
</style>
