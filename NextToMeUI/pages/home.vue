<template>
  <div class="home-page">
    <tagView
      :avatar="src"
      :username="username"
      :position="position"
      :text="message"
      :views="view"
      :time="time"
    />
  </div>
</template>

<script>
import tagView from "@/components/Tags/TagView.vue";
import { GET_MESSAGES } from "@/store/actions/messages";
export default {
  components: {
    tagView
  },
  data: () => ({
    src: "https://avatars0.githubusercontent.com/u/9064066?v=4&s=460",
    username: "Евгений",
    position: "100м",
    message:
      " А также некоторые особенности внутренней политики могут быть рассмотрены исключительно в разрезе маркетинговых и финансовыхпредпосылок. Вот вам яркий пример современ ьных тенденций - пониманиесути ресурсосберегающих технологий обеспечивает актуальность инновационных методов управления процессами. Для современного мирасуществующая теория прекрасно подходит для реализации поэтапного и последовательного развития общества. Но многие известные личности, инициированные исключительно синтетически, ассоциативно.",
    time: "14 минут",
    view: "30"
  }),
  mounted() {
    this.$store.dispatch(GET_MESSAGES).then(res => console.log(res));
  }
};
</script>
