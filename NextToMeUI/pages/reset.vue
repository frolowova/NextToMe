<template>
  <div class="reset-page">
    <span class="reset__descr">Логин для восстановления</span>
    <resetForm class="mb-6" />
    <v-btn nuxt to="/login" block elevation="2" x-large outlined>Назад</v-btn>
  </div>
</template>

<script>
import resetForm from "@/components/Auth/ResetForm";
export default {
  layout: "auth",
  components: {
    resetForm
  }
};
</script>

<style scoped>
.reset-page {
  max-width: 640px;
  width: 100%;
}

.reset__descr {
  display: block;
  font-size: 1.25rem;
  text-align: center;
  font-family: Roboto;
  letter-spacing: 0.5px;
  opacity: 0.8;
}
</style>
