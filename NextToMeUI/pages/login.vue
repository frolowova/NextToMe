<template>
  <div class="login-page">
    <span class="auth__descr">Вход</span>
    <loginForm class="mb-6" />
    <v-btn nuxt to="/signup" block elevation="2" x-large outlined class="mb-4"
      >Зарегистрироваться</v-btn
    >
    <nuxt-link class="login-page__link" to="/reset">Забыли пароль</nuxt-link>
  </div>
</template>

<script>
import loginForm from "@/components/Auth/LoginForm";

export default {
  middleware: ["notAuth"],
  layout: "auth",
  components: {
    loginForm
  }
};
</script>

<style style="scss" scoped>
.login-page {
  max-width: 640px;
  width: 100%;
}

.login-page__link {
  text-decoration: none;
  text-transform: uppercase;
  text-align: center;
  display: block;
}

.login-page__link:hover {
  text-decoration: underline;
}

.auth__descr {
  display: block;
  font-size: 1.25rem;
  text-align: center;
  font-family: Roboto;
  letter-spacing: 0.5px;
  opacity: 0.8;
}
</style>
