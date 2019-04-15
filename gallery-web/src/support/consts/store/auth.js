import {
    defaultStateNames,
    defaultGlobalGettersNames,
    defaultMutationsNames, 
    defaultActionsNames,
    defaultGlobalActionsNames
} from './default';

export const authNameSpace = 'auth';

export const authState = {
    ...defaultStateNames,
    isAuthenticated : 'IS_AUTHENTICATED',
    userData : 'USER_DATA'
}

export const authGetters = {
    ...authState,
    userName: 'USER_NAME',
    roleId : 'ROLE_ID',      
}

export const authGlobalGetters = {
    ...defaultGlobalGettersNames(authNameSpace),
    isAuthenticated : `${authNameSpace}/${authGetters.isAuthenticated}`,
    userName        : `${authNameSpace}/${authGetters.userName}`,    
    roleId          : `${authNameSpace}/${authGetters.roleId}`,      
}

export const authMutations = {
    ...defaultMutationsNames,
    authSuccess: 'AUTH_SUCCESS',
    authError: 'AUTH_ERROR',
    authLogout:'AUTH_LOGOUT',
    setUserInfo:'SET_AUTH_INFO',
}

export const authActions = {
    ...defaultActionsNames,
    authentication: 'AUTHENTICATION',
    logout: 'LOGOUT',
    register: 'REGISTER',
    loadUserData: 'LOAD_USER_DATA'
}

export const authGlobalActions = {
    ...defaultGlobalActionsNames(authNameSpace),
    authentication: `${authNameSpace}/${authActions.authentication}`,
    logout: `${authNameSpace}/${authActions.logout}`,
    register: `${authNameSpace}/${authActions.register}`,
    loadUserData: `${authNameSpace}/${authActions.loadUserData}`,
}