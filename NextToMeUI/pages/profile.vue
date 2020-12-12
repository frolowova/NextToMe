<template>
  <v-container>
    <div class="d-flex justify-space-between">
      <div class="d-flex align-center">
        <avatar :sizeC="100"></avatar>
        <div>
          <p class="ps-4 text--secondary">{{ userName }}</p>
        </div>
      </div>
      <div>
        <v-btn icon @click="settingsGo" color="secondary">
          <v-icon>mdi-tune-variant</v-icon>
        </v-btn>
      </div>
    </div>
    <my-tags />
  </v-container>
</template>

<script>
import avatar from "@/components/ProfileSettings/Avatar";
import { GET_USER_INFO, SEND_USER_INFO } from "@/store/actions/userInfo";
import { mapGetters } from "vuex";
import MyTags from "@/components/Profile/MyTags";
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

  methods: {
    settingsGo() {
      this.$router.push("/settings");
    },
  },
  mounted() {
    this.$store.dispatch(GET_USER_INFO);
  },
  computed: mapGetters(["userName"]),
};
</script>
