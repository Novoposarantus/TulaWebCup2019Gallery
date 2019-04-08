import Vue from 'vue';
import Vuex from 'vuex';

import {auth} from './auth';

Vue.use(Vuex);

export function createStore() {
    return new Vuex.Store({
        modules:{
            auth
        }
    });
}