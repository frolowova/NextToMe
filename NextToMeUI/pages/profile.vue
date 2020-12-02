<template>
<v-container>
  <v-row
   justify="space-around">
    <v-card
      width="600"
      elevation="0">
      <v-row class="d-flex justify-space-between pa-2 pr-0"> 
        <v-col class="d-flex align-center">
          <avatar :sizeC=100></avatar>
          <v-col>
            <p class="ps-4"> Name</p>
            <p class="ps-4 body-2">Tags N</p>
          </v-col>
        </v-col>
        <v-btn
        text
        icon 
        @click="settingsGo">
        <v-icon>mdi-tune-variant</v-icon> </v-btn>
      </v-row>
    </v-card>
  </v-row>
</v-container>
  
</template>


<script>
import avatar from '@/components/ProfileSettings/Avatar';

export default {
  components: {avatar},
  methods:{
    settingsGo() {
      this.$router.push("/settings")
    },
  }
}
</script>