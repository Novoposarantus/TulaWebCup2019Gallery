<template>
  <div id="app">
    <v-app id="inspire">
      <app-header></app-header>
      <v-content>
        <v-container fluid>
          <router-view></router-view>
        </v-container>
      </v-content>
      <app-footer></app-footer>
    </v-app>
  </div>
</template>

<script>
import AppHeader from './components/layout/AppHeader';
import AppFooter from './components/layout/AppFooter';
export default {
  name: 'app',
  components:{
    AppHeader,
    AppFooter
  },
}
</script>
