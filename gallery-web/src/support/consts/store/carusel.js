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
    image : 'IMAGE'
}

export const caruselGetters = {
    ...caruselState     
}

export const caruselGlobalGetters = {
    ...defaultGlobalGettersNames(caruselNameSapace),
    image : `${caruselNameSapace}/${caruselGetters.image}`      
}

export const caruselMutations = {
    ...defaultMutationsNames,
    setImage: 'SET_IMAGE'
}

export const caruselActions = {
    ...defaultActionsNames,
    loadImage: 'LOAD_IMAGE',
    setImage: 'SET_IMAGE',
    next: 'NEXT',
    prev: 'PREV',
    setRating: 'SET_RATING'
}

export const caruselGlobalActions = {
    ...defaultGlobalActionsNames(caruselNameSapace),
    loadImage: `${caruselNameSapace}/${caruselActions.loadImage}`,
    setImage: `${caruselNameSapace}/${caruselActions.setImage}`,
    next: `${caruselNameSapace}/${caruselActions.next}`,
    prev: `${caruselNameSapace}/${caruselActions.prev}`,
    setRating: `${caruselNameSapace}/${caruselActions.setRating}`,
}
