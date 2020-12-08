<template>
  <v-app dark>
    <app-header :header-data="headerData" />
    <v-container
      style="margin-bottom: 56px; padding: 0; max-width: 900px"
    >
      <nuxt />
    </v-container>
    <footer-menu :btnValue="btnValue" />
  </v-app>
</template>
<script>
import FooterMenu from "@/components/NavMenu/FooterMenu";
import AppHeader from "@/components/NavMenu/AppHeader";

export default {
  components: {
    FooterMenu,
    AppHeader,
  },
  mounted() {
    setTimeout(() => {
      this.$vuetify.theme.dark = this.$store.getters.darkTheme;
    }, 0);
  },
  computed: {
    headerData() {
      return this.$route.matched.map((r) => {
        return r.components.default.options.headerData;
      })[0];
    },
    btnValue() {
      return this.$route.matched.map((r) => {
        return r.components.default.options.btnValue;
      })[0];
    },
  },
};
</script>


<style scoped>
.header {
  position: relative;
  z-index: 100;
  display: flex;
  overflow: hidden;
  position: fixed;
  top: 0;
  width: 100%;
}

.theme--dark.v-application {
  background-color: var(--v-background-base, #292929) !important;
}
.theme--light.v-application {
  background-color: var(--v-background-base, rgb(249, 249, 249)) !important;
}
</style>


