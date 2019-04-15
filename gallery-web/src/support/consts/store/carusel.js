import {
    defaultStateNames,
    defaultGlobalGettersNames,
    defaultMutationsNames, 
    defaultActionsNames,
    defaultGlobalActionsNames
} from './default';

export const caruselNameSapace = 'carusel'

export const caruselState = {
    ...defaultStateNames,
    image : 'IMAGE',
    isFirst : 'IS_FIRST',
    isLast : 'IS_LAST'
}

export const caruselGetters = {
    ...caruselState     
}

export const caruselGlobalGetters = {
    ...defaultGlobalGettersNames(caruselNameSapace),
    image : `${caruselNameSapace}/${caruselGetters.image}`,
    isFirst : `${caruselNameSapace}/${caruselGetters.isFirst}`,
    isLast : `${caruselNameSapace}/${caruselGetters.isLast}`,         
}

export const caruselMutations = {
    ...defaultMutationsNames,
    setImage: 'SET_IMAGE'
}

export const caruselActions = {
    ...defaultActionsNames,
    loadImage: 'LOAD_IMAGE'
}

export const caruselGlobalActions = {
    ...defaultGlobalActionsNames(caruselNameSapace),
    loadImage: `${caruselNameSapace}/${caruselActions.loadImage}`,
}
