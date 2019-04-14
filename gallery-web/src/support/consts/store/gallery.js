import {
    defaultStateNames,
    defaultGlobalGettersNames,
    defaultMutationsNames, 
    defaultActionsNames,
    defaultGlobalActionsNames
} from './default';

export const galleryNameSapace = 'gallery'

export const galleryState = {
    ...defaultStateNames,
    images : 'IMAGES',
    filter : 'FILTER'
}

export const galleryGetters = {
    ...galleryState     
}

export const galleryGlobalGetters = {
    ...defaultGlobalGettersNames(galleryNameSapace),
    images : `${galleryNameSapace}/${galleryGetters.images}`,
    filter : `${galleryNameSapace}/${galleryGetters.filter}`,     
}

export const galleryMutations = {
    ...defaultMutationsNames,
    setImages: 'SET_IMAGES',
    setFilter: 'SET_FILTER',
    setPageNumaber: 'SET_PAGE_NUMBER',
    setImagesOnPageCount : 'SET_IMAGES_ON_PAGE_COUNT',
    setTags : 'SET_TAGS',
    setSortBy : 'SET_SORT_BY',
    setReverseSort: 'SET_REVERSE_SORT'
}

export const galleryActions = {
    ...defaultActionsNames,
    loadImages: 'LOAD_IMAGES',
    setPageNumaber: 'SET_PAGE_NUMBER',
    setImagesOnPageCount : 'SET_IMAGES_ON_PAGE_COUNT',
    setTags : 'SET_TAGS',
    setSortBy : 'SET_SORT_BY',
    setReverseSort: 'SET_REVERSE_SORT'
}

export const galleryGlobalActions = {
    ...defaultGlobalActionsNames(galleryNameSapace),
    loadImages: `${galleryNameSapace}/${galleryActions.loadImages}`,
    setPageNumaber: `${galleryNameSapace}/${galleryActions.setPageNumaber}`,
    setImagesOnPageCount : `${galleryNameSapace}/${galleryActions.setImagesOnPageCount}`,
    setTags : `${galleryNameSapace}/${galleryActions.setTags}`,
    setSortBy : `${galleryNameSapace}/${galleryActions.setSortBy}`,
    setReverseSort: `${galleryNameSapace}/${galleryActions.setReverseSort}`,
}
