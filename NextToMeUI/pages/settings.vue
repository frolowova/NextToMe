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
          :label = userName
          v-model="newUserName"
          type="text"
          single-line
          reverse
          loading="0"
          @click ="shown = !shown"
        ></v-text-field>
        <v-btn v-if ="!shown"
          icon
          color="primary"
          @click="sendUserInfo">
          <v-icon>mdi-check</v-icon>
        </v-btn>
      </div >

      <div class="d-flex align-start justify-space-between pa-2"> 
        <sub-title-text title = "Почта"></sub-title-text>
        <p>email</p>
      </div>

      <v-divider></v-divider>

      <h2 class="subtitle-1 primary--text pa-2">Общие настройки</h2>

      <div class="d-flex justify-space-between  pa-2 pr-0"> 
        <sub-title-text title = "Вкл. push-уведомления"></sub-title-text>
        <v-switch
          class="ma-0 pa-0"
          v-model="btnSwitch"
          color="primary"
          inset
        ></v-switch>
      </div>

      <div class="d-flex justify-space-between pa-2 pr-0"> 
        <sub-title-text title = "Вкл. темную тему"></sub-title-text>
        <v-switch
          class="ma-0 pa-0"
          v-model="darkTheme"
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
import {CHANGE_THEME} from "@/store/actions/userInfo";
import {mapGetters} from 'vuex';

export default {
  components: {avatar,SubTitleText},
  data: () => ({
    title: " ",
    btnSwitch: false,
    newUserName: "",
    shown: true

  }),
  methods: {
    sendUserInfo() {
      this.$store.dispatch(SEND_USER_INFO, {userName: this.newUserName})
     .catch(err => console.log(err)) 

    },
    changeTheme() {
      this.$vuetify.theme.dark=!this.$vuetify.theme.dark
      localStorage.setItem('isDark', this.$vuetify.theme.dark);
    },
 
    authLogOut() {
      this.$store.dispatch(AUTH_LOGOUT, {})
      .catch(err => console.log(err))
      this.$router.push("/login")
      },
  },
  
   computed: mapGetters(['userName', 'darkTheme']),
            
}

</script>

<style>

</style>