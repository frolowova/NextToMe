export default function({ redirect }) {
  if (!localStorage.getItem('accessToken')) {
    redirect('/login')
  } else {
    redirect('/home')
  }
}
