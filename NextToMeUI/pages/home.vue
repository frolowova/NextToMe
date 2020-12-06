<template>
  <div class="home-page mx-6 mt-4">
    <v-select
      v-model="select"
      :items="items"
      item-text="abbr"
      item-value="state"
      return-object
      solo
      single-line
    ></v-select>
    <div v-if="loading">
      <skeletonTag v-for="i in 10" :key="i" />
    </div>
    <div v-else class="messages" @click="cardClick">
      <h3 v-if="!messages.length" class="text-center">
        К сожалению сейчас нет новых тэгов
      </h3>
      <tagView
        v-for="message in messages"
        :message="message"
        :key="message.id"
        :id="message.id"
        :avatarLoading="avatarLoading"
        class="mb-4 tag-view"
      />
    </div>
  </div>
</template>

<script>
import tagView from "@/components/Tags/TagView.vue";
import skeletonTag from "@/components/Tags/SkeletonTag.vue";
import { GET_MESSAGES, LOAD_AVATARS } from "@/store/actions/messages";
export default {
  components: {
    tagView,
    skeletonTag,
  },
  headerData: {
    title: "Главная",
  },
  btnValue: {
    value: "home",
  },
  data: () => ({
    loading: false,
    avatarLoading: false,
    select: {
      state: "Сначала самые просматриваемые",
      abbr: "Самые просматриваемые",
    },
    items: [
      { state: "Сначала самые просматриваемые", abbr: "Самые просматриваемые" },
      { state: "Сначала менее просматриваемые", abbr: "Менее просматриваемые" },
      { state: "Сначала самые обсуждаемые", abbr: "Самые обсуждаемые" },
      { state: "Сначала менее обсуждаемые", abbr: "Менее обсуждаемые" },
    ],
  }),
  computed: {
    messages() {
      const messages = [...this.$store.state.messages.messages];
      switch (this.select.abbr) {
        case this.items[0].abbr:
          return messages.sort((a, b) => b.views - a.views);
        case this.items[1].abbr:
          return messages.sort((a, b) => a.views - b.views);
        case this.items[2].abbr:
          return messages.sort((a, b) => b.commentsCount - a.commentsCount);
        case this.items[3].abbr:
          return messages.sort((a, b) => a.commentsCount - b.commentsCount);
      }
    },
  },
  methods: {
    cardClick(e) {
      const parent = e.target.closest(".tag-view");
      if (parent) {
        this.$router.push(`/tag?id=${parent.id}`);
      }
    },
  },
  mounted() {
    this.loading = true;
    this.avatarLoading = true;
    this.$store
      .dispatch(GET_MESSAGES)
      .then((result) => {
        this.loading = false;
        return this.$store.dispatch(LOAD_AVATARS);
      })
      .then((res) => (this.avatarLoading = false))
      .catch((err) => {
        this.loading = false;
        this.avatarLoading = false;
      });
  },
};
</script>
