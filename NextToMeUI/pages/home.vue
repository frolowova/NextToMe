<template>
  <div class="home-page">
    <v-tabs grow v-model="toggle_list" @change="onChangeList">
      <v-tab :disabled="avatarLoading">Рядом</v-tab>
      <v-tab :disabled="avatarLoading">ТОП-10</v-tab>
    </v-tabs>
    <div class="home-page__content mx-6 mt-4">
      <v-select
        v-model="select"
        :items="items"
        item-text="abbr"
        item-value="state"
        return-object
        solo
        single-line
        @change="onSortChange"
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
          :showPlace="toggle_list == 1"
          class="mb-4 tag-view"
        />
      </div>
    </div>
  </div>
</template>

<script>
import tagView from "@/components/Tags/TagView.vue";
import skeletonTag from "@/components/Tags/SkeletonTag.vue";
import MessageController from "@/api/MessageController";
import {
  GET_MESSAGES,
  LOAD_AVATARS,
  GET_TOP_MESSAGES,
  CHANGE_LIST_TITLE,
  SET_SORTED_MESSAGES
} from "@/store/actions/messages";

export default {
  middleware: ["auth", "geoAlreadyGranted"],
  components: {
    tagView,
    skeletonTag
  },
  headerData: {
    title: "Главная"
  },
  btnValue: {
    value: "home"
  },
  data: () => ({
    loading: false,
    avatarLoading: false,
    select: {
      state: "Сначала самые просматриваемые",
      abbr: "Самые просматриваемые"
    },
    items: [
      { state: "Сначала самые просматриваемые", abbr: "Самые просматриваемые" },
      { state: "Сначала менее просматриваемые", abbr: "Менее просматриваемые" },
      { state: "Сначала самые обсуждаемые", abbr: "Самые обсуждаемые" },
      { state: "Сначала менее обсуждаемые", abbr: "Менее обсуждаемые" }
    ],
    toggle_list: 0
  }),
  computed: {
    messages() {
      return this.$store.state.messages.messages;
    }
  },
  methods: {
    onSortChange() {
      const messages = [...this.messages];
      switch (this.select.abbr) {
        case this.items[0].abbr:
          messages.sort((a, b) => b.views - a.views);
          break;
        case this.items[1].abbr:
          messages.sort((a, b) => a.views - b.views);
          break;
        case this.items[2].abbr:
          messages.sort((a, b) => b.commentsCount - a.commentsCount);
          break;
        case this.items[3].abbr:
          messages.sort((a, b) => a.commentsCount - b.commentsCount);
          break;
      }
      this.$store.dispatch(SET_SORTED_MESSAGES, messages);
    },
    cardClick(e) {
      const parent = e.target.closest(".tag-view");
      if (parent) {
        this.$router.push(`/tag?id=${parent.id}`);
      }
    },
    onChangeList() {
      this.loading = true;
      this.avatarLoading = true;
      this.$store.dispatch(
        CHANGE_LIST_TITLE,
        this.toggle_list === 0 ? "Рядом" : "Топ"
      );
      this.$store
        .dispatch(this.toggle_list === 0 ? GET_MESSAGES : GET_TOP_MESSAGES)
        .then(result => {
          this.loading = false;
          return this.$store.dispatch(LOAD_AVATARS);
        })
        .then(res => (this.avatarLoading = false))
        .catch(err => {
          this.avatarLoading = false;
          this.loading = false;
        });
    }
  },
  mounted() {
    this.onChangeList();
  }
};
</script>

<style scoped>
.home-page__header {
  width: 100%;
}
</style>
