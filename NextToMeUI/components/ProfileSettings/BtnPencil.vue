<template>
  <v-btn 
    class="text-capitalize body-1 red--text"
    text>
    <v-icon>{{ icons.mdiPencilOutline }}</v-icon> {{btnPencilName}}
  </v-btn>
</template>

<script>
import { mdiAccount,mdiPencilOutline, } from '@mdi/js';

export default {
  props: {
      btnPencilName: String,
    },
  data: () => ({
    icons: {
        mdiAccount,
        mdiPencilOutline,
      },
    
  }),
}

</script>