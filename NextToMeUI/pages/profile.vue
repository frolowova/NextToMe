<template>
<v-container>
  <v-row
   justify="space-around">
    <v-card
      width="600"
      elevation="0">
      <v-row class="d-flex justify-space-between pa-2 pr-0"> 
        <v-col class="d-flex align-center">
          <avatar ></avatar>
          <v-col>
            <p class="ps-4">Name</p>
            <p class="ps-4 body-2">Tags N</p>
          </v-col>
        </v-col>
        <v-btn text> <v-icon>{{ icons.mdiTuneVariant }}</v-icon> </v-btn>
      </v-row>
    </v-card>
  </v-row>
</v-container>
  
</template>



<script>
import avatar from '@/components/ProfileSettings/Avatar';
import { mdiAccount, mdiTuneVariant  } from '@mdi/js';
export default {
  components: {avatar},
  data: () => ({
    icons: {
        mdiAccount,
        mdiTuneVariant,
      },
  }),
}
</script>