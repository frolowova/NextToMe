<template>
  <v-form
    class="login-form"
    @submit.prevent="onLogin"
    v-model="valid"
    ref="form"
  >
    <v-alert dense text type="error" v-if="error">{{ error }}</v-alert>
    <v-container class="mb-6">
      <v-text-field
        v-model="user.login"
        label="E-mail"
        :rules="[rules.required, rules.email]"
      ></v-text-field>
      <v-text-field
        v-model="user.password"
        label="Пароль"
        :rules="[rules.required]"
        type="password"
      ></v-text-field>
    </v-container>
    <v-btn
      :loading="loading"
      :disabled="loading || !valid"
      elevation="0"
      type="submit"
      color="primary"
      x-large
      block
    >
      Войти
    </v-btn>
  </v-form>
</template>

<script>
import { AUTH_LOGIN } from "@/store/actions/auth";

export default {
  layout: "auth",
  data: () => ({
    valid: true,
    user: {
      login: "",
      password: ""
    },
    rules: {
      required: v => !!v || "Обязательное поле",
      email: value =>
        !value ||
        /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(value) ||
        "Невалидный e-mail"
    },
    error: null,
    loading: false
  }),
  computed: {
    errorMessage() {
      return this.$store.errorMessage;
    }
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    onLogin() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        this.$store
          .dispatch(AUTH_LOGIN, { ...this.user })
          .then(res => {
            this.loading = false;
            this.$router.push("/about");
          })
          .catch(err => {
            console.log(err);
            if (err == 401) {
              this.error = "Неверный логин или пароль";
            } else {
              this.error = "Ошибка авторизации";
            }
            this.loading = false;
          });
      }
    }
  }
};
</script>
