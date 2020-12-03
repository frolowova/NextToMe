<template>
  <v-card>
    <v-container>
      <div class="tag d-flex">
        <div class="tag__left">
          <v-avatar size="40px">
            <v-icon v-if="!avatarLoading && !avatar.imageBase64" dark
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
            <div class="grey--text">{{ message.distanceToUser }} м</div>
          </div>
          <div class="tag__content ml-2 mr-4">
            {{ message.text }}
          </div>
          <v-card-actions class="d-flex justify-space-between mt-2">
            <bomb :time="time" class="flex-grow-1" />
            <nuxt-link class="tag-link" :to="`/tag?id=${message.id}`">
              <comments :amount="message.commentsCount" />
            </nuxt-link>
            <eye :views="message.views" />
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
    time() {
      const time = Date.now() - Date.parse(this.message.createdAt)
      const seconds = Math.floor(time / 1000);
      if (seconds < 60) {
        return `${seconds} секунд`;
      }
      const minutes = Math.floor(seconds / 60);
      console.log(minutes)
      if (minutes < 60) {
        return `${minutes} минут`;
      }
      const hours = Math.floor(minutes / 60);
      if (hours < 24) {
        return `${hours} часа`;
      }
      const days = Math.floor(hours / 24);
      return `${days} день`;
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
