export default function({ redirect }) {
  return navigator.permissions
    .query({ name: "geolocation" })
    .then(({ state }) => {
      if (state != "granted") {
        redirect("/permissions");
      }
    });
}