<template>
  <v-form
    class="signup-form"
    @submit.prevent="onSignup"
    v-model="valid"
    ref="form"
  >
    <v-alert dense text type="success" v-if="successMessage">{{
      successMessage
    }}</v-alert>
    <v-alert dense text type="error" v-if="errorMessage">{{
      errorMessage
    }}</v-alert>
    <v-container class="mb-6">
      <v-text-field v-model="user.name" label="Имя"></v-text-field>
      <v-text-field
        v-model="user.email"
        label="E-mail"
        :rules="[rules.required, rules.email]"
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
      Зарегистрироваться
    </v-btn>
  </v-form>
</template>

<script>
import { AUTH_SIGNUP } from "@/store/actions/auth";

export default {
  layout: "auth",
  data: () => ({
    valid: true,
    user: {
      name: "",
      email: ""
    },
    rules: {
      required: v => !!v || "Обязательное поле",
      email: value =>
        !value ||
        /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(value) ||
        "Невалидный e-mail"
    },
    errorMessage: null,
    successMessage: null,
    loading: false
  }),
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    onSignup() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        this.$store
          .dispatch(AUTH_SIGNUP, { ...this.user })
          .then(res => {
            this.loading = false;
            this.successMessage = "Успешно. Подтвердите почту, пожалуйста";
            this.errorMessage = null;
          })
          .catch(err => {
            this.loading = false;
            if (err == 400) {
              this.errorMessage = "Такой логин уже зарегистрирован";
            } else {
              this.errorMessage = "Ошибка регистрации";
            }
            this.successMessage = null;
          });
      }
    }
  }
};
</script>
