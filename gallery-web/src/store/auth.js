import Cookie from 'js-cookie';
import {
    request,
    defaultGetters,
    defaultState,
    defaultActions,
    defaultMutations,
    authState,
    authGetters,
    authActions,
    authMutations,
    globalActions
} from '../support';

export const auth = {
    namespaced: true,
    state:{
        ...defaultState,
        [authState.isAuthenticated] : !!Cookie.get('user-token'),
        [authState.userData]        : null
    },
    getters:{
        ...defaultGetters,
        [authGetters.isAuthenticated]    : (state) => state[authState.isAuthenticated],
        [authGetters.userName]           : (state) => state[authState.userData] 
                                                    ? state[authState.userData].userName 
                                                    : null,
        [authGetters.roleId]             : (state) => state[authState.userData] 
                                                    ? state[authState.userData].roleId 
                                                    : null,
    },
    mutations:{
        ...defaultMutations,
        [authMutations.authSuccess]: (state) => {
            state.isAuthenticated = true;
        },
        [authMutations.authError]: (state,error = 'Что-то пошло не так. Повторите попытку позже.') => {
            state.isAuthenticated = false;
            state.error = error;
        },
        [authMutations.authLogout]: (state)=>{
            state.isAuthenticated = false;
        },
        [authMutations.setUserInfo]: (state, userData) =>{
            state.userData = {
                ...userData
            }
        },
        [authMutations.clear](state){
            state.isAuthenticated = false;
            state.isLoading = false;
            state.error = null;
            state.userData = null;
        }
    },
    actions:{
        ...defaultActions,
        [authActions.authentication]: async ({commit, dispatch}, user)=>{
            commit(authMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_AUTHENTICATION, 'POST', user);
                dispatch(authActions.logout);
                Cookie.set('user-token', json.access_token, { expires: json.timeOut * 60 });
                commit(authMutations.setUserInfo, {
                    userName: json.userName,
                    roleId : json.roleId
                })
                commit(authMutations.authSuccess);
            }
            catch (error) {
                if(!error.response || error.response.status !== 401){
                    commit(authMutations.authError);
                }
                else{
                    commit(authMutations.authError, error.response.data);
                }
                Cookie.remove('user-token');
                commit(authMutations.finishLoading);
            }
        },
        [authActions.logout]: ({commit, dispatch}) => {
            Cookie.remove('user-token');
            commit(authMutations.authLogout);
            dispatch(globalActions.clearStore, null , {root: true})
        },
        [authActions.register]: async ({commit}, user)=>{
            commit(authMutations.startLoading);
            try {
                await request(process.env.VUE_APP_USERS, 'POST', user);
                commit(authMutations.finishLoading);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(authMutations.authError);
                }
                else{
                    commit(authMutations.authError, error.response.data);
                }
                commit(authMutations.finishLoading);
            }
        }
    }
};
