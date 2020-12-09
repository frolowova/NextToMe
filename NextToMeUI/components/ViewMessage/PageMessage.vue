<template >
  <div v-if="tagInformation" class="cardBackground mb-6">
    <div class="mx-4 pt-6">
      <header-message
        :username="userInfo.userName"
        :position="tagInformation.distanceToUser"
        :src="userInfo.imageBase64"
      />
      <text-message :message="tagInformation.text" />
    </div>
    <div>
      <pictures-of-message  :images="images"/>
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
import { GET_IMAGES } from "~/store/actions/currentTag";
import MessageController from "@/api/MessageController";
export default {
  headerData: {
    title: "",
  },
  btnValue: {
    value: "",
  },
  components: {
    Bomb,
    Eye,
    HeaderMessage,
    TextMessage,
    PicturesOfMessage,
    StatisticMessage,
  },
  mounted() {
    const photos = this.tagInformation.photos[0];
    if (photos !== undefined){
      this.$store.dispatch(GET_IMAGES, photos);
    }
    if (!this.$route.query.id) {
      this.$router.push("/home");
    } else {
      MessageController.updateViews(this.$route.query.id);
    } 
    
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
    images() {
      return this.$store.state.currentTag.images;
    },
  },
};
</script> 