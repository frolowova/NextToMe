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
    <v-btn
      :loading="loading"
      :disabled="loading || !valid"
      elevation="0"
      type="submit"
      color="primary"
      x-large
      block
    >
      Подтвердить
    </v-btn>
  </v-form>
</template>

<script>
export default {
  layout: "auth",
  data: () => ({
    valid: true,
    password: "",
    repeatPassword: "",
    rules: {
      required: v => !!v || "Обязательное поле",
      min: v => v.length >= 6 || "Пароль должен содержать > 6 символов",
      password: v =>
        /((?=.*\d)(?=.*[A-Z]).{6,6})/.test(v) || "Невалидный пароль"
    },
    errorMessage: null,
    loading: false
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
  props: {
    action: String
  },
  methods: {
    validate() {
      this.$refs.form.validate();
    },
    onConfirm() {
      this.validate();
      if (this.valid) {
        this.loading = true;
        this.$store
          .dispatch(this.action, {
            password: this.password,
            code: this.code,
            userId: this.userId
          })
          .then(res => {
            this.loading = false;
            this.$router.push("/login");
          })
          .catch(err => {
            this.loading = false;
            this.errorMessage = "Ошибка подтверждения";
          });
      }
    }
  }
};
</script>
