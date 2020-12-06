<template>
  <div class="d-flex align-top justify-space-between">
    <div class="d-flex align-center">
      <v-avatar size="40px">
        <img alt="Avatar" :src="src" />
      </v-avatar>
      <div class="mx-2">
        <div class="">{{ username }}</div>
        <div class="grey--text">{{ position }} м</div>
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
  },
};
</script>