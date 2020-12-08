<template>
  <div class="my-3" v-if="images">
    <!-- Пытался сделать через v-for, но все попытки заканчивались провалом, ибо свои классы мне писать нельзя (media query). К сожалению, поэтому пришлось использовать такой подход. -->
    <!-- Я буду выдавать определённый блок по количеству фотографий в массиве, который пришёл с сервера для данного сообщения -->

    <div v-if="isOpen">
      <!-- Для восьми картинок -->
      <div v-if="getLength(images) === 8">
        <one-column :arraySrc="[images[0]]" />
        <three-column :arraySrc="[images[1], images[2], images[3]]" />
        <three-column :arraySrc="[images[4], images[5], images[6]]" />
        <one-column :arraySrc="[images[7]]" />
      </div>

      <!-- Для семи картинок -->
      <div v-if="getLength(images) === 7">
        <one-column :arraySrc="[images[0]]" />
        <three-column :arraySrc="[images[1], images[2], images[3]]" />
        <three-column :arraySrc="[images[4], images[5], images[6]]" />
      </div>

      <!-- Для шести картинок -->
      <div v-if="getLength(images) === 6">
        <one-column :arraySrc="[images[0]]" />
        <three-column :arraySrc="[images[1], images[2], images[3]]" />
        <two-column :arraySrc="[images[4], images[5]]" />
      </div>

      <!-- Для пяти картинок -->
      <div v-if="getLength(images) === 5">
        <one-column :arraySrc="[images[0]]" />
        <three-column :arraySrc="[images[1], images[2], images[3]]" />
        <one-column :arraySrc="[images[4]]" />
      </div>
    </div>

    <div v-else>
      <!-- Компонент кнопки показа остальных картинок -->
      <div v-if="getLength(images) > 4">
        <one-column :arraySrc="[images[0]]" />
        <three-column
          :arraySrc="[images[1], images[2], images[3]]"
          :isOpen="isOpen"
          @setOpen="setOpen"
          :picNotShown="getLength(images) - 4"
        />
      </div>

      <!-- Для четырёх картинок -->
      <div v-if="getLength(images) === 4">
        <one-column :arraySrc="[images[0]]" />
        <three-column :arraySrc="[images[1], images[2], images[3]]" />
      </div>

      <!-- Для трёъ картинок -->
      <div v-if="getLength(images) === 3">
        <one-column :arraySrc="[images[0]]" />
        <two-column :arraySrc="[images[1], images[2]]" />
      </div>

      <!-- Для двух картинок -->
      <div v-if="getLength(images) === 2">
        <two-column :arraySrc="[images[0], images[1]]" />
      </div>

      <!-- Для одной картинки -->
      <div v-if="getLength(images) === 1">
        <one-column :arraySrc="[images[0]]" />
      </div>
    </div>
  </div>
</template>

<script>
import TwoColumn from "./ColumnsPictures/TwoColumn.vue";
import ThreeColumn from "./ColumnsPictures/ThreeColumn.vue";
import OneColumn from "./ColumnsPictures/OneColumn.vue";
import { GET_IMAGES } from "~/store/actions/currentTag";
export default {
  components: {
    TwoColumn,
    ThreeColumn,
    OneColumn,
  },
  data: () => ({
    isOpen: false,
  }),
  // props: {
  //   images: Array,
  // },
  computed: {
    images() {
      return this.$store.state.currentTag.images;
    },
  },
  methods: {
    getLength(array) {
      return array.length;
    },
    setOpen(isOpen) {
      this.isOpen = isOpen;
    },
  },
  mounted() {
    this.$store.dispatch(GET_IMAGES, this.$route.params.id);
  },
};
</script>
