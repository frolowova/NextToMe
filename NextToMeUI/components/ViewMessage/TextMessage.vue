<template>
  <div class="my-2">
    <p class="tag-message" v-html="text(message)" />
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

<style scoped>
.tag-message {
  margin-right: 1rem;
  overflow-x: auto;
}
::-webkit-scrollbar {
  width: 5px;
  height: 5px;
  margin-left: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: transparent;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #1e1f2044;
  border-radius: 5px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  opacity: 0.5;
}
</style>