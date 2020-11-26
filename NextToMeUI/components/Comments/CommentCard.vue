<template>
  <div>
    <v-card class="pa-md-4 mb-4 mx-lg-auto" color="card">
      <div class="d-flex align-top justify-space-between">
        <div class="d-flex align-center">
          <v-avatar size="40px">
            <img alt="Avatar" :src="src" />
          </v-avatar>
          <div class="mx-2">
            <div>{{ commentData.from }}</div>
          </div>
        </div>
        <v-btn icon>
          <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
      </div>

      <v-card-text>{{commentData.text}}</v-card-text>

      <v-card-actions>
        <v-list-item class="grow">
          <v-list-item-content></v-list-item-content>
          <v-container justify="start">
            <span>{{commentData.createdAt}}</span>
          </v-container>
        </v-list-item>
      </v-card-actions>
    </v-card>
  </div>
</template>

<script>
export default {
  props: {
    commentData: Object,
    index: Number
  }
  //   computed: {
  //     comments() {
  //       return this.$store.state.comments;
  //     }
  //   }
};
</script>

<style lang="scss" scoped>
.mx-auto {
  margin-bottom: 1rem;
  width: 20.5rem;
}
</style>