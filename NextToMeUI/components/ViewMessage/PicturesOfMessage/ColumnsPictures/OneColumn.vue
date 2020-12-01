<template>
  <v-img class="my-1" max-height="250px" :src="arraySrc[0]" />
</template>

<script>
export default {
  props: {
    arraySrc: Array,
  },
};
</script>