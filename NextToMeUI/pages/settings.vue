<template>
    <v-container >
      <v-row 
        justify="space-around"> 
        <v-card
          width="600"
          elevation="0">

          <v-row
            class=" mb-10"
            justify="center">
            <avatar></avatar>
            <btn-pencil></btn-pencil>
          </v-row>
            
          <h2 class="subtitle-1 red--text pa-2">Настройки профиля</h2>

          <v-col class="d-flex align-start pa-2"> 
            <sub-title-input title = "Имя"></sub-title-input>
            <settings-input></settings-input>
          </v-col >

          <v-col class="d-flex align-start pa-2"> 
            <sub-title-input title = "Почта"></sub-title-input>
            <settings-input></settings-input>
          </v-col>

          <v-divider></v-divider>

          <h2 class="subtitle-1 red--text pa-2">Общие настройки</h2>

          <v-col class="d-flex justify-space-between  pa-2 pr-0"> 
            <sub-title-input title = "Вкл. push-уведомления"></sub-title-input>
            <btn-switch></btn-switch>
          </v-col>

          <v-col class="d-flex justify-space-between pa-2 pr-0"> 
            <sub-title-input title = "Вкл. темную тему"></sub-title-input>
            <btn-switch></btn-switch>
          </v-col>
          <btn-pencil btnPencilName="Изменить пароль"></btn-pencil>
          

          <v-row
            align="center"
            justify="space-around"
            >
            <v-btn class="text-capitalize body-1" text>Выйти</v-btn>
          </v-row>
          

        </v-card>
      </v-row>
    </v-container>
</template>

<script>
import avatar from '@/components/ProfileSettings/Avatar';
import BtnSwitch from '@/components/ProfileSettings/BtnSwitch';
import SettingsInput from '@/components/ProfileSettings/SettingsInput';
import SubTitleInput from '@/components/ProfileSettings/SubTitleInput';
import BtnPencil from '@/components/ProfileSettings/BtnPencil';


export default {
  components: {avatar, BtnSwitch, SettingsInput,SubTitleInput,BtnPencil},
  data: () => ({
    title: " ",
    btnPencilName: " ",

  }),
}

</script>

<style>

</style>