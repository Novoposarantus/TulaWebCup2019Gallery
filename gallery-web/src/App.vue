<template>
  <div id="app">
    <v-app id="inspire">
      <v-toolbar app>
        <v-toolbar-title>Галерея</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-btn 
              flat
              :to="{ name : routeNames.Login }" 
              v-if="isAuthenticated"
            >
              Галерея
            </v-btn>
            <v-btn 
              flat
              :to="{ name : routeNames.Login }"
              v-if="!isAuthenticated"
            >
              Войти
            </v-btn>
            <v-btn 
              flat
              :to="{ name : routeNames.Registration }"
              v-if="!isAuthenticated"
            >
              Зарегистрироваться
            </v-btn>
            <v-btn  
              flat
              @click="onLogout"
              v-if="isAuthenticated"
            >
              Выйти
            </v-btn>
        </v-toolbar-items>
      </v-toolbar>
      <v-content>
        <v-container fluid>
          <router-view></router-view>
        </v-container>
      </v-content>
      <v-footer
        height="auto"
        dark
      >
        <v-layout>
          <v-flex
            text-xs-center
            white--text
          >
            TulaWebCup 2019 <strong> Gallery</strong>
          </v-flex>
        </v-layout>
      </v-footer>
    </v-app>
  </div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {routeNames} from './support';
export default {
  name: 'app',
  data(){
    return {
      routeNames,
    }
  },
  computed:{
    ...mapGetters({
      isAuthenticated : 'auth/isAuthenticated'
    })
  },
  methods:{
    ...mapActions({
      logout : 'auth/logout'
    }),
    onLogout(){
      this.logout();
      this.$router.push("/");
    }
  }
}
</script>
