<template>
  <v-card class="mx-auto" margin color="card" width="328">
    <v-card-title>
      <v-list-item-avatar color="grey darken-3" large left></v-list-item-avatar>

      <span class="title font-weight-light">{{commentData.from}}</span>
    </v-card-title>

    <v-card-text>{{commentData.text}}</v-card-text>

    <v-card-actions>
      <v-list-item class="grow">
        <v-list-item-content></v-list-item-content>
        <v-row align="center" justify="start">
          <span>{{commentData.createdAt}}</span>
        </v-row>

        <v-row align="center" justify="end">
          <v-icon class="mr-1">mdi-heart</v-icon>
          <span class="subheading mr-2">256</span>
        </v-row>
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
};
</script>

<style lang="scss" scoped>
.mx-auto {
  margin-bottom: 1rem;
}
</style>
