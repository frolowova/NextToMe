<template>
  <div class="footer-menu">
    <v-bottom-navigation
      :value="value"
      color="primary"
      class="footer-menu_btns"
    >
      <v-btn>
        <span class="text-caption">Главная</span>

        <v-icon>mdi-home</v-icon>
      </v-btn>

      <v-btn>
        <span class="text-caption">Тегнуть</span>

        <v-icon>mdi-pencil</v-icon>
      </v-btn>

      <v-btn>
        <span class="text-caption">Мои Теги</span>

        <v-icon>mdi-email</v-icon>
      </v-btn>

      <v-btn>
        <span class="text-caption">Профиль</span>

        <v-icon>mdi-account</v-icon>
      </v-btn>
    </v-bottom-navigation>
  </div>
</template>
<script>
export default {
  data: () => ({ value: 1 }),
};
</script>

<style scoped>
.footer-menu {
  box-shadow: 8px 0 10px rgba(0, 0, 0, 0.5);
  position: relative;
  display: flex;
  overflow: hidden;
  position: fixed;
  bottom: 0;
  width: 100%;
}
.footer-menu_btns {
  display: flex;
  justify-content: space-around;
}
</style>