<template>
  <v-card>
    <v-container>
      <div class="tag d-flex">
        <div class="tag__left">
          <v-avatar size="40px">
            <v-img :src="avatar"></v-img>
          </v-avatar>
        </div>
        <div class="tag__right d-flex flex-column">
          <div class="tag__user mx-2 mb-2">
            <div class="">{{ username }}</div>
            <div class="grey--text">{{ position }}</div>
          </div>
          <div class="tag__content ml-2 mr-4">
            {{ text }}
          </div>
          <v-card-actions class="d-flex justify-space-between mt-2">
            <bomb :time="time" class="flex-grow-1" />
            <comments :views="views" />
            <eye :views="views" />
          </v-card-actions>
        </div>
      </div>
    </v-container>
  </v-card>
</template>

<script>
import bomb from "@/components/ViewMessage/Bomb";
import eye from "@/components/ViewMessage/Eye";
import comments from "@/components/ViewMessage/Comments";

export default {
  components: {
    bomb,
    eye,
    comments,
  },
  props: {
    avatar: String,
    username: String,
    text: String,
    position: String,
    views: String,
    time: String
  }
};
</script>

<style scoped>
.tag__content {
  overflow: hidden;
  display: -webkit-box;
  -webkit-line-clamp: 3;
  -webkit-box-orient: vertical;
}
</style>
