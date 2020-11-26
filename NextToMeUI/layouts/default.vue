<template class="template">
  <v-app>
    <v-container> </v-container>
    <footer-menu></footer-menu>
  </v-app>
</template>
<script>
import FooterMenu from "@/components/FooterMenu";

export default {
  components: {
    FooterMenu,
  },

  data() {
    return {};
  },
};
</script>
<style scoped>
.container {
  margin: 0 auto;
  padding: 16px;
}
</style>
