<template>
  <div>
    <div class="mx-2 mt-4">
      <header-message :username="username" :position="position" :src="src" />
      <text-message :message="message" />
    </div>
    <div>
      <pictures-of-message />
      <statistic-message :time="time" :view="view" />
    </div>
    <div>
      <CommentsList></CommentsList>
    </div>
  </div>
</template>

<script>
import Bomb from "@/components/ViewMessage/Bomb";
import Eye from "@/components/ViewMessage/Eye";
import HeaderMessage from "@/components/ViewMessage/HeaderMessage";
import TextMessage from "@/components/ViewMessage/TextMessage";
import PicturesOfMessage from "@/components/ViewMessage/PicturesOfMessage/PicturesOfMessage";
import StatisticMessage from "@/components/ViewMessage/StatisticMessage";
import CommentsList from "@/components/Comments/CommentsList";
export default {
  components: {
    Bomb,
    Eye,
    HeaderMessage,
    TextMessage,
    PicturesOfMessage,
    StatisticMessage,
    CommentsList
  },
  data: () => ({
    src: "https://avatars0.githubusercontent.com/u/9064066?v=4&s=460",
    username: "Евгений",
    position: "100м",
    message:
      " А также некоторые особенности внутренней политики могут быть рассмотрены исключительно в разрезе маркетинговых и финансовыхпредпосылок. Вот вам яркий пример современ ьных тенденций - пониманиесути ресурсосберегающих технологий обеспечивает актуальность инновационных методов управления процессами. Для современного мирасуществующая теория прекрасно подходит для реализации поэтапного и последовательного развития общества. Но многие известные личности, инициированные исключительно синтетически, ассоциативно.",
    time: "14 минут",
    view: "30"
  })
};
</script> 