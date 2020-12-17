<template>
  <div class="d-flex align-top justify-space-between">
    <div class="d-flex align-center">
      <v-avatar size="40px">
        <img v-if="src" alt="Avatar" :src="src" />
        <v-icon v-else>mdi-account-circle</v-icon>
      </v-avatar>
      <div class="mx-2">
        <div class="">{{ username }}</div>
        <div class="grey--text">{{ distnace }}</div>
      </div>
    </div>
    <v-btn icon>
      <v-icon>mdi-dots-vertical</v-icon>
    </v-btn>
  </div>
</template>

<script>
export default {
  props: {
    src: String,
    username: String,
    position: Number,
    place: String,
  },
  methods: {
    declOfNum(n, titles) {
      return (
        n +
        " " +
        titles[
          n % 10 == 1 && n % 100 != 11
            ? 0
            : n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20)
            ? 1
            : 2
        ]
      );
    },
  },
  computed: {
    messages() {
      return this.$store.state.messages.messages;
    },

    distnace() {
      return !this.position
        ? this.place
        : `${this.declOfNum(Math.floor(this.position), [
            "метр",
            "метра",
            "метров",
          ])}`;
    },
  },
};
</script>