<template>
  <div class="card-wraper">
    <v-card class="pa-2 my-2 rounded-xl" color="cardBackground">
      <div class="d-flex align-top justify-space-between">
        <div class="d-flex align-center">
          <v-avatar size="40px">
            <v-icon v-if="avatar && !avatarLoading && !avatar.imageBase64"
              >mdi-account-circle</v-icon
            >
            <v-img v-else :src="src"></v-img>
          </v-avatar>
          <div class="mx-2">
            <div v-if="avatar">
              {{ avatar.userName ? avatar.userName : "Пользователь" }}
            </div>
          </div>
        </div>
        <v-btn icon>
          <v-icon>mdi-dots-vertical</v-icon>
        </v-btn>
      </div>
      <v-card-text class="pa-2 text-body-1" v-html="text"></v-card-text>

      <v-container class="mt-0 pt-2" justify="start">
        <span class="text--disabled">{{ this.time }}</span>
      </v-container>
    </v-card>
  </div>
</template>

<script>
import declOfNum from "@/helpers/declOfNum";

export default {
  props: {
    commentData: Object,
    index: Number,
    avatarLoading: Boolean,
  },

  data: () => ({
    lifeTime: "",
  }),

  computed: {
    text() {
      return this.commentData.text.split("\n").join("<br>");
    },
    time() {
      if (this.lifeTime) return this.lifeTime;
      return this.showTime();
    },

    avatar() {
      return this.$store.state.currentTag.commentsAvatars.find(
        (el) => el.userId === this.commentData.from
      );
    },

    src() {
      return this.avatar ? this.avatar.imageBase64 : null;
    },
  },

  mounted() {
    setInterval(() => {
      this.lifeTime = this.showTime();
    }, 60000);
  },

  methods: {
    showTime() {
      let currentDateInMs = new Date().getTime();
      let dateFromServerInMs = new Date(
        this.commentData.createdAt + "Z"
      ).getTime();

      let timeLifeInMin = Math.round(
        (currentDateInMs - dateFromServerInMs) / 60000
      );

      return this.formatTime(timeLifeInMin);
    },

    formatTime(minutes) {
      let inHours = Math.round(minutes / 60);
      let inDays = Math.round(minutes / 1440);
      let timeForm = "";

      if (minutes === 0) {
        timeForm = "только что";
      } else if (minutes < 60) {
        timeForm = declOfNum(minutes, ["минуту", "минуты", "минут"]) + " назад";
      } else if (minutes > 60 && minutes < 1440) {
        timeForm = declOfNum(inHours, ["час", "часа", "часов"]) + " назад";
        return timeForm;
      } else if (minutes > 1440 && minutes < 2880) {
        timeForm = declOfNum(inDays, ["день", "дня", "дней"]) + " назад";
      } else if (minutes > 2880) {
        timeForm = "более двух дней" + " назад";
      }
      return timeForm;
    },
  },
};
</script>

<style lang="scss" scoped>
.card-wraper {
  width: 100%;
}

.text--disabled {
  font-size: 0.9em;
}
</style>
