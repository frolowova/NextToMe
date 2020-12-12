<template>
  <div class="confirm-page">
    <span class="auth__descr">Придумайте пароль</span>
    <confirmForm :action="action" class="mb-6" />
  </div>
</template>

<script>
import confirmForm from "@/components/Auth/confirmForm";
import { SIGNUP_CONFIRM } from "@/store/actions/auth";

export default {
  middleware: ['notAuth'],
  components: {
    confirmForm
  },
  data: () => ({
    action: SIGNUP_CONFIRM
  }),
  layout: "auth"
};
</script>

<style scoped>
.confirm-page {
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
