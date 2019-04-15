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
    images : 'IMAGES',
    imagesCount : 'IMAGES_COUNT'
}

export const caruselGetters = {
    ...caruselState     
}

export const caruselGlobalGetters = {
    ...defaultGlobalGettersNames(caruselNameSapace),
    images : `${caruselNameSapace}/${caruselGetters.images}`,
    imagesCount : `${caruselNameSapace}/${caruselGetters.imagesCount}`,     
}

export const caruselMutations = {
    ...defaultMutationsNames,
    setImages: 'SET_IMAGES'
}

export const caruselActions = {
    ...defaultActionsNames,
    loadImages: 'LOAD_IMAGES'
}

export const caruselGlobalActions = {
    ...defaultGlobalActionsNames(caruselNameSapace),
    loadImages: `${caruselNameSapace}/${caruselActions.loadImages}`,
}
