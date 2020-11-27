<template>
  <div class="reset-confirm-page">
    <span class="reset__descr">Придумайте пароль</span>
    <confirmForm :action="action" class="mb-6" />
  </div>
</template>

<script>
import confirmForm from "@/components/confirmForm";
import { RESET_PASSWORD_CONFIRM } from "@/store/actions/auth";

export default {
  components: {
    confirmForm
  },
  data: () => ({
    action: RESET_PASSWORD_CONFIRM
  }),
  layout: "auth"
};
</script>

<style scoped>
.reset-confirm-page {
  max-width: 640px;
  width: 100%;
}

.reset__descr {
  display: block;
  font-size: 1.25rem;
  text-align: center;
  font-family: Roboto;
  letter-spacing: 0.5px;
  color: #ffffff;
  opacity: 0.8;
}
</style>
