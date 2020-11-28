<template>
  <div class="reset-page">
    <span class="reset__descr">Логин для восстановления</span>
    <resetForm class="mb-6" />
    <linkButton to="/login" text="Назад" class="mb-4" />
  </div>
</template>

<script>
import resetForm from "@/components/Auth/ResetForm";
import linkButton from "@/components/Auth/LinkButton";
export default {
  layout: "auth",
  components: {
    resetForm,
    linkButton
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
  color: #ffffff;
  opacity: 0.8;
}
</style>
