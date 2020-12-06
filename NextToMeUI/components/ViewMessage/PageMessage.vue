<template>
  <div>
    <div class="mx-2 mt-4">
      <header-message
        :username="tagInformation.from"
        :position="tagInformation.distanceToUser"
        :src="tagInformation.photos[0]"
      />
      <text-message :message="tagInformation.text" />
    </div>
    <div>
      <pictures-of-message />
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
export default {
  components: {
    Bomb,
    Eye,
    HeaderMessage,
    TextMessage,
    PicturesOfMessage,
    StatisticMessage,
  },
  computed: {
    tagInformation() {
      return this.$store.state.messages.messages.find(
        (message) => message.id === this.$route.params.id
      );
    },
  },
};
</script> 