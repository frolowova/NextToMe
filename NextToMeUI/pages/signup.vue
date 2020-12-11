<template>
  <div class="signup-page">
    <span class="auth__descr">Регистрация</span>
    <signupForm class="mb-6" />
    <v-btn nuxt to="/login" block elevation="2" x-large outlined class="mb-4"
      >Войти</v-btn
    >
  </div>
</template>

<script>
import signupForm from "@/components/Auth/SignupForm";

export default {
  middleware: ["notAuth"],
  layout: "auth",
  components: {
    signupForm
  }
};
</script>

<style scoped>
.signup-page {
  max-width: 640px;
  width: 100%;
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
