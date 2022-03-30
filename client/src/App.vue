<script >
import AuthPage from './components/AuthPage.vue'
import AdminPage from './components/AdminPage.vue'

import MainPage from './components/MainPage.vue'
const routes = {
  '/admin': AuthPage,
  '/admin/edit': AdminPage,
  '/': MainPage,
}
export default {
  data() {
    return {
      currentPath: window.location.hash
    }
  },
  computed: {
    currentView() {
      return routes[this.currentPath.slice(1) || '/'] || NotFound
    }
  },
  created() {
    localStorage.removeItem('isAuth');
    window.addEventListener('hashchange', () => {
		  this.currentPath = window.location.hash
		})
  }
}
</script>

<template>
  <component :is="currentView" />
  <div v-if="false">
    <AdminPage/>
    <AuthPage/> 
  </div>
</template>

<style>
@import './assets/base.css';

#app {
  margin-left: auto;
  margin-right: auto;

  font-weight: normal;
}
body{
  background-image: url("https://wallpapercave.com/wp/A2u0Ixo.jpg");
  background-color: #cccccc;
  background-size: 1300px;
}

</style>
