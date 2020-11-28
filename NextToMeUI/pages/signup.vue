<template>
  <div class="signup-page">
    <span class="auth__descr">Регистрация</span>
    <signupForm class="mb-6"/>
    <linkButton to='/login' text='Войти'/>
  </div>
</template>

<script>
import signupForm from "@/components/Auth/SignupForm";
import linkButton from "@/components/Auth/LinkButton";

export default {
  middleware: ['notAuth'],
  layout: "auth",
  components: {
    signupForm,
    linkButton
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
  color: #ffffff;
  opacity: 0.8;
}
</style>