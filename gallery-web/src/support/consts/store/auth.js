import {
    defaultStateNames,
    defaultGlobalGettersNames,
    defaultMutationsNames, 
    defaultActionsNames,
} from './default';

export const authNameSpace = 'AUTH';

export const authState = {
    ...defaultStateNames,
    isAuthenticated = 'IS_AUTHENTICATED',
    userData = 'USER_DATA'
}

export const authGetters = {
    ...authStateNames,
    userName: 'USER_NAME',
    roleId : 'ROLE_ID',      
}

export const authGlobalGetters = {
    ...defaultGlobalGettersNames(authNameSpace),
    isAuthenticated : `${authNameSpace}/${authGettersNames.isAuthenticated}`,
    userName        : `${authNameSpace}/${authGettersNames.userName}`,    
    roleId          : `${authNameSpace}/${authGettersNames.roleId}`,      
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
}