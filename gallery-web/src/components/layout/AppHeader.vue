<template>
    <v-toolbar app>
        <v-toolbar-title>Галерея</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-btn
              flat
              :to="{ name : routeNames.StartGallery }"
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
        <v-toolbar-title
          class="hidden-sm-and-down"
          v-if="isAuthenticated"
        >
          <v-icon>fas fa-user</v-icon>
          {{userName}}
        </v-toolbar-title>
      </v-toolbar>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
  routeNames,
  authGlobalGetters,
  authGlobalActions
} from '@/support';
export default {
  data(){
    return {
      routeNames,
    }
  },
  computed:{
    ...mapGetters({
      isAuthenticated : authGlobalGetters.isAuthenticated,
      userName : authGlobalGetters.userName
    })
  },
  methods:{
    ...mapActions({
      logout : authGlobalActions.logout
    }),
    onLogout(){
      this.logout();
      this.$router.push({name: routeNames.StartGallery});
    }
  }
}
</script>
