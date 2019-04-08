import Vue from 'vue'
import App from './App.vue'
import { sync } from 'vuex-router-sync';
import BootstrapVue from 'bootstrap-vue'

import {createStore} from './store/intex';
import {createRouter} from './router.js';

import '../public/index.css';

Vue.use(BootstrapVue);
Vue.config.productionTip = false


const store = createStore();
const router = createRouter(store);

sync(store,router)

new Vue({
  el: '#app',
  store,
  router,
  render: h => h(App)
});