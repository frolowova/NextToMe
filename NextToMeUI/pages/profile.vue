<template>
    <v-container>
      <v-row 
        justify="space-around"> 
          <v-card
            width="600"
            elevation="0">

            <v-row
              justify="space-around mb-10">
              <v-avatar
                color="grey lighten-2"
                size="200">
              </v-avatar>
            </v-row>
            
            <!-- <v-card-title>Настройки профиля</v-card-title>
            <v-row >
              <v-col>
                <p>Имя</p>
                <p>Почта</p>
              </v-col>
              <v-col>
                <p>Настя</p>
                <p>Nastya@gmail.com</p>
              </v-col>
            </v-row>

            <v-divider></v-divider>

            <v-card-title>Общие настройки</v-card-title> -->
          

          
            <h3>Настройки профиля</h3>

            <div class="d-flex justify-space-between py-4">
              <div>Имя</div>
              <div>Настя</div>
            </div>

            <div class="d-flex justify-space-between py-4">
              <div>Почта</div>
              <div>Nastya@gmail.com</div>
            </div>

            <v-divider></v-divider>

            <h3>Общие настройки</h3>

            <div class="d-flex justify-space-between align-center">
              <div>Вкл. push-уведомления</div>
              <div>
                <v-switch
                  v-model="switch1"
                  color="red"
                  inset
                ></v-switch>
              </div>
            </div>

            <div class="d-flex justify-space-between align-center">
              <div>Вкл. темную тему</div>
              <div>
                <v-switch
                  v-model="switch2"
                  color="red"
                  inset
                ></v-switch>
                </div>
            </div>

            <!-- <v-btn text>Изменить пароль</v-btn> -->

            <v-row
              align="center"
              justify="space-around"
            ><v-btn text>Выйти</v-btn>
            </v-row>

          </v-card>
      </v-row>
    </v-container>
</template>

<script>
export default {
  data: () => ({
    switch1: false,
    switch2: false,
  }),
}

</script>

<style>

</style>