<template>
  <div class="image-view">
    <v-img :src="img" aspect-ratio="1" class="grey lighten-2">
      <template v-slot:placeholder>
        <v-row class="fill-height ma-0" align="center" justify="center">
          <v-progress-circular
            indeterminate
            color="grey lighten-5"
          ></v-progress-circular>
        </v-row>
      </template>
    </v-img>
  </div>
</template>

<script>
export default {
  props: {
    img: String
  }
};
</script>
