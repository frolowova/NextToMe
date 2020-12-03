<template>
    <v-container>

      <div class=" d-flex justify-center mb-10">
        <avatar :sizeC=200></avatar>
        <v-btn
          color="primary"
          text
          icon>
          <v-icon>mdi-pencil-outline</v-icon>
        </v-btn>
      </div>
            
          <h2 class="subtitle-1 primary--text pa-2">Настройки профиля</h2>

          <div class="d-flex align-start pa-2"> 
            <sub-title-input title = "Имя"></sub-title-input>
            <settings-input labelC = "Введите новое имя"></settings-input>
          </div >

          <div class="d-flex align-start pa-2"> 
            <sub-title-input title = "Почта"></sub-title-input>
            <settings-input labelC = "Введите новый e-mail"></settings-input>
          </div>

          <v-divider></v-divider>

          <h2 class="subtitle-1 primary--text pa-2">Общие настройки</h2>

          <div class="d-flex justify-space-between  pa-2 pr-0"> 
            <sub-title-input title = "Вкл. push-уведомления"></sub-title-input>
            <v-switch
              class="ma-0 pa-0"
              v-model="btnSwitch1"
              color="primary"
              inset
            ></v-switch>
          </div>

          <div class="d-flex justify-space-between pa-2 pr-0"> 
            <sub-title-input title = "Вкл. темную тему"></sub-title-input>
            <v-switch
              class="ma-0 pa-0"
              v-model="btnSwitch2"
              color="primary"
              inset
              @click="changeTheme"
            ></v-switch>

          </div>
          <v-btn
            class="text-capitalize"
            text
            color="primary">
              <v-icon>mdi-pencil-outline</v-icon>
              Изменить пароль
            </v-btn>

          <v-row
            align="center"
            justify="space-around"
            >
            <v-btn class="text-capitalize body-1" text @click="settingsOut">Выйти</v-btn>
          </v-row>
      </v-row>
    </v-container>
</template>

<script>
import avatar from '@/components/ProfileSettings/Avatar';
import SettingsInput from '@/components/ProfileSettings/SettingsInput';
import SubTitleInput from '@/components/ProfileSettings/SubTitleInput';


export default {
  components: {avatar, SettingsInput,SubTitleInput},
  data: () => ({
    title: " ",
    btnPencilName: " ",
    btnSwitch1: false,
    btnSwitch2: false,

  }),
  methods: {
    settingsOut() {
      this.$router.push({path:"/profile"})
    },
    changeTheme() {
      this.$vuetify.theme.dark=!this.$vuetify.theme.dark
      console.log( this.$vuetify.theme.dark)

    }

  }
}

</script>

<style>

</style>