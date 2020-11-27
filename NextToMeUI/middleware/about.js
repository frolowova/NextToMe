export default function ({ redirect, store }) {
  console.log(store.state.auth.username, localStorage.getItem('firstSeenSlider'))
  if (localStorage.getItem('firstSeenSlider') == store.state.auth.username) {
    redirect('/home')
  }
}