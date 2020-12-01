<template>
  <v-container style="padding-left: 0px; padding-right: 0px">
    <v-card class="pa-md-4 mb-4 mx-lg-auto">
      <div class="d-flex align-top justify-space-between" style="padding: 16px">
        <div class="d-flex align-start">
          <v-avatar size="40px" style="margin-right: 12px">
            <img alt="Avatar" src="src" />
          </v-avatar>
          <div class="mx-2">
            <div>Виктор</div>
            <div>оставил(а) комментарий:</div>
            <v-card-text style="padding: 0px"
              >Банальные, но неопровержимые выводы, а также представители
              современных.</v-card-text
            >
            <v-card-actions style="padding: 0px">
              <v-list-item
                class="grow"
                style="padding: 0px; color: color: #FFFFFF; opacity: 0.38;"
              >
                <span>1 минуту назад</span>
              </v-list-item>
            </v-card-actions>
          </div>
        </div>
      </div>
    </v-card>
    <v-card class="pa-md-4 mb-4 mx-lg-auto">
      <div class="d-flex align-top justify-space-between" style="padding: 16px">
        <div class="d-flex align-start">
          <v-avatar size="40px" style="margin-right: 12px">
            <img alt="Avatar" src="src" />
          </v-avatar>
          <div class="mx-2">
            <div>Ольга</div>
            <div>оставил(а) комментарий:</div>
            <v-card-text style="padding: 0px"
              >Современные технологии достигли такого уровня, что высокое
              качество позиционных исследований однозначно
              определяет...</v-card-text
            >
            <v-card-actions style="padding: 0px">
              <v-list-item
                class="grow"
                style="padding: 0px; color: color: #FFFFFF; opacity: 0.38;"
              >
                <span>15 минут назад</span>
              </v-list-item>
            </v-card-actions>
          </div>
        </div>
      </div>
    </v-card>
  </v-container>
</template>

<script>
export default {
  data: () => ({}),
};
</script>