<template>
  <div class="profile__content" style="margin-top: 20px">
    <div v class="messages" @click="cardClick">
      <h3 v-if="!messages.length" class="text-center">
        К сожалению у Вас нет тэгов
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
import {
  GET_MESSAGES,
  LOAD_AVATARS,
  GET_MY_MESSAGES,
} from "@/store/actions/messages";
import UserController from "~/api/UserController";

export default {
  components: {
    tagView,
  },
  data: () => ({
    tags: [],
    avatarLoading: false,
  }),
  computed: {
    messages() {
      return this.tags;
    },
  },
  methods: {
    getUserInfo() {
      this.$store.dispatch(GET_USER_INFO);
    },
    cardClick(e) {
      const parent = e.target.closest(".tag-view");
      if (parent) {
        this.$router.push(`/tag?id=${parent.id}`);
      }
    },
    ImgView() {
      this.avatarLoading = true;
      this.$store
        .dispatch(GET_MESSAGES)
        .then((result) => {
          return this.$store.dispatch(LOAD_AVATARS);
        })
        .then((res) => (this.avatarLoading = false));
    },
  },
  async mounted() {
    await this.ImgView();
    const response = await UserController.getMyTagsIds();
    await response.data.forEach(async (id) => {
      const tag = await UserController.getMyTag(id);
      this.tags.push(tag);
      this.$store.dispatch(GET_MY_MESSAGES, this.tags);
    });
  },
};
</script>
