import Vue from 'vue';
import Vuex from 'vuex';
import {AUTH} from './auth';
import {GALLERY} from './gallery';

import {
    defaultMutations,
    globalActions
} from '@/support';

Vue.use(Vuex);

const modules = {
    AUTH,
    GALLERY
}

export function createStore() {
    return new Vuex.Store({
        modules,
        actions:{
            [globalActions.clearStore] : ({commit}) => {
                for(let moduleName in modules){
                    commit(`${moduleName}/${defaultMutations.clear}`);
                }
            }
        }
    });
}