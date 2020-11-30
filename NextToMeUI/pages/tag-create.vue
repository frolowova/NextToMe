<template>
  <createTagForm />
</template>

<script>
import createTagForm from "@/components/Tags/CreateTagForm.vue";

export default {
  middleware: ["auth"],
  components: {
    createTagForm
  }
};
</script>
