<template>
  <v-form
    class="confirm-form"
    @submit.prevent="onConfirm"
    v-model="valid"
    ref="form"
  >
    <v-alert dense text type="error" v-if="errorMessage">{{
      errorMessage
    }}</v-alert>
    <v-container class="mb-6">
      <v-text-field
        v-model="password"
        label="Пароль"
        type="password"
        :rules="[rules.required, rules.min, rules.password]"
      ></v-text-field>
      <v-text-field
        v-model="repeatPassword"
        label="Подтвердите пароль"
        type="password"
        :rules="[rules.required, passwordsMatch]"
      ></v-text-field>
    </v-container>
    <v-btn type="submit" color="primary" x-large block>
      Подтвердить
    </v-btn>
  </v-form>
</template>

<script>
import { SIGNUP_CONFIRM } from "@/store/actions/auth";

export default {
  layout: "auth",
  data: () => ({
    valid: true,
    password: "",
    repeatPassword: "",
    rules: {
      required: v => !!v || "Обязательное поле",
      min: v => v.length >= 6 || "Пароль должен содержать > 6 символов",
      password: v => /((?=.*\d)(?=.*[A-Z]).{6,6})/.test(v) || "Невалидный пароль"
    },
    errorMessage: null
  }),
  computed: {
    passwordsMatch() {
      return this.password === this.repeatPassword || "Пароли не совпадают";
    },
    userId() {
      return this.$route.query.userId;
    },
    code() {
      return this.$route.query.code;
    }
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    onConfirm() {
      this.validate();
      if (this.valid) {
        this.$store
          .dispatch(SIGNUP_CONFIRM, {
            password: this.password,
            code: this.code,
            userId: this.userId
          })
          .then(res => this.$router.push("/login"))
          .catch(err => (this.errorMessage = "Ошибка подтверждения"));
      }
    }
  }
};
</script>
