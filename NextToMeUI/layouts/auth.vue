<template>
  <v-app>
    <div class="auth-outer-container">
      <div class="auth-inner-container d-flex flex-column">
        <div class="background-container">
          <img class="back" src="~assets/back-1.svg" />
          <img class="front" src="~assets/back-2.svg" />
        </div>
        <div class="auth__logo">
          <img src="~assets/logo.svg" alt="auth__logo" />
        </div>
        <div class="auth__content d-flex justify-center align-center">
          <nuxt />
        </div>
      </div>
    </div>
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

<style style="sass" scoped>
.auth__logo {
  position: fixed;
  top: 1rem;
  left: 1rem;
}

.auth-inner-container {
  width: 100%;
  height: 100vh;
  overflow: hidden;
}

.auth-outer-container {
  height: 100%;
  width: 100%;
  position: relative;
}

.background-container {
  width: 100%;
  position: relative;
}

.back {
  width: 100%;
}

.front {
  position: absolute;
  top: 0;
  right: -5%;
  width: 130%;
}

.auth__content {
  width: 100%;
  padding: 1rem;
  position: relative;
}


@media only screen and (max-height: 550px) {
  .background-container {
    display: none;
  }

  .auth-inner-container {
    justify-content: center;
    align-content: center;
    height: 100%;
    overflow-y: visible;
  }
}

@media only screen and (max-height: 550px) and (orientation: landscape) {
  .auth__content {
    position: static;
  }
}

@media only screen and (max-height: 768px) and (orientation: landscape) {
  .auth-inner-container {
    height: 100%;
    overflow-y: visible;
  }
}
@media only screen and (min-width: 600px) and (orientation: portreit) {
  .background-container {
    top: -15%;
  }
  .auth__content {
    top: -15%;
  }
}

@media only screen and (min-width: 768px) {
  .background-container {
    top: -15%;
  }
  .auth__content {
    top: -15%;
  }
}

@media only screen and (min-width: 992px) {
  .background-container {
    top: -25%;
  }
  .auth__content {
    top: -25%;
  }
}

@media only screen and (min-width: 1200px) {
  .background-container {
    top: -45%;
  }
  .auth__content {
    top: -45%;
  }
}
</style>
