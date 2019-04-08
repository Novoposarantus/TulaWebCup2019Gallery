import Vue from 'vue';
import VueRouter from 'vue-router';

import Login from './pages/auth/Login';
import Registration from './pages/auth/Registration';
import Gallery from './pages/Gallery';

Gallery
Vue.use(VueRouter);


export function createRouter (store) {
    return new VueRouter({
        mode: 'history',
        routes : [
            {
                path: '/login',
                component :  Login
            },
            {
                path: '/registration',
                component :  Registration
            },
            {
                path: '/gallery',
                component :  Gallery
            },

        ]
    });
}