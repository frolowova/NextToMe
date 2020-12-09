<template>
  <div class="my-2">
    <p v-html="text(message)" />
  </div>
</template>

<script>
export default {
  props: {
    message: String,
  },
  methods: {
    text(message) {
      return message.split("\n").join("<br>");
    },
  },
};
</script>