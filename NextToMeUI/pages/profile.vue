<template>
  <v-container>
    <div class="d-flex justify-space-between">
      <div class="d-flex align-center">
        <avatar :sizeC="100"></avatar>
        <div>
          <p class="ps-4 text--secondary">{{ userName }}</p>
          <p class="ps-4 body-2 text--secondary">Tags N</p>
        </div>
      </div>
      <div>
        <v-btn icon @click="settingsGo" color="secondary">
          <v-icon>mdi-tune-variant</v-icon>
        </v-btn>
      </div>
    </div>
    <!--  -->

    <div class="profile__content mx-6 mt-4">
      <div v-if="loading">
        <skeletonTag v-for="i in 10" :key="i" />
      </div>
      <div v-else class="messages" @click="cardClick">
        <h3 v-if="!messages.length" class="text-center">
          К сожалению у Вас нет тэгов
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
    <!--  -->
  </v-container>
</template>


<script>
import avatar from "@/components/ProfileSettings/Avatar";
import { GET_USER_INFO, SEND_USER_INFO } from "@/store/actions/userInfo";
import { mapGetters } from "vuex";
//

import tagView from "@/components/Tags/TagView.vue";
import skeletonTag from "@/components/Tags/SkeletonTag.vue";
import {
  GET_MESSAGES,
  LOAD_AVATARS,
  GET_TOP_MESSAGES,
  CHANGE_LIST_TITLE,
  GET_MY_MESSAGES,
} from "@/store/actions/messages";
import UserController from "~/api/UserController";

//
export default {
  components: {
    avatar,
    tagView,
    skeletonTag,
  },
  headerData: {
    title: "Мой профиль",
  },
  btnValue: {
    value: "profile",
  },
  //
  data: () => ({
    tags: [],
    loading: false,
    avatarLoading: false,
    select: {
      state: "Сначала самые просматриваемые",
      abbr: "Самые просматриваемые",
    },
    items: [
      { state: "Сначала самые просматриваемые", abbr: "Самые просматриваемые" },
    ],
    toggle_list: 0,
  }),
  computed: {
    messages() {
      return this.tags;
    },
  },
  methods: {
    settingsGo() {
      this.$router.push("/settings");
    },
    getUserInfo() {
      this.$store.dispatch(GET_USER_INFO);
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
  },
  async mounted() {
    await this.onChangeList();
    const response = await UserController.getMyTagsIds();
    await response.data.forEach(async (id) => {
      const tag = await UserController.getMyTag(id);
      this.tags.push(tag);
      this.$store.dispatch(GET_MY_MESSAGES, this.tags);
    });
  },
};
//
</script>

<style scoped>
.profile__content {
  width: 100%;
}
</style>