<template >
  <div v-if="tagInformation" class="cardBackground mb-6">
    <v-toolbar color="header" elevation="0">
      <v-row align="center" justify="space-between">
        <v-btn
          text
          class="white--text font-weight-light"
          @click="toPrevTag"
          :disabled="index === 0 || timeout === true"
        >
          <v-icon>mdi-chevron-left</v-icon>
          Предыдущий
        </v-btn>
        <v-btn
          text
          class="white--text font-weight-light"
          @click="toNextTag"
          :disabled="index === lastIndex - 1 || timeout === true"
        >
          Следующий
          <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
      </v-row>
    </v-toolbar>

    <div class="mx-4 pt-6">
      <header-message
        :username="userInfo.userName"
        :position="tagInformation.distanceToUser"
        :src="userInfo.imageBase64"
        :place="tagInformation.place"
      />
      <text-message :message="tagInformation.text" />
    </div>
    <div>
      <pictures-of-message :images="images" />
      <statistic-message
        :time="tagInformation.deleteAt"
        :view="tagInformation.views"
      />
    </div>
  </div>
</template>

<script>
import Bomb from "@/components/Bomb/Bomb";
import Eye from "@/components/ViewMessage/Eye";
import HeaderMessage from "@/components/ViewMessage/HeaderMessage";
import TextMessage from "@/components/ViewMessage/TextMessage";
import PicturesOfMessage from "@/components/ViewMessage/PicturesOfMessage/PicturesOfMessage";
import StatisticMessage from "@/components/ViewMessage/StatisticMessage";
import {
  GET_IMAGES,
  RESET_IMAGES,
  GET_COMMENTS,
  LOAD_COMMENT_AVATARS,
  RESET_COMMENTS,
  BTN_SHOW_PICTURE,
} from "~/store/actions/currentTag";
import MessageController from "@/api/MessageController";

export default {
  components: {
    Bomb,
    Eye,
    HeaderMessage,
    TextMessage,
    PicturesOfMessage,
    StatisticMessage,
  },
  data: () => ({
    timeout: false,
    avatarLoading: false,
    skeletonLoad: false,
  }),
  methods: {
    timeoutForTab() {
      this.timeout = true;
      setTimeout(() => {
        this.timeout = false;
      }, 1500);
    },
    toPrevTag() {
      const id = this.$store.state.messages.messages[this.index - 1].id;
      this.skeletonLoad = true;
      this.avatarLoading = true;
      this.$router.push({ path: this.$route.path, query: { id } });
      this.$store
        .dispatch(GET_COMMENTS, id)
        .then((result) => {
          this.skeletonLoad = false;
          return this.$store.dispatch(LOAD_COMMENT_AVATARS);
        })
        .then((res) => (this.avatarLoading = false));
      this.$store.dispatch(RESET_IMAGES);
      this.$store.dispatch(RESET_COMMENTS);
      const indexLoading = this.index - 1;
      this.isMountedOrUpdate(id, indexLoading);
    },
    toNextTag() {
      const id = this.$store.state.messages.messages[this.index + 1].id;
      this.skeletonLoad = true;
      this.avatarLoading = true;
      this.$router.push({ path: this.$route.path, query: { id } });
      this.$store
        .dispatch(GET_COMMENTS, id)
        .then((result) => {
          this.skeletonLoad = false;
          return this.$store.dispatch(LOAD_COMMENT_AVATARS);
        })
        .then((res) => (this.avatarLoading = false));
      this.$store.dispatch(RESET_IMAGES);
      this.$store.dispatch(RESET_COMMENTS);
      const indexLoading = this.index + 1;
      this.isMountedOrUpdate(id, indexLoading);
    },
    isMountedOrUpdate(id = null, index = false) {
      if (index !== false) {
        this.$store.state.messages.messages[index].photos.forEach((item) => {
          this.$store.dispatch(GET_IMAGES, item);
        });
      } else {
        this.tagInformation.photos.forEach((item) => {
          this.$store.dispatch(GET_IMAGES, item);
        });
      }
      if (!id) {
        this.$router.push("/home");
      } else {
        MessageController.updateViews(id);
      }
      this.timeoutForTab();
      this.$store.dispatch(BTN_SHOW_PICTURE, false);
    },
  },
  mounted() {
    this.isMountedOrUpdate(this.tagInformation.id);
  },
  destroyed() {
    this.$store.dispatch(RESET_IMAGES);
    this.$store.dispatch(RESET_COMMENTS);
  },

  computed: {
    tagInformation() {
      return this.$store.state.messages.messages.find(
        (message) => message.id === this.$route.query.id
      );
    },
    userInfo() {
      return this.$store.state.messages.avatars.find(
        (user) => user.userId === this.tagInformation.from
      );
    },
    index() {
      return this.$store.state.messages.messages.findIndex(
        (message) => message.id === this.$route.query.id
      );
    },
    lastIndex() {
      return this.$store.state.messages.messages.length;
    },
    images() {
      return this.$store.state.currentTag.images;
    },
  },
};
</script> 

<style lang="stylus" scoped lang="scss">
.button-nuv {
  display: flex;
  justify-content: space-between;
  width: 100%;
}
</style>
