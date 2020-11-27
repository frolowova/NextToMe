<template>
  <div class="my-3">
    <v-img
      class="mx-1"
      lazy-src="https://picsum.photos/id/11/10/6"
      max-height="200px"
      src="data:image/png;base64, iVBORw0KGgoAAAANSUhEUgAAAAUA
    AAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO
        9TXL0Y4OHwAAAABJRU5ErkJggg=="
    ></v-img>

    <v-row class="mx-0">
      <v-col v-for="n in 3" :key="n" class="d-flex child-flex px-1" cols="4">
        <v-img
          :src="`https://picsum.photos/500/300?image=${n * 5 + 1}`"
          aspect-ratio="0"
          class="grey lighten-1"
        >
          <template v-slot:placeholder>
            <v-row class="fill-heigh ma-0" align="center" justify="center">
              <v-progress-circular color="grey lighten-5"></v-progress-circular>
            </v-row>
          </template>
        </v-img>
      </v-col>
    </v-row>
  </div>
</template>
