<template>
  <v-card class="cardBackground">
    <v-container>
      <div class="tag d-flex">
        <div class="tag__left">
          <v-avatar size="40px">
            <v-icon v-if="!avatarLoading && !avatar.imageBase64"
              >mdi-account-circle</v-icon
            >
            <v-img v-else :src="src">
              <template v-slot:placeholder>
                <v-row class="fill-height ma-0" align="center" justify="center">
                  <v-progress-circular
                    indeterminate
                    color="grey lighten-5"
                  ></v-progress-circular>
                </v-row>
              </template>
            </v-img>
          </v-avatar>
        </div>
        <div class="tag__right d-flex flex-column flex-grow-1">
          <div class="tag__user mx-2 mb-2">
            <div class="">
              {{
                `${
                  avatar && avatar.userName ? avatar.userName : "Пользователь"
                }`
              }}
            </div>
            <div class="grey--text">
              {{ Math.floor(message.distanceToUser) }} м
            </div>
          </div>
          <div class="tag__content ml-2 mr-4" v-html="text"></div>
          <v-card-actions class="d-flex justify-space-between mt-2">
            <bomb :deleteTime="this.message.deleteAt" class="flex-grow-1" />
            <!-- <nuxt-link class="tag-link" :to="`/tag?id=${message.id}`"> -->
            <nuxt-link class="tag-link" :to="`/tag?id=${this.message.id}`">
              <comments :amount="message.commentsCount" />
            </nuxt-link>
            <!-- </nuxt-link> -->
            <eye :views="message.views" />
          </v-card-actions>
        </div>
      </div>
    </v-container>
  </v-card>
</template>

<script>
import bomb from "@/components/Bomb/Bomb";
import eye from "@/components/ViewMessage/Eye";
import comments from "@/components/ViewMessage/Comments";
import imageView from "./Image.vue";

export default {
  components: {
    bomb,
    eye,
    comments
  },
  props: {
    message: Object,
    avatarLoading: Boolean
  },
  computed: {
    text() {
      return this.message.text.split("\n").join("<br>");
    },
    avatar() {
      return this.$store.state.messages.avatars.find(
        el => el.userId === this.message.from
      );
    },
    src() {
      return this.avatar ? this.avatar.imageBase64 : null;
    }
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

.tag-link {
  text-decoration: none;
}
</style>
