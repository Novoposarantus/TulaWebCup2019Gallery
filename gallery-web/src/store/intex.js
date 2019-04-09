import Vue from 'vue';
import Vuex from 'vuex';

import {auth} from './auth';

Vue.use(Vuex);

const modules = {
    auth,
}

export function createStore() {
    return new Vuex.Store({
        modules,
        actions:{
            clearStore({commit}){
                for(let moduleName in modules){
                    commit(`${moduleName}/CLEAR`);
                }
            }
        }
    });
}