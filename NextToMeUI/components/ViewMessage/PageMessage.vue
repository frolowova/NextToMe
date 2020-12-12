<template >
  <div v-if="tagInformation" class="cardBackground mb-6">
    <v-tabs grow>
      <v-tab v-if="index === 0 || timeout === true" disabled>Предыдущее</v-tab>
      <v-tab v-if="index !== 0 && timeout === false" @click="toPrevTag"
        >Предыдущее</v-tab
      >
      <!--  -->
      <v-tab v-if="index === lastIndex - 1 || timeout === true" disabled
        >Следующее</v-tab
      >
      <v-tab
        v-if="index !== lastIndex - 1 && timeout === false"
        @click="toNextTag"
        >Следующее</v-tab
      >
    </v-tabs>
    <div class="mx-4 pt-6">
      <header-message
        :username="userInfo.userName"
        :position="tagInformation.distanceToUser"
        :src="userInfo.imageBase64"
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
import { GET_IMAGES, RESET_IMAGES } from "~/store/actions/currentTag";
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
  }),
  methods: {
    timeoutForTab() {
      this.timeout = true;
      setTimeout(() => {
        this.timeout = false;
      }, 2000);
    },
    toPrevTag() {
      const id = this.$store.state.messages.messages[this.index - 1].id;
      this.$router.push({ path: this.$route.path, query: { id } });
       this.$store.dispatch(GET_COMMENTS, this.messageId)
      this.$store.dispatch(RESET_IMAGES);
      this.isMountedOrUpdate(id, this.index - 1);
    },
    toNextTag() {
      const id = this.$store.state.messages.messages[this.index + 1].id;
      this.$router.push({ path: this.$route.path, query: { id } });
      this.$store.dispatch(GET_COMMENTS, this.messageId)
      this.$store.dispatch(RESET_IMAGES);
      this.isMountedOrUpdate(id, this.index + 1);
    },
    isMountedOrUpdate(id = null, index = false) {
      if (index) {
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
    },
  },
  mounted() {
    this.isMountedOrUpdate(this.tagInformation.id);
  },
  destroyed() {
    this.$store.dispatch(RESET_IMAGES);
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