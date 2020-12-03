<template>
<v-container>
  <div class="d-flex justify-space-between">
    <div class="d-flex align-center">
      <avatar :sizeC=100></avatar>
      <div>
        <p class="ps-4 text--secondary" >userName</p>
        <p class="ps-4 body-2 text--secondary">Tags N</p>
      </div>
    </div>
    <div>
      <v-btn
        icon 
        @click="settingsGo"
        color="secondary">
        <v-icon>mdi-tune-variant</v-icon>
      </v-btn>
    </div>
  </div>
    
  <v-btn @click.prevent="getUserInfo"> Получить имя</v-btn>
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