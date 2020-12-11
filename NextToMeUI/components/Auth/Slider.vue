<template>
  <div class="slider-container">
    <v-carousel
      class="carousel"
      v-model="currentSlide"
      :continuous="false"
      :show-arrows="false"
      hide-delimiter-background
    >
      <sliderItem
        v-for="(slide, i) in slides"
        :key="i"
        :title="slide.title"
        :text="slide.text"
        :src="slide.image"
      />
    </v-carousel>

    <v-card-actions class="d-flex justify-space-between">
      <v-btn text @click="prev">
        {{ `${currentSlide == 0 ? "Пропустить" : "Назад"}` }}
      </v-btn>
      <v-btn text @click="next">
        Далее
      </v-btn>
    </v-card-actions>
  </div>
</template>

<script>
import sliderItem from "@/components/Auth/SliderItem";

export default {
  data: () => ({
    currentSlide: 0
  }),
  props: {
    slides: Array
  },
  components: {
    sliderItem
  },
  methods: {
    prev() {
      if (this.currentSlide == 0) {
        this.$router.push("/permissions");
        localStorage.setItem(
          "firstSeenSlider",
          this.$store.state.auth.username
        );
        return;
      }
      this.currentSlide--;
    },
    next() {
      if (this.currentSlide == this.slides.length - 1) {
        this.$router.push("/permissions");
        localStorage.setItem(
          "firstSeenSlider",
          this.$store.state.auth.username
        );
        return;
      }
      this.currentSlide++;
    }
  }
};
</script>

<style scoped>
.slider-container {
  max-width: 600px;
}

@media only screen and (max-height: 600px) and (orientation: landscape) {
  .carousel {
    max-height: 250px;
  }
}
</style>
