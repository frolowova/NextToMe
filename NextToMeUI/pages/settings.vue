<template>
    <v-container>
      <div class="d-flex justify-center mb-10">
        <avatar :sizeC=200></avatar>
        <v-btn
          color="primary"
          icon>
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>
      </div>
            
      <h2 class="subtitle-1 primary--text pa-2">Настройки профиля</h2>

      <div class="d-flex align-start pa-2"> 
        <sub-title-text title = "Имя"></sub-title-text>
        <v-text-field 
          class="pa-0 ma-0"
          label = "Введите новое имя"
          type="text"
          single-line
          reverse
          loading="0"
        ></v-text-field>
        <v-btn 
          icon
          color="primary"
          @click="sendUserInfo">
          <v-icon>mdi-check</v-icon>
        </v-btn>
      </div >

      <div class="d-flex align-start justify-space-between pa-2"> 
        <sub-title-text title = "Почта"></sub-title-text>
        <p> E-mail</p>
      </div>

      <v-divider></v-divider>

      <h2 class="subtitle-1 primary--text pa-2">Общие настройки</h2>

      <div class="d-flex justify-space-between  pa-2 pr-0"> 
        <sub-title-text title = "Вкл. push-уведомления"></sub-title-text>
        <v-switch
          class="ma-0 pa-0"
          v-model="btnSwitch1"
          color="primary"
          inset
        ></v-switch>
      </div>

      <div class="d-flex justify-space-between pa-2 pr-0"> 
        <sub-title-text title = "Вкл. темную тему"></sub-title-text>
        <v-switch
          class="ma-0 pa-0"
          v-model="btnSwitch2"
          color="primary"
          inset
          @click="changeTheme"
        ></v-switch>
      </div>
      <div>
        <v-btn
          class="text-capitalize"
          text
          color="primary">
          <v-icon>mdi-pencil-outline</v-icon>
          Изменить пароль
        </v-btn>
      </div>
      <div class="d-flex justify-center">
        <v-btn 
          class="text-capitalize body-1"
          text @click="authLogOut">
          Выйти
        </v-btn>
      </div>  
    </v-container>
</template>

<script>
import avatar from '@/components/ProfileSettings/Avatar';
import SubTitleText from '@/components/ProfileSettings/SubTitleText';
import {SEND_USER_INFO} from "@/store/actions/userInfo";
import {AUTH_LOGOUT} from "@/store/actions/auth";


export default {
  components: {avatar,SubTitleText},
  data: () => ({
    title: " ",
    btnSwitch1: false,
    btnSwitch2: false,

  }),
  methods: {
    authLogOut() {
      this.$store.dispatch(AUTH_LOGOUT, {})
      .then(res => {
        console.log(res)
      }).catch(err => console.log(err))
      
      },
    sendUserInfo() {
      this.$store.dispatch(SEND_USER_INFO, {userName: this.userName})
      .then(res => {
        console.log(res)
      }).catch(err => console.log(err))
    },
    changeTheme() {
      this.$vuetify.theme.dark=!this.$vuetify.theme.dark
      console.log( this.$vuetify.theme.dark);


    }

  }
}

</script>

<style>

</style>