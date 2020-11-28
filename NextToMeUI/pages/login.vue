<template>
  <div class="login-page">
    <span class="auth__descr">Вход</span>
    <loginForm class="mb-6"/>
    <linkButton to='/signup' text='Зарегистрироваться' class="mb-4"/>
    <nuxt-link class="login-page__link" to='/reset'>Забыли пароль</nuxt-link>
  </div>
</template>

<script>
import loginForm from "@/components/Auth/LoginForm";
import linkButton from "@/components/Auth/LinkButton";

export default {
  middleware: ['notAuth'],
  layout: "auth",
  components: {
    loginForm,
    linkButton
  },
  
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
  color: #ffffff;
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
  color: #ffffff;
  opacity: 0.8;
}
</style>