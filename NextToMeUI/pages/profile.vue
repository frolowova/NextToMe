<template>
<v-container >
  <v-row
   justify="space-around">
   
      <v-row class="d-flex justify-space-between pa-2 pr-0"> 
        <v-col class="d-flex align-center">
          <avatar :sizeC=100></avatar>
          <v-col>
            <p class="ps-4" >userName</p>
            <p class="ps-4 body-2">Tags N</p>

            <v-btn @click.prevent="getUserInfo"> Получить имя</v-btn>
          </v-col>
        </v-col>
        <v-btn
        text
        icon 
        @click="settingsGo">
        <v-icon>mdi-tune-variant</v-icon> </v-btn>
      </v-row>
  </v-row>
</v-container> 
  
</template>


<script>
import avatar from '@/components/ProfileSettings/Avatar';
import { GET_USER_INFO} from "@/store/actions/userInfo";
import {mapGetters} from 'vuex';
export default {
  components: {avatar},
  data: () => ({
  
  }),
  methods:{
    settingsGo() {
      this.$router.push("/settings")
    },
    getUserInfo(){
      this.$store.dispatch(GET_USER_INFO, {}).then(res => {
        console.log(`userInfo ${res}`)
      }).catch(err => console.log(err))
    },
    
  },
  // computed: mapGetters(['userName']),
  
}
</script>