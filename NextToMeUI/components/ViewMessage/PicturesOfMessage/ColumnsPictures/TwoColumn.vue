<template>
  <v-list-item class="d-flex flex-wrap justify-space-between px-0">
    <v-img
      class="my-1 mx-0"
      max-width="49.5%"
      max-height="250px"
      :src="arraySrc[0]"
    />
    <v-img
      class="my-1 mx-0"
      max-width="50.1%"
      max-height="250px"
      :src="arraySrc[1]"
    />
  </v-list-item>
</template>

<script>
export default {
  props: {
    arraySrc: Array,
  },
};
</script>