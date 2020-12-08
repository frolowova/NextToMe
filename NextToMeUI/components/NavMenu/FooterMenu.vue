<template>
  <div class="footer-menu">
    <v-bottom-navigation
      color="primary"
      :v-model="value"
      class="footer-menu_btns"
    >
      <div class="footer-menu_container">
        <v-btn value="home" to="/home" nuxt>
          <span>Главная</span>
          <v-icon>mdi-home</v-icon>
        </v-btn>

        <v-btn value="notifications" to="/notifications" nuxt>
          <span>Уведомления</span>
          <v-icon>mdi-bell</v-icon>
        </v-btn>

        <v-btn value="profile" to="/profile" nuxt>
          <span>Профиль</span>
          <v-icon>mdi-account</v-icon>
        </v-btn>

        <v-btn value="tag-create" to="/tag-create" nuxt>
          <span>Тегнуть</span>
          <v-icon>mdi-pencil</v-icon>
        </v-btn>
      </div>
    </v-bottom-navigation>
  </div>
</template>

<script>
export default {
  props: {
    btnValue: {
      type: Object,
    },
  },
  data: () => ({ value: "" }),
  mounted() {
    this.value = this.btnValue.value;
  },
  watch: {
    value: function (value) {
      console.log(this.value);
      this.value = this.btnValue.value;
    },
  },
};
</script>

<style scoped lang="scss">
.footer-menu {
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.5);
  z-index: 2;
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
@media screen and (max-width: 350px) {
  span {
    display: none;
  }
  .v-item-group.v-bottom-navigation .v-btn {
    min-width: none;
  }

  .v-btn:not(.v-btn--round).v-size--default {
    min-width: 0px;
    padding: 0px 0px;
  }
}
</style>