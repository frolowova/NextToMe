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
