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
    filter : 'FILTER',
    imagesCount: 'IMAGES_COUNT'
}

export const galleryGetters = {
    ...galleryState     
}

export const galleryGlobalGetters = {
    ...defaultGlobalGettersNames(galleryNameSapace),
    images : `${galleryNameSapace}/${galleryGetters.images}`,
    filter : `${galleryNameSapace}/${galleryGetters.filter}`, 
    imagesCount : `${galleryNameSapace}/${galleryGetters.imagesCount}`,     
}

export const galleryMutations = {
    ...defaultMutationsNames,
    setImages: 'SET_IMAGES',
    setFilter: 'SET_FILTER',
    setPageNumber: 'SET_PAGE_NUMBER',
    setImagesOnPageCount : 'SET_IMAGES_ON_PAGE_COUNT',
    setTags : 'SET_TAGS',
    setSortBy : 'SET_SORT_BY',
    setReverseSort: 'SET_REVERSE_SORT'
}

export const galleryActions = {
    ...defaultActionsNames,
    loadImages: 'LOAD_IMAGES',
    saveImages: 'SAVE_IMAGES',
    setPageNumber: 'SET_PAGE_NUMBER',
    setImagesOnPageCount : 'SET_IMAGES_ON_PAGE_COUNT',
    setTags : 'SET_TAGS',
    setSortBy : 'SET_SORT_BY',
    setReverseSort: 'SET_REVERSE_SORT',
    loadAllImages: 'LOAD_ALL_IMAGES',
}

export const galleryGlobalActions = {
    ...defaultGlobalActionsNames(galleryNameSapace),
    loadImages: `${galleryNameSapace}/${galleryActions.loadImages}`,
    setPageNumber: `${galleryNameSapace}/${galleryActions.setPageNumber}`,
    setImagesOnPageCount : `${galleryNameSapace}/${galleryActions.setImagesOnPageCount}`,
    setTags : `${galleryNameSapace}/${galleryActions.setTags}`,
    setSortBy : `${galleryNameSapace}/${galleryActions.setSortBy}`,
    setReverseSort: `${galleryNameSapace}/${galleryActions.setReverseSort}`,
    saveImages: `${galleryNameSapace}/${galleryActions.saveImages}`,
    loadAllImages: `${galleryNameSapace}/${galleryActions.loadAllImages}`,
}
