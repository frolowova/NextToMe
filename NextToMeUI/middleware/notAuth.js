export default function ({ redirect }) {
  if (localStorage.getItem('accessToken')) {
    redirect('/home')
  }
}