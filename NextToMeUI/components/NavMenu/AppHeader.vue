<template>
  <div v-if="headerData" class="app-header">
    <v-toolbar
      v-if="!headerData.subtitle_1 & !headerData.subtitle_2"
      class="header"
      height="56px"
      elevation="0"
    >
      <v-container style="max-width: 900px">
        <v-toolbar-title class="header-title">
          <span>{{ headerData.title }}</span>
        </v-toolbar-title>
      </v-container>
    </v-toolbar>
    <v-toolbar v-else class="header" height="112px" elevation="0">
      <v-container style="max-width: 900px">
        <v-toolbar-title class="header-title">
          <span style="padding-bottom: 6px">{{ headerData.title }}</span>
        </v-toolbar-title>
        <div style="padding-top: 20px" class="header-title_subtitle">
          <span>{{ headerData.subtitle_1 }}</span>
          <span>{{ headerData.subtitle_2 }}</span>
        </div>
      </v-container>
    </v-toolbar>
  </div>
</template>

<script>
export default {
  props: {
    headerData: {
      type: Object,
    },
  },
};
</script>
<style scoped>
.header-title_subtitle {
  display: flex;
  justify-content: space-around;
  font-size: 14px;
  line-height: 16px;
  color: white;
}
.header-title {
  color: white;
}
</style>