<template>
  <createTagForm />
</template>

<script>
import createTagForm from "@/components/Tags/CreateTagForm.vue";

export default {
  middleware: ["auth", "geoAlreadyGranted"],
  components: {
    createTagForm,
  },
  headerData: {
    title: "Тегнуть",
  },
  btnValue: {
    value: "tag-create",
  },
};
</script>
