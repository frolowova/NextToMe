<template>
  <div class="home-page">
    <v-select
      v-model="select"
      :items="items"
      item-text="abbr"
      item-value="state"
      return-object
      solo
      single-line
    ></v-select>
    <div v-if="!loading" class="messages">
      <tagView
        v-for="message in messages"
        :message="message"
        :key="message.id"
        :avatarLoading="avatarLoading"
        class="mb-4"
      />
    </div>
  </div>
</template>

<script>
import tagView from "@/components/Tags/TagView.vue";
import { GET_MESSAGES, LOAD_AVATARS } from "@/store/actions/messages";
export default {
  components: {
    tagView
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
    ]
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
    }
  },
  mounted() {
    console.log(localStorage.getItem("nextId"));
    this.loading = true;
    this.avatarLoading = true;
    this.$store
      .dispatch(GET_MESSAGES)
      .then(result => {
        this.loading = false;
        return this.$store.dispatch(LOAD_AVATARS);
      })
      .then(res => (this.avatarLoading = false))
      .catch(err => {
        this.loading = false;
        this.avatarLoading = false;
      });
  }
};
</script>
