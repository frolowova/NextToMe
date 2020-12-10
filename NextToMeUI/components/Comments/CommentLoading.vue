<template>
  <v-sheet>
    <v-skeleton-loader v-bind="attrs" type="list-item-avatar, list-item-two-line"></v-skeleton-loader>
  </v-sheet>
</template>

<script>
export default {
  data: () => ({
    attrs: {
      boilerplate: true,
      elevation: 2
    }
  })
};
</script>


