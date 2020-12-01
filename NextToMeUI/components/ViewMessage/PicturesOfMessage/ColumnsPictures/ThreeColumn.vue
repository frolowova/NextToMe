<template>
  <v-list-item class="d-flex flex-wrap px-0">
    <v-img
      class="mx-1 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[0]"
    />
    <v-img
      class="mx-1 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[1]"
    />
    <div
      v-if="!isOpen & (isOpen !== undefined)"
      class="show-all"
      @click="setOpen"
    >
      <div class="show-all__info">
        <p>{{ picNotShown }}</p>
        <p>Показать все</p>
      </div>
      <v-img max-width="100%" max-height="250px" :src="arraySrc[2]" />
    </div>

    <v-img
      v-else
      class="mx-1 my-1"
      max-width="33%"
      max-height="250px"
      :src="arraySrc[2]"
    />
  </v-list-item>
</template>

<script>
export default {
  name: "setOpen",
  props: {
    arraySrc: Array,
    isOpen: Boolean | undefined,
    picNotShown: Number,
  },
  methods: {
    setOpen() {
      this.$emit("setOpen", true);
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
</style>