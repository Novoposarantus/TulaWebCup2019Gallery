export const defaultStateNames = {
    isLoading : 'IS_LOADING',
    error: 'ERROR'
}

export function defaultGlobalGettersNames(nameSpace) {
    return {
        isLoading : `${nameSpace}/${defaultStateNames.isLoading}`,
        error: `${nameSpace}/${defaultStateNames.error}`
    }
}

export const defaultMutationsNames = {
    startLoading: 'START_LOADING' ,
    finishLoading: 'FINISH_LOADING',
    setError: 'SET_ERROR',
    clear: 'CLEAR'
}

export const defaultActionsNames = {
    closeError: 'CLOSE_ERROR'
}

export function defaultGlobalActionsNames(nameSpace) {
    return {
        closeError : `${nameSpace}/${defaultActions.closeError}`,
    }
}

export const defaultState = {
    [defaultStateNames.isLoading] : false,
    [defaultStateNames.error]: null
}

export const defaultGetters = {
    [defaultStateNames.isLoading] : (state) => state[defaultStateNames.isLoading],
    [defaultStateNames.error] : (state) => state[defaultStateNames.error]
}

export const defaultMutations = {
    [defaultMutationsNames.startLoading]: (state) => {
        state.error = null;
        state.isLoading = true;
    },
    [defaultMutationsNames.finishLoading]: (state) => {
        state.isLoading = false;
    },
    [defaultMutationsNames.setError]: (state, error = 'Что-то пошло не так. Повторите попытку позже.') => {
        state.error = error;
    },
}

export const defaultActions = {
    [defaultActionsNames.closeError]: ({commit}) => {
        commit(defaultMutationsNames.setError, null);
    }
}