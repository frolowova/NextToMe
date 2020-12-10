export default function ({ redirect, store }) {
  if (localStorage.getItem('firstSeenSlider') == store.state.auth.username) {
    redirect('/permissions')
  }
}