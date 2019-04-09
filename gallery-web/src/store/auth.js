import Cookie from 'js-cookie';
import {request} from '../support';

export const auth = {
    namespaced: true,
    state:{
        isAuthenticated : !!Cookie.get('user-token'),
        isLoading       : false,
        error           : null,
        userData        : null
    },
    getters:{
        isAuthenticated    : (state) => state.isAuthenticated,
        isLoading          : (state) => state.isLoading,
        error              : (state) => state.error,
        userData           : (state) => state.userData,
        roleId             : (state) => state.userData ? state.userData.roleId : null,
    },
    mutations:{
        AUTH_SUCCESS: (state) => {
            state.isAuthenticated = true;
        },
        AUTH_ERROR: (state,error = 'Что-то пошло не так. Повторите попытку позже.') => {
            state.isAuthenticated = false;
            state.error = error;
        },
        AUTH_LOGOUT: (state)=>{
            state.isAuthenticated = false;
        },
        START_LOADING: (state) => {
            state.error = null;
            state.isLoading = true;
        },
        FINISH_LOADING: (state) => {
            state.isLoading = false;
        },
        SET_ERROR: (state, error = 'Что-то пошло не так. Повторите попытку позже.') => {
            state.error = error;
        },
        SET_AUTH_INFO(state, userData){
            state.userData = {
                ...userData,
                password: null,
                passwordConfirm: null
            }
        },
        CLEAR(state){
            state.isAuthenticated = false;
            state.isLoading = false;
            state.error = null;
            state.userData = null;
        }
    },
    actions:{
        authentication: async ({commit, dispatch}, user)=>{
            commit('START_LOADING');
            try {
                const {json} = await request(process.env.VUE_APP_AUTHENTICATION, 'POST', user);
                dispatch('logout');
                Cookie.set('user-token', json.access_token, { expires: json.timeOut * 60 });
                commit('AUTH_SUCCESS');
                // await dispatch('getUserInfo');
            }
            catch (error) {
                if(!error.response || error.response.status !== 401){
                    commit('AUTH_ERROR');
                }
                else{
                    commit('AUTH_ERROR', error.response.data);
                }
                Cookie.remove('user-token');
                commit('FINISH_LOADING');
            }
        },
        logout: ({commit, dispatch}) => {
            Cookie.remove('user-token');
            commit('AUTH_LOGOUT');
            dispatch('clearStore', null , {root: true})
        },
        register: async ({commit}, user)=>{
            commit('START_LOADING');
            try {
                await request(process.env.VUE_APP_USERS, 'POST', user);
                commit('FINISH_LOADING');
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit('AUTH_ERROR');
                }
                else{
                    commit('AUTH_ERROR', error.response.data);
                }
                commit('FINISH_LOADING');
            }
        },
        getUserInfo: async ({commit})=>{
            commit('START_LOADING');
            try {
                const {json} = await request(process.env.VUE_APP_USER_INFO, 'GET');
                commit('SET_AUTH_INFO', json);
                commit('FINISH_LOADING');
            }
            catch (error) {
                commit('SET_ERROR');
                commit('FINISH_LOADING');
            }
        },
        closeError({commit}){
            commit('SET_ERROR', null);
        }
    }
};
