<template>
  <v-sheet>
    <v-skeleton-loader
      v-bind="attrs"
      type="list-item-avatar, list-item-three-line"
    ></v-skeleton-loader>
  </v-sheet>
</template>

<script>
export default {
  data: () => ({
    attrs: {
      class: "mb-4",
      boilerplate: true,
      elevation: 2
    }
  })
};
</script>
