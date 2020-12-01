<template>
  <v-app dark>
    <v-toolbar class="header" height="56px">
      <v-container style="max-width: 900px">
        <v-toolbar-title v-if="$route.name === 'home'">
          {{ page[0] }}
        </v-toolbar-title>
        <v-toolbar-title v-if="$route.name === 'notifications'">
          {{ page[1] }}
        </v-toolbar-title>
        <v-toolbar-title v-if="$route.name === 'profile'">
          {{ page[2] }}</v-toolbar-title
        >
        <v-toolbar-title v-if="$route.name === 'tag-create'">
          {{ page[3] }}</v-toolbar-title
        >
      </v-container>
    </v-toolbar>
    <v-container
      style="margin-bottom: 56px; margin-top: 56px; max-width: 900px"
    >
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

  data: () => ({
    page: ["Главная", "Уведомления", "Профиль", "Тегнуть"],
  }),
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
</style>


