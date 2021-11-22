<template>
  <v-btn
    block
    elevation="2"
    x-large
    :color="color"
    :outlined="outlined"
    :disabled="disabled"
    :loading="loading"
    class="rounded-lg"
    v-on="$listeners"
  >
    {{ title }}
  </v-btn>
</template>

<script>
export default {
  props: {
    title: String,
    color: {
      type: String,
      default: "primary",
    },
    outlined: {
      type: Boolean,
      default: false,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    loading: {
      type: Boolean,
      default: false,
    },
  },
};
</script>
