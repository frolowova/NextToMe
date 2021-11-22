<template>
  <div class="bomb-icon mr-4 d-flex">
    <v-img :src="src" width="18px" height="20px" class="flex-grow-0"></v-img>
    <p class="ml-2">{{ localTime }}</p>
  </div>
</template>

<script>
import declOfNum from "@/helpers/declOfNum";

export default {
  props: {
    deleteTime: String,
  },

  computed: {
    src() {
      const date = Date.parse(this.deleteTime + "Z") - Date.parse(new Date());
      let leftHours = Math.floor(date / (1000 * 60 * 60));
      if (leftHours < 0) {
        return "";
      } else if ((leftHours >= 0) & (leftHours < 1)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAGeSURBVHgBfVTBdcIwDJU4cUw3oBM0TABsADc4USYoGwAb0AlKbzwupBOUTlBGgAlKT8Al6ldsg+0k1XuJZUv6+pLiMP0jMhqNSWQOtYV1T8wDXq8PsV+jFmA4bGFZ4pkgkAHyDv2zyrcWBNJFYAaAnW54s1lifwC7jpeoWwsC4xzLWMuwuk3Z+LDlqc+sSKQJSgD9fkLN5k90/Ki9sLZv7Fd4vgqQPBeOgl/QvCmyJWV6sqTrdQGfBPoUfpkzsaWm3d/CkOoWOizss5TCFz0BUJuz7GTLITBcuJ684UlvwAZAPBAFEJy3wGTrgm1JxEB8tiBkHbmk+2yM9NzUVJTJ+Ja1DCBWJwqHMPP0AuTJluCRZ0PflWZ0IrtgTWOQh8DBB7r3he2ZsyUhCPPpVoLfTN2bKVEpicgpBNGLdc/MARNTituLZ9uHIEQLqhORcMx3kBXFBoxZb2cHQWGDw7E62WG8vZgJ0eUywPtoAaqyi1fGJAI1IPoZ40tsw+k1CnTBv3jP6XzuVf2UyrdY71Gep7j2qd5QrEcwzYpENfIHpSPBXoDfLjAAAAAASUVORK5CYII=";
      } else if ((leftHours >= 1) & (leftHours < 3)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAHHSURBVHgBfZM9VsJAEMdnKHiU0coynsBYWIulnXZ26gHA3AC5gT4o6NSOzlhYCyeQI8AJjA0feZDxv8ku7CbEfQ9mNjPzm5mdXaZ/VrvdvoV4FBGfmSdJklwPBoNp0a9WBQjD0EfwU5qm971ej6G/1ev1r32+lZDNZtOEiPr9/kjtAXoCaNpqtc6tRM1KCNp4hFCt+Fo360O1p3066/U6g3ARIFdXXu/o6Mf+1orjYx4Op8jsHSbJ91mSvH42GuPL+byJKoTtYGo0Hog5JBGvVB7Oh1arLnw86CH8ImPKIHJz48PwDkOgttBhYbtKyXxxJgCdchTFiOlkgOGwa87kBb9gC84BYkEUQPDdRyXvJhhinOkg3mkIaUcu6XY1+boAZGQMqpLbbdYyQLRO5A6hY+kZ5ES3YBXPefmmtVwn0gIyKEIOHAcbtDsX1t+MzXMhzPG2Bfsw1T6fEpWSiMQuRGRiZWankrwVsxfLNnEhRF2qWiLumHeQVyoaMGb1Os8R5B6wO1azRhjvRbESouXyGv8zDdiXXaw27gvQHKKuMW7iKZyeC4Em+JfU610s1CWbFiHlV6zeUZoGVKsFkAI5Q6VRlqhi/QHKstQAtW7NQQAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 3) & (leftHours < 5)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAHOSURBVHgBfVNLUgJBDE2mLIqdeAM8gXgCYOPenTt1Dzg3UG6AwIKdumPneALwBHIEOIAlFgsoPhNfmmnoaRhTxSTdSV6SF5rpH2k0GrdQTyJSZObRcrm87vV6Yz8uyAIIw7CI5FYcx/edTodhv+VyucGx2EyQzWZTgYq63e5QzwBqAWhcr9fLTqFKJgjGeILSUYqJbeVDx0tiHtfrtQFhOhyjgC5+wAGhsrlbrVbnyoX6MN4Xrl6hP4MgqEALu8m4eFATyQUXxLQcBC3cNWFqXIhzhE72nCiJcAyQpJs4xZUogALpT8+amHQxbbfbIQDKACorZycacTWbvXwzl5IE+9E+bKfqkRirzhO9t4iqSG7WarWKccrNzR30iwm15X17K+JwWOV+f7gbNdmCJPV8AElsfwmPjm1ALkyAW3TPKpPLsCVapOSDnKUCXCDboe3CFmIupEGYp7sR9knbs6GW6aCIyDQNIjJyKnOqk+0o9iyOb5QGIWpSlkhqRrfAK/kOrFlfZxlJaYLTa7UyxHqrfidEi8U1vpME4Fh1cca490C3IBxFU8rnLxH07CXa5F/S1zuf659s7IMcvGKMVqQ4LuHFlaAFeoJOI1MoQ/4AC0z2YZA66GwAAAAASUVORK5CYII=";
      } else if ((leftHours >= 5) & (leftHours < 15)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAHcSURBVHgBfVNLUsJAEO1OWehOvEE8gbi3CjiCG8udegAwN1BugMCCnbhjZzwBeAI5ApxArKIKik/a15OJmQTiVCU9093vdb+eGqZ/VrPZvIN5FhGfmSfr9fq63+9P83leEUEQBD7A7SiKHrrdLmP/ViqVRodyC0l2u10NJuz1emM9g6gNommj0ag6hWqFJJDxDKNSfLtP1ofKszlP2+3WkDDtyyiji2/MgFDZ+DabzbnOQmOQ9wXXAPbT87warLALhuNRtwCXXRLTsue14Wthq3kBziE6SWeiQ0RgBJDexClcogRKpJ+eFWi7mHc6nQAEVRBVdWaeHeIrQBVblO1n2rDdsCX2kfuuDoBbKskEMe172FdLoAi2Uty9ielB9/jqya3pOrpaLu+2Gks70J85s5VFMZitNDoWeeoRpSQ3i8WFwbGFJpKSoTgdOtQV50LNYM/iNKHMignEmVNMENtyloR5TukEU6ZYfAp0i4jMsyQiE6cyZzqJp5qcxYlNsiRELSpaktHoFhhQPiC3t/o6qwDxX/uWhvafxpiHw3q+E6LV6hr/mSU4VF0cGQ850piEw3BOJyeXSHrJARPwD+nrXS7r6GKaJ9l7xZDmUxRV8OIqsAI7Q6ehKVSwfgFb8RNZWDgEXAAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 15) & (leftHours < 20)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAHcSURBVHgBfVRLbsIwELWjKuqu9AbpCZruKyUcgU3FjnIAaG4A3IASFuygO3ZNTwA9QTkCnKCphBTEJ+mbxKaDIbVkz3g882beOI4U/4x2u92A6GZZ5kgpF9vttjYajZamn1UGEASBg+B+mqbNMAwl9DfbtmeXfEtBDoeDDxENh8M57QHUB9Cy1Wp5LJFfCgIaXQii4ihdjw+ip3w6+/0+B5HinEYFVXyjBwKZc9tut7ujXtAZ6H3BNIH8tCzLh8wkD4bhhVQEVzhIXrJl9WHrQSW/APsIlfz1hJqIgxmC6CZuYMoIgIBo0p4CVRXxYDAIAOAByKOeWaqJYwS5KqlUMy9DVSMVsAPfdzIguEeU8kN0+xlyrAAoQioqXM/PaEM6ZlXfmqbT0Fl1ozWAUjNFqVgKih1+GVeY9zqjHhRnZM5BWaNdDkKV3LLg44ECOKlQV0I3ZILEjMIxTiu8Qtaf+ITO03q9sIXweTZBjkUE99V9EhshFiEHeUwS+oB8cWnoj8WgBNuEuxW3Ua/T6/QENVTKi9nZmMvptMoNxQPcbGpYVwogM5McbfinYG0aoAWIjKJYXF8/wOnVCNTBP4Jeb5JUUcXSBDl7xaDmiDR18eJcyAxyhUqjPFHJ+AUhNBoIL35xMQAAAABJRU5ErkJggg==";
      } else if (leftHours >= 20) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABEAAAATCAYAAAB2pebxAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAHFSURBVHgBfVTbVQIxEE2Ch1+1g7UC1woWSqACpABwOwA6WIEP/sAKxArACtwSsALXX17xTsgsQ2DNOSGzycydeycZtPpn9Hq9NpaBtTbSWuebzaY1nU7XoZ+pAkjTNEJwdjgcOuPxWMN+q9fry2u+lSD7/b6BZTGZTFb0DaAMQOtut5uIRI1KEMgYYCEpkbd5fJA879Pf7XYORKtLGXdg8YMaKGR2e9vt9oFqQWeQ94WtOdZPY0wDq9UyGBsvZCL4ToI4ysZk2BvCJL8U3wswOdWEioiDJYLoJm6xZQmAgGjSNwV6FsVoNEoBkAAooZoZX8QZgmKfVPvpaHg22gNH8H2nDQQPSZI7RLWfsc48AEVoL0Xa7ow+yMZs8q2xnDZn5UIzgDetl3T8OUrsy8u4wXzkjDwoLsjsQEWhYwlCTO5FcHngAc4YMhO6oRCkEBLKODYkQ1GfIgTJvU4eJZNAgiCm8jMQHAylDMHELeKtaLYx5xLEneKaqTsTddkGZXHFWKEZm6EcVavVWvD95tcp6sAAXJ8cfdQJEh1Bsiwr8ISfYL4KNlad3s8vAAbwaV77U7rWxRH6IkZA7DoUDMF0QYlUxfgDR5MjeT6/0lYAAAAASUVORK5CYII=";
      }
    },
    localTime() {
      const time = new Date(this.deleteTime + "Z") - Date.now();
      const seconds = Math.floor(time / 1000);
      if (seconds < 60) {
        return declOfNum(seconds, ["секунду", "секунды", "секунд"]);
      }
      const minutes = Math.floor(seconds / 60);
      if (minutes < 60) {
        return declOfNum(minutes, ["минуту", "минуты", "минут"]);
      }
      const hours = Math.floor(minutes / 60);
      if (hours < 24) {
        return declOfNum(hours, ["час", "часа", "часов"]);
      }
      const days = Math.floor(hours / 24);
      return declOfNum(days, ["день", "дня", "дней"]);
    },
  },
};
</script>
