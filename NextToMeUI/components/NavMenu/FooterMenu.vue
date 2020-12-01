<template>
  <div class="footer-menu">
    <v-bottom-navigation
      color="primary"
      :value="value"
      class="footer-menu_btns"
    >
      <div class="footer-menu_container">
        <v-btn to="/home" nuxt>
          <span>Главная</span>
          <v-icon>mdi-home</v-icon>
        </v-btn>

        <v-btn to="/notifications" nuxt>
          <span>Уведомления</span>
          <v-icon>mdi-bell</v-icon>
        </v-btn>

        <v-btn to="/profile" nuxt>
          <span>Профиль</span>
          <v-icon>mdi-account</v-icon>
        </v-btn>

        <v-btn to="/tag-create" nuxt>
          <span>Тегнуть</span>
          <v-icon>mdi-pencil</v-icon>
        </v-btn>
      </div>
    </v-bottom-navigation>
  </div>
</template>

<script>
export default {
  data: () => ({ value: 0 }),
};
</script>

<style scoped>
.footer-menu {
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
.footer-menu_container {
  max-width: 900px;
  width: 100%;
  display: flex;
  justify-content: space-evenly;
}
</style>