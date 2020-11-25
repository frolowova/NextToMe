<template>
  <v-app dark>
    <v-container >
      <nuxt />
    </v-container>
    <footer-menu />
    
  </v-app>
</template>
<script>
  import FooterMenu from "@/components/NavMenu/FooterMenu";

  export default {
  components: {
    FooterMenu,
    },

    data() {
      return {};
    },
  };
</script>

