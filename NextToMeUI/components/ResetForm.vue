<template>
  <v-form
    class="confirm-form"
    @submit.prevent="onReset"
    v-model="valid"
    ref="form"
  >
    <v-alert dense text type="error" v-if="errorMessage">{{
      errorMessage
    }}</v-alert>
    <v-alert dense text type="success" v-if="successMessage">{{
      successMessage
    }}</v-alert>
    <v-container class="mb-6">
      <v-text-field
        v-model="login"
        label="E-mail"
        type="text"
        :rules="[rules.required]"
      ></v-text-field>
    </v-container>
    <v-btn
      :loading="loading"
      :disabled="loading"
      type="submit"
      color="primary"
      x-large
      block
    >
      Сбросить пароль
    </v-btn>
  </v-form>
</template>

<script>
import { RESET_PASSWORD_ATTEMPT } from "@/store/actions/auth";

export default {
  layout: "auth",
  data: () => ({
    valid: true,
    login: "",
    rules: {
      required: v => !!v || "Обязательное поле",
      min: v => v.length >= 6 || "Пароль должен содержать > 6 символов",
      password: v =>
        /((?=.*\d)(?=.*[A-Z]).{6,6})/.test(v) || "Невалидный пароль"
    },
    errorMessage: null,
    successMessage: null,
    loading: false
  }),
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    onReset() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        this.$store
          .dispatch(RESET_PASSWORD_ATTEMPT, {
            login: this.login
          })
          .then(res => {
            this.loading = false;
            this.successMessage =
              "Успешно. Вам на почту было отправлено письмо для сброса пароля";
          })
          .catch(err => {
            this.loading = false;
            this.errorMessage = "Проверьте правильность введенного логина";
          });
      }
    }
  }
};
</script>
