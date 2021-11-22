<template>
  <v-container>
    <div class="d-flex justify-space-between">
      <div class="d-flex align-center">
        <avatar :sizeC="100"></avatar>
        <div>
          <p class="user-name ps-4 text--secondary">{{ userName }}</p>
          <p v-if="countMessages > 0" class="user-name ps-4 text--secondary">
            {{ strCountMessages }}
          </p>
        </div>
      </div>
      <div>
        <v-btn icon @click="settingsGo" class="text--secondary">
          <v-icon>mdi-tune-variant</v-icon>
        </v-btn>
      </div>
    </div>
    <my-tags />
  </v-container>
</template>

<script>
import { mapGetters } from "vuex";
import { GET_USER_INFO } from "@/store/actions/userInfo";
import MyTags from "@/components/Profile/MyTags";
import avatar from "@/components/ProfileSettings/Avatar";

import declOfNum from "@/helpers/declOfNum";

export default {
  components: {
    avatar,
    MyTags,
  },
  headerData: {
    title: "Мой профиль",
  },
  btnValue: {
    value: "profile",
  },

  mounted() {
    this.$store.dispatch(GET_USER_INFO);
  },

  computed: {
    ...mapGetters(["userName", "countMessages"]),
    strCountMessages() {
      return declOfNum(this.countMessages, ["тег", "тега", "тегов"]);
    },
  },

  methods: {
    settingsGo() {
      this.$router.push("/settings");
    },
  },
};
</script>

<style lang="scss" scoped>
.user-name {
  margin-bottom: 0;
}
</style>
