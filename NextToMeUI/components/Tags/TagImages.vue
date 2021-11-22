<template>
  <div>
    <v-row v-if="isOpen || arrayOfPic.length < 4">
      <v-col
        v-for="(img, i) in arrayOfPic"
        :key="i"
        class="d-flex child-flex images-block"
        cols="4"
        @dblclick="$emit('remove-image', i)"
      >
        <imageView :img="img" />
        <div class="remove-img" @click="$emit('remove-image', i)">
          <img :src="close" />
        </div>
      </v-col>
    </v-row>

    <v-row v-else>
      <v-col
        v-for="(img, i) in arrayOfPic.slice(0, 2)"
        :key="i"
        class="d-flex child-flex images-block"
        cols="4"
        @dblclick="$emit('remove-image', i)"
      >
        <imageView :img="img" />
        <div class="remove-img" @click="$emit('remove-image', i)">
          <img :src="close" />
        </div>
      </v-col>

      <v-col>
        <div class="show-all" @click="setOpen">
          <div class="show-all__info">
            <p>{{ arrayOfPic.length - 3 }}</p>
            <p>Показать все</p>
          </div>
          <imageView :img="arrayOfPic[2]" />
        </div>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import imageView from "./Image.vue";

export default {
  components: {
    imageView,
  },

  data: () => ({
    isOpen: false,
    close: require("@/assets/icon/close.svg"),
  }),

  props: {
    arrayOfPic: {
      type: Array,
      default: [],
    },
  },

  methods: {
    setOpen() {
      this.isOpen = !this.isOpen;
    },
  },
};
</script>

<style scoped lang="scss">
.images-block {
  position: relative;
}

.remove-img {
  position: absolute;
  right: 10px;
  top: 10px;
  cursor: pointer;
  z-index: 1;

  img {
    filter: drop-shadow(0px 0px 1px white);
  }
}

.show-all {
  background: rgba(43, 43, 43, 0.4);
  position: relative;
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
