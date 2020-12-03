<template>
  <v-img :src="howRed(timeUpload)" width="19px" height="19px"></v-img>
</template>

<script>
export default {
  props: {
    timeUpload: Date, // При подключении с беком должно быть String
  },
  methods: {
    // const t = Date.parse(endtime) - Date.parse(new Date()),
    //         days = Math.floor( (t/(1000*60*60*24)) ),
    //         seconds = Math.floor( (t/1000) % 60 ),
    howRed(timeOfDeath) {
      const date =
        Date.parse("2021-12-02T22:00:04.243Z") - Date.parse(new Date());
      let leftHours = Math.floor(date / (1000 * 60 * 60));
      console.log(leftHours);
      if (leftHours < 0) {
        return "Error";
      } else if ((leftHours >= 0) & (leftHours < 4)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 4) & (leftHours < 8)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 8) & (leftHours < 12)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 12) & (leftHours < 16)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      } else if ((leftHours >= 16) & (leftHours < 20)) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      } else if (leftHours >= 20) {
        return "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAATCAYAAAByUDbMAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAItSURBVHgBhVOxThtBEN0ZDovKPlERAcflCxxSRaJI8gVJQ5HKrmmcSEntuI0UcZaQ6CJTukr+gC4SojgjpedCkSaFoQVulze7t+Z8OpuR7nZ2d+bNm7e7Sj1haRiG6dbWm3RjIy6vT6LIVGN5GdBFFPWp1ZoS8yk1Gpcyr8b82dxsT3Z2fi4FS7e3uxj6xpih0fot/JHM0yjqyP6LqyuS8T4IEkM0UMsM1U7xXVbWUoD9ssWiKAHTw/J+UAvk9TDG+p6F0jpjotdg/R4L78BoUhQxhBZorjWIHTSbvVyprxX8kbm9Hai1tZC0Fn1iJE4188vdLMt80AwMlDugk2ChZUnJHpFl5/7qBt9AMY9EMiEEoOtyRQt2Ecc9iJxUNoBCwn42lwI42W47y05UjVEaxzGop/DDWQIYGYfi/TL4tWF+XmUlxpTn/bnWnPAOyHoQVgAxtxyJQhzExzpmjM22Z2PTC1DPxHboe5XBsezUgym1OyNjzCOg76/MWCm/Hy8CczGSL3le8AKIqHR7itNdZAHuSmYrOSY2oRC8ysbqWPiTWrD/6+snSO57Fl4eSbLs5AQdUzKeZZ7Xg/1rNhMOgh4CW3PaqGJSLuB0mTbu7mofto3/tr/fxev/Qe74RXsqWlUFjPGxDVyLz+PxcCGY2JeDg9493iSyQn/D5mKIpitaf/p+fFx7+8VWvPP7/Pzs1d7eGI881EKF+ZmWFpn/Yhzy6uqHw6OjM7XEHgAZyP0aY7N5wwAAAABJRU5ErkJggg==";
      }
    },
  },
};
</script>