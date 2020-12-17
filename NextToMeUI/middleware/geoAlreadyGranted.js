export default function ({ redirect }) {
  // Обнаружение Safari
  let isSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);

  if (!isSafari) return navigator.permissions
    .query({ name: "geolocation" })
    .then(({ state }) => {
      if (state != "granted") {
        redirect("/permissions");
      }
    });
}