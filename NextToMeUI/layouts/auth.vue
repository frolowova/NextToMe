<template>
  <v-app>
    <v-container
      class="auth-container d-flex flex-column justify-center align-center"
    >
      <nuxt />
    </v-container>
  </v-app>
</template>

<script>
export default {
  computed: {
    description() {
      if (this.$router.currentRoute.path == "/login") {
        return "Вход";
      } else {
        return "Регистрация";
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.auth-container {
  position: relative;
  overflow: hidden;
  height: 100%;
}

.svg-container {
  position: fixed;
  top: 0;
  height: 25%;
  width: 100%;
  overflow: hidden;
}

.svg-wave {
  position: absolute;
  top: 0;
  right: -60px;
}
</style>
