<template>
  <v-container>
    <div class="d-flex justify-center mb-10">
      <avatar :sizeC="200" />

      <v-btn
        v-show="loading"
        color="primary"
        icon
        @click.prevent="loadImageClickHandler"
      >
        <v-icon>mdi-pencil-outline</v-icon>
      </v-btn>

      <v-file-input
        accept="image/x-png, image/jpeg"
        ref="fileInput"
        v-show="false"
        v-model="selectedImage"
        @change="onChangeFile(), (loading = !loading), (update = !update)"
      />

      <v-btn
        icon
        color="primary"
        v-show="update"
        @click="updPhoto(), (update = !update), (loading = !loading)"
      >
        <v-icon>mdi-check</v-icon>
      </v-btn>
    </div>

    <h2 class="subtitle-1 primary--text pa-2 body-2">
      Настройки профиля
    </h2>

    <div class="d-flex align-start pa-2">
      <sub-title-text title="Имя" />

      <v-text-field
        class="pa-0 ma-0"
        :label="userName"
        v-model="newUserName"
        type="text"
        single-line
        reverse
        loading="0"
        @click="shown = !shown"
      />

      <v-btn
        v-show="!shown"
        icon
        color="primary"
        @click="sendUserInfo(), (shown = true)"
      >
        <v-icon>mdi-check</v-icon>
      </v-btn>
    </div>

    <div class="d-flex align-start justify-space-between pa-2">
      <sub-title-text title="Почта" />
      <p>{{ email }}</p>
    </div>

    <v-divider />

    <h2 class="subtitle-1 primary--text pa-2 body-2">
      Общие настройки
    </h2>

    <div class="d-flex justify-space-between pa-2 pr-0">
      <sub-title-text title="Вкл. push-уведомления" />
      <v-switch class="ma-0 pa-0" color="primary" inset disabled />
    </div>

    <div class="d-flex justify-space-between pa-2 pr-0">
      <sub-title-text title="Вкл. темную тему" />
      <v-switch class="ma-0 pa-0" v-model="darkTheme" color="primary" inset />
    </div>

    <div>
      <v-btn
        class="text-capitalize body-1"
        text
        color="primary"
        @click="changePass"
      >
        <v-icon>mdi-pencil-outline</v-icon>
        Изменить пароль
      </v-btn>
    </div>
    <div class="d-flex justify-center">
      <v-btn class="text-capitalize body-1" text @click="authLogOut">
        Выйти
      </v-btn>
    </div>
  </v-container>
</template>

<script>
import avatar from "@/components/ProfileSettings/Avatar";
import SubTitleText from "@/components/ProfileSettings/SubTitleText";
import loadAndPreviewImages from "@/mixins/loadAndPreviewImages";
import { GET_USER_INFO, SEND_USER_INFO } from "@/store/actions/userInfo";
import { AUTH_LOGOUT } from "@/store/actions/auth";
import { mapGetters } from "vuex";

export default {
  components: { avatar, SubTitleText },
  mixins: [loadAndPreviewImages],

  headerData: {
    title: "Настройки профиля",
  },
  btnValue: {
    value: "",
  },

  data: () => ({
    loading: true,
    update: false,
    shown: true,
    title: " ",
    newUserName: "",
    selectedImage: [],
    attachedFiles: [],
    isDarkTheme: JSON.parse(localStorage.getItem("isDark") || false),
  }),

  methods: {
    getUserInfo() {
      this.$store.dispatch(GET_USER_INFO);
    },
    sendUserInfo() {
      this.$store
        .dispatch(SEND_USER_INFO, {
          userName: this.newUserName,
        })
        .then((res) => this.$store.dispatch(GET_USER_INFO))
        .catch((err) => console.log(err));
    },
    updPhoto() {
      this.$store
        .dispatch(SEND_USER_INFO, {
          imageBase64: this.src,
        })
        .then((res) => this.$store.dispatch(GET_USER_INFO))
        .catch((err) => console.log(err));
    },

    loadImageClickHandler() {
      const fileInput = this.$refs.fileInput.$children[0].$el;
      fileInput.click();
    },
    onChangeFile() {
      const selectedImage = [this.selectedImage];
      this.attachedFiles = [];
      this.loadAndPreviewImages(selectedImage);
    },

    changePass() {
      this.$router.push("/reset");
    },
    authLogOut() {
      this.$store.dispatch(AUTH_LOGOUT, {}).catch((err) => console.log(err));
      this.$router.push("/login");
    },
  },

  mounted() {
    this.getUserInfo();
  },

  computed: {
    ...mapGetters(["userName", "email"]),
    darkTheme: {
      get() {
        return this.isDarkTheme;
      },
      set(value) {
        this.$vuetify.theme.dark = value;
        localStorage.setItem("isDark", this.$vuetify.theme.dark);
      },
    },
    src() {
      const lastSrc = this.attachedFiles.map((img) => img.url);
      if (lastSrc.length === 1) {
        return lastSrc[0];
      }
      return "";
    },
  },
};
</script>
