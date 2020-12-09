<template>
  <div>
    <v-avatar
      color="grey lighten-2"
      :size = sizeC>
      <v-img 
        :src=imageBase64 alt="ava"></v-img>
    </v-avatar>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  props: {
    sizeC: Number,
  },
  computed: mapGetters(['imageBase64']),
}
</script>