import Vue from 'vue';
import Vuex from 'vuex';
import {auth} from './auth';
import {gallery} from './gallery';

import {
    defaultMutationsNames,
    globalActions
} from '@/support';

Vue.use(Vuex);

const modules = {
    auth,
    gallery,
    carusel
}

export function createStore() {
    return new Vuex.Store({
        modules,
        actions:{
            [globalActions.clearStore] : ({commit}) => {
                for(let moduleName in modules){
                    commit(`${moduleName}/${defaultMutationsNames.clear}`);
                }
            }
        }
    });
}