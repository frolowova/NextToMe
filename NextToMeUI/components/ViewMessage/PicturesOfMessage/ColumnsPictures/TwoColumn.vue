<template>
  <v-list-item class="d-flex flex-wrap px-0">
    <v-img
      class="mx-1 my-1"
      max-width="50%"
      max-height="250px"
      :src="arraySrc[0]"
    />
    <v-img
      class="mx-1 my-1"
      max-width="50%"
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