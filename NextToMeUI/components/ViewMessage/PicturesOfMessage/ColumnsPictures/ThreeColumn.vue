<template>
  <v-list-item class="d-flex flex-wrap justify-space-between px-0">
    <v-img
      class="mx-0 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[0]"
    />
    <v-img
      class="mx-0 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[1]"
    />
    <div v-if="!isOpen" class="show-all" @click="show()">
      <div class="show-all__info">
        <p>{{ picNotShown }}</p>
        <p>Показать все</p>
      </div>
      <v-img max-width="100%" max-height="250px" :src="arraySrc[2]" />
    </div>

    <v-img
      v-else
      class="mx-0 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[2]"
    />
  </v-list-item>
</template>

<script>
import { BTN_SHOW_PICTURE } from "~/store/actions/currentTag";
export default {
  props: {
    arraySrc: Array,
    picNotShown: Number,
  },
  computed: {
    isOpen() {
      return this.$store.state.currentTag.isOpen;
    },
  },
  methods: {
    show() {
      this.$store.dispatch(BTN_SHOW_PICTURE, true);
    },
  },
};
</script>


<style scoped lang="scss">
.show-all {
  background: rgba(43, 43, 43, 0.4);
  position: relative;
  width: 33%;
  cursor: pointer;

  &__info {
    z-index: 1;
    position: absolute;
    text-align: center;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);

    & p {
      font-family: Roboto;
      font-style: normal;
      font-weight: normal;
      font-size: 14px;
      line-height: 20px;
      letter-spacing: 0.16px;
      font-feature-settings: "pnum" on, "lnum" on;
      color: #ffffff;
    }

    & p + p {
      margin-top: 20px;
    }
  }
}

@media screen and (max-width: 480px) {
  .show-all {
    &__info {
      & p {
        font-size: 8px;
        line-height: 10px;
        margin-bottom: 0px;
      }
    }
  }
}
</style>