import Vue from 'vue';
import VueRouter from 'vue-router';

import LoginView from '@/views/auth/LoginView';
import RegistrationView from '@/views/auth/RegistrationView';
import GalleryView from '@/views/GalleryView';
import ImagesLoaderView from '@/views/ImagesLoaderView';

import {
    routeNames,
    authGlobalGetters,
    galleryGlobalActions,
    galleryGlobalGetters
} from './support';

Vue.use(VueRouter);

export function createRouter (store) {
    function ifNotAuthenticated(next){
        if (store.getters[authGlobalGetters.isAuthenticated]) {
            next({name: routeNames.StartGallery});
            return false;
        }
        return true;
    }
 
    function ifAuthenticated(next){
        if (!store.getters[authGlobalGetters.isAuthenticated]) {
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
                name : routeNames.Start,
                redirect: () =>({
                    name: routeNames.StartGallery
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
                name: routeNames.StartGallery,
                redirect: () =>({
                    name: routeNames.Gallery,
                    params: {pageNumber: 1}
                })
            },
            {
                path: `/gallery/:pageNumber`,
                name: routeNames.Gallery,
                component :  GalleryView,
                beforeEnter: async (to, _from, next) =>{
                    store.dispatch(galleryGlobalActions.setPageNumber, to.params.pageNumber);
                    if(store.getters[galleryGlobalGetters.error]) {
                        next({name:routeNames.StartGallery});
                        return;
                    }
                    next();
                }
            },
            {
                path: `/load-images`,
                name: routeNames.LoadImages,
                component :  ImagesLoaderView,
                beforeEnter: async (_to, _from, next) =>{
                    if(!ifAuthenticated(next)){
                        return;
                    }
                    next();
                }
            }
        ]
    });
}