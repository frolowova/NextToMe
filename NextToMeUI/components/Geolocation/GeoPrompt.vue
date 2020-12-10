<template>
  <v-overlay :value="show">
    <v-card class="mx-auto my-12" max-width="270">
      <div class="d-flex justify-center pa-4 primary">
        <svg width="48" height="48" fill="none">
          <path
            d="M24 4c-7.74 0-14 6.26-14 14 0 3.48 1 6.74 2.82 9.68 1.9 3.08 4.4 5.72 6.32 8.8.94 1.5 1.62 2.9 2.34 4.52.52 1.1.94 3 2.52 3s2-1.9 2.5-3c.74-1.62 1.4-3.02 2.34-4.52 1.92-3.06 4.42-5.7 6.32-8.8C37 24.74 38 21.48 38 18c0-7.74-6.26-14-14-14zm0 19.5c-2.76 0-5-2.24-5-5s2.24-5 5-5 5 2.24 5 5-2.24 5-5 5z"
            fill="#fff"
          />
        </svg>
      </div>
      <v-card-text>
        <v-row align="center" class="mx-0">
          Для использования NextToMe, разрешите доступ к вашему местоположению.
          Вы готовы это сделать?
        </v-row>
      </v-card-text>

      <v-card-actions class="justify-end">
        <v-btn text @click="$emit('show-propmt')">
          Не сейчас
        </v-btn>
        <v-btn text @click="getGeoAccess">
          Ок
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-overlay>
</template>

<script>
export default {
  props: {
    show: Boolean
  },
  methods: {
    getGeoAccess() {
      navigator.geolocation.getCurrentPosition(() => {
        console.log("Success");
      });
    }
  }
};
</script>
