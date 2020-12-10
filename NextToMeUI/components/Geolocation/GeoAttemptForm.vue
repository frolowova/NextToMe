<template>
  <div class="permission-container">
    <v-container
      class="permission-item d-flex flex-column align-center justify-center"
    >
      <img :src="icon" alt="slider-icon" />
      <v-card-text class="text-center desc">
        <div>
          Next to me не может работать без геолокации. Включив геолокацию, вы
          сможете просматривать чужие теги поблизости и публиковать свои
        </div>
      </v-card-text>
      <v-btn
        type="submit"
        color="primary"
        x-large
        block
        @click="onGeoAttempt"
        class="rounded-lg"
      >
        Включить геолокацию
      </v-btn>
    </v-container>
    <geo-prompt :show="showPropmt" @show-propmt="promptShow" />
  </div>
</template>

<script>
import geoIcon from "@/assets/geo.svg";
import GeoPrompt from "./GeoPrompt.vue";

export default {
  data: () => ({
    icon: geoIcon,
    showPropmt: false
  }),
  methods: {
    promptShow() {
      this.showPropmt = !this.showPropmt;
    },
    onGeoAttempt() {
      navigator.permissions
        .query({
          name: "geolocation"
        })
        .then(res => {
          res.onchange = e => {
            this.showPropmt = false;
            if (e.target.state == "granted") {
              this.$router.push("/home");
            }
          };
          if (res.state == "prompt") {
            this.showPropmt = true;
          } else {
            this.$router.push("/instructions");
          }
        });
    }
  },
  components: {
    GeoPrompt
  }
};
</script>

<style scoped>
.permission-item {
  height: 100%;
}
.permission-item img {
  width: 60%;
  height: auto;
}

@media only screen and (max-height: 600px) and (orientation: landscape) {
  .permission-item img {
    width: 20%;
  }
  .permission-item {
    height: auto;
  }
}

.permission-container {
  max-width: 600px;
}
</style>
