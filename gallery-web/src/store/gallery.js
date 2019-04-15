import {
    request,
    galleryState,
    galleryGetters,
    galleryActions, 
    galleryMutations,
    defaultGetters,
    defaultState,
    defaultActions,
    defaultMutations,
    sort,
    defaultClear,
} from '../support';

export const gallery = {
    namespaced: true,
    state:{
        ...defaultState,
        [galleryState.images] : [],
        [galleryState.filter] : {
            imagesOnPageCount: 20,   
            pageNumber: 1,
            tags: [],
            sortBy: sort.none,          
            reverseSort: false  
        },
        [galleryState.imagesCount]: 0
    },
    getters:{
        ...defaultGetters,
        [galleryGetters.images] : (state) => state[galleryState.images],
        [galleryGetters.filter] : (state) => state[galleryState.filter],
        [galleryGetters.imagesCount]: (state) => state[galleryState.imagesCount]
    },
    mutations:{
        ...defaultMutations,
        [galleryMutations.setImages]: (state, imagesData) => {
            state[galleryState.images] = [
                ...imagesData.images
            ];
            state[galleryState.imagesCount] = imagesData.imagesCount;
        },
        [galleryMutations.setFilter]: (state, filter) => {
            state[galleryState.filter] = {
                ...filter,
                pageNumber: Number(filter.pageNumber)
            }
        },
        [galleryMutations.setPageNumber]: (state, pageNumber) =>{
            state[galleryState.filter] = {
                ...state[galleryState.filter],
                pageNumber: Number(pageNumber)
            }
        },
        [galleryMutations.setImagesOnPageCount]: (state, imagesOnPageCount) =>{
            state[galleryState.filter] = {
                ...state[galleryState.filter],
                imagesOnPageCount
            }
        },
        [galleryMutations.setTags]: (state, tags) =>{
            state[galleryState.filter] = {
                ...state[galleryState.filter],
                tags : [
                    ...tags
                ]
            }
        },
        [galleryMutations.setSortBy]: (state, sortBy) =>{
            state[galleryState.filter] = {
                ...state[galleryState.filter],
                sortBy
            }
        },
        [galleryMutations.setReverseSort]: (state, reverseSort) =>{
            state[galleryState.filter] = {
                ...state[galleryState.filter],
                reverseSort
            }
        },
        [galleryMutations.clear]: (state) => {
            defaultClear(state);
        }
    },
    actions:{
        ...defaultActions,
        [galleryActions.loadImages]: async ({commit, state}) => {
            commit(galleryMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_IMAGES, 'POST', state[galleryState.filter]);
                commit(galleryMutations.setImages, json);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(galleryMutations.setError);
                }
                else{
                    commit(galleryMutations.setError, error.response.data);
                }
                commit(galleryMutations.finishLoading);
            }
        },
        [galleryActions.loadAllImages]: async ({commit, state})=>{
            commit(galleryMutations.startLoading);
            try {
                var filter = {
                    ...state[galleryState.filter],
                    imagesOnPageCount: 10000,   
                    pageNumber: 1,
                }
                const {json} = await request(process.env.VUE_APP_IMAGES, 'POST', filter);
                commit(galleryMutations.setImages, json);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(galleryMutations.setError);
                }
                else{
                    commit(galleryMutations.setError, error.response.data);
                }
                commit(galleryMutations.finishLoading);
            }
        },
        [galleryActions.setPageNumber]: async ({commit, dispatch}, pageNumber)=>{
            commit(galleryMutations.setPageNumber, pageNumber);
            await dispatch(galleryActions.loadImages);
        },
        [galleryActions.setImagesOnPageCount]: async ({commit, dispatch}, imagesOnPageCount)=>{
            commit(galleryMutations.setImagesOnPageCount, imagesOnPageCount);
            await dispatch(galleryActions.loadImages);
        },
        [galleryActions.setTags]: async ({commit, dispatch}, tags)=>{
            commit(galleryMutations.setTags, tags);
            await dispatch(galleryActions.loadImages);
        },
        [galleryActions.setSortBy]: async ({commit, dispatch}, sortBy)=>{
            commit(galleryMutations.setSortBy, sortBy);
            await dispatch(galleryActions.loadImages);
        },
        [galleryActions.setReverseSort]: async ({commit, dispatch}, reverseSort)=>{
            commit(galleryMutations.setReverseSort, reverseSort);
            await dispatch(galleryActions.loadImages);
        },
        [galleryActions.saveImages]: async ({commit}, images)=>{
            commit(galleryMutations.startLoading);
            try {
                await request(process.env.VUE_APP_IMAGE, 'POST', images);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(galleryMutations.setError);
                }
                else{
                    commit(galleryMutations.setError, error.response.data);
                }
                commit(galleryMutations.finishLoading);
            }
        }
    }
};
