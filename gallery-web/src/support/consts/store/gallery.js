import {
    defaultStateNames,
    defaultGlobalGettersNames,
    defaultMutationsNames, 
    defaultActionsNames
} from './default';

export const galleryNameSapace = 'GALLERY'

export const galleryState = {
    ...defaultStateNames,
    images = 'IMAGES',
    filter = 'FILTER'
}

export const galleryGetters = {
    ...authStateNames     
}

export const galleryGlobalGetters = {
    ...defaultGlobalGettersNames(galleryNameSapace),
    images : `${galleryNameSapace}/${galleryGettersNames.images}`,
    filter        : `${galleryNameSapace}/${galleryGettersNames.filter}`,     
}

export const galleryMutations = {
    ...defaultMutationsNames,
    setImages: 'SET_IMAGES',
    setFilter: 'SET_FILTER'
}

export const galleryActions = {
    ...defaultActionsNames,
    loadImages: 'LOAD_IMAGES' 
}