<template>
  <v-btn 
    class="text-capitalize body-1 red--text"
    text
    >
    <v-icon>mdi-pencil-outline</v-icon> {{btnPencilName}}
  </v-btn>
</template>

<script>


export default {
  props: {
      btnPencilName: String,
    },
}

</script>