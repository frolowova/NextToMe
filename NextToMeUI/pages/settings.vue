<template>
    <v-container>
      <div class="d-flex justify-center mb-10">
        <avatar :sizeC=200>
        </avatar>
        <v-btn
          color="primary"
          icon
          v-show="loading"
          @click.prevent="loadImage">
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>

        <v-file-input
          accept="image/x-png,image/jpeg,image/gif"
          ref="fileInput"
          multiple
          v-show="false"
          v-model="selectedImage"
          @change="onChangeFile(), (loading = !loading), (hidden = !hidden)"
        ></v-file-input>

        <v-btn 
          icon 
          color="primary" 
          v-show="hidden"
          @click="updPhoto(), (hidden = !hidden), (loading = !loading)">
          <v-icon>mdi-check</v-icon>
        </v-btn>
      </div>
          

    <h2 class="subtitle-1 primary--text pa-2">Настройки профиля</h2>

    <div class="d-flex align-start pa-2">
      <sub-title-text title="Имя"></sub-title-text>
      <v-text-field
        class="pa-0 ma-0"
        :label="userName"
        v-model="newUserName"
        type="text"
        single-line
        reverse
        loading="0"
        @click="shown = !shown"
      ></v-text-field>
      <v-btn 
        v-if="!shown" 
        icon 
        color="primary" 
        @click="sendUserInfo">
        <v-icon>mdi-check</v-icon>
      </v-btn>
    </div>


    <div class="d-flex align-start justify-space-between pa-2">
      <sub-title-text title="Почта"></sub-title-text>
      <p>{{email}}</p>
    </div>

    <v-divider></v-divider>

    <h2 class="subtitle-1 primary--text pa-2">Общие настройки</h2>

    <div class="d-flex justify-space-between pa-2 pr-0">
      <sub-title-text title="Вкл. push-уведомления"></sub-title-text>
      <v-switch
        class="ma-0 pa-0"
        v-model="btnSwitch"
        color="primary"
        inset
      ></v-switch>
    </div>

    <div class="d-flex justify-space-between pa-2 pr-0">
      <sub-title-text title="Вкл. темную тему"></sub-title-text>
      <v-switch
        class="ma-0 pa-0"
        v-model="darkTheme"
        color="primary"
        inset
        @click="changeTheme"
      ></v-switch>
    </div>
    <div>
      <v-btn class="text-capitalize" text color="primary">
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
import { GET_USER_INFO, SEND_USER_INFO,SEND_USER_PHOTO } from "@/store/actions/userInfo";
import { AUTH_LOGOUT } from "@/store/actions/auth";
import { mapGetters } from "vuex";

export default {

  components: { avatar, SubTitleText },
  headerData: {
    title: "Настройки профиля",
  },
  btnValue: {
    value: "",
  },

  data: () => ({
    title: " ",
    btnSwitch: false,
    newUserName: "",
    shown: true,

    selectedImage: [],
    attachedImage: [],
    hidden: false,
    loading: true,

  }),

  
  methods: {
    sendUserInfo() {
      this.$store.dispatch(SEND_USER_INFO, { userName: this.newUserName })
      .catch((err) => console.log(err));
    },
    updPhoto() {
      this.$store.dispatch(SEND_USER_INFO, { imageBase64: this.src})
      .catch((err) => console.log(err));
      // console.log(this.src) 
    },
    changeTheme() {
      this.$vuetify.theme.dark = !this.$vuetify.theme.dark;
      localStorage.setItem("isDark", this.$vuetify.theme.dark);
    },
    authLogOut() {
      this.$store.dispatch(AUTH_LOGOUT, {})
      .catch(err => console.log(err))
      this.$router.push("/login")
    },

    loadImage() {
      const fileInput = this.$refs.fileInput.$children[0].$el;
      fileInput.click();
    },
    onChangeFile() {
      const selectedImage = this.selectedImage;
      selectedImage.forEach(file => {
        if (file.type !== "image/jpeg") {
          return;
        }
        const reader = new FileReader();
        reader.addEventListener("load", ({ target: { result } }) => {
          this.compressImage(result, file);
        });
        reader.readAsDataURL(file);
      });
    },
    compressImage(base64, file) {
      const canvas = document.createElement("canvas");
      const img = document.createElement("img");

      img.addEventListener("load", () => {
        let width = img.width;
        let height = img.height;
        const MAX_WIDTH = 200;
        const MAX_HEIGHT = 200;

        if (width > height) {
          if (width > MAX_WIDTH) {
            height = Math.round((height *= MAX_WIDTH / width));
            width = MAX_WIDTH;
          }
        } else {
          if (height > MAX_HEIGHT) {
            width = Math.round((width *= MAX_HEIGHT / height));
            height = MAX_HEIGHT;
          }
        }
        canvas.width = width;
        canvas.height = height;

        const ctx = canvas.getContext("2d");
        ctx.drawImage(img, 0, 0, width, height);
        const compressedData = canvas.toDataURL("image/jpeg", 0.8);
        this.attachedImage.push({
          url: compressedData,
          file: file,
        });
      });
      img.addEventListener("error", () => {
        throw new Error("Compressed img error!");
      });
      img.src = base64;
    }
  },
  
   computed: {
     ...mapGetters(['userName','email', 'darkTheme']),

     src() {
      const lastSrc = this.attachedImage.map(img => img.url);
      if(lastSrc.length === 1) {
        return lastSrc[0] ;
      } else if (lastSrc.length>0 && lastSrc.length > 1) {
        return lastSrc[colors.length - 1];
      }
      return "";
    }
   }           
}


</script>
