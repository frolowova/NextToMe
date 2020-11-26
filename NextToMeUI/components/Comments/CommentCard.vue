<template>
  <v-card class="mx-auto" margin color="card">
    <v-card-title>
      <v-list-item-avatar color="grey darken-3" large left></v-list-item-avatar>

      <span class="title font-weight-light">{{commentData.from}}</span>
       <v-btn icon>
      <v-icon>mdi-dots-vertical</v-icon>
    </v-btn>
    </v-card-title>

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