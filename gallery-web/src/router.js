import Vue from 'vue';
import VueRouter from 'vue-router';

import LoginView from './views/auth/LoginView';
import RegistrationView from './views/auth/RegistrationView';
import GalleryView from './views/GalleryView';

import {routeNames} from './support'

Vue.use(VueRouter);

export function createRouter (store) {
    function ifNotAuthenticated(next){
        if (store.getters['auth/isAuthenticated']) {
            next({name: routeNames.Gallery});
            return false;
        }
        return true;
    }
      
    function ifAuthenticated(next){
        if (!store.getters['auth/isAuthenticated']) {
            next({name: routeNames.Login});
            return false;
        }
    
        return true;
    }

    return new VueRouter({
        mode: 'history',
        routes : [
            {
                path: '/',
                redirect: () =>({
                    name: store.getters['auth/isAuthenticated']
                        ? routeNames.Gallery
                        : routeNames.Login
                })
            },
            {
                path: '/login',
                name: routeNames.Login,
                component :  LoginView,
                beforeEnter: (_to, _from, next) => {
                    if(!ifNotAuthenticated(next)) return;
                    next();
                }
            },
            {
                path: '/registration',
                name: routeNames.Registration,
                component :  RegistrationView,
                beforeEnter: (_to, _from, next) => {
                    if(!ifNotAuthenticated(next)) return;
                    next();
                }
            },
            {
                path: `/gallery`,
                name: routeNames.Gallery,
                component :  GalleryView,
                beforeEnter: (_to, _from, next) => {
                    if(!ifAuthenticated(next)) return;
                    next();
                }
            },

        ]
    });
}