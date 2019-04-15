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
            imagesOnPageCount: 50,   
            pageNumber: 1,
            tags: [],
            sortBy: sort.none,          
            reverseSort: false  
        }
    },
    getters:{
        ...defaultGetters,
        [galleryGetters.images] : (state) => state[galleryState.images],
        [galleryGetters.filter] : (state) => state[galleryState.filter]
    },
    mutations:{
        ...defaultMutations,
        [galleryMutations.setImages]: (state, images) => {
            state[galleryState.images] = [
                ...images
            ]
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
        [galleryActions.setPageNumber]: ({commit}, pageNumber)=>{
            commit(galleryMutations.setPageNumber, pageNumber);
        },
        [galleryActions.setImagesOnPageCount]: ({commit}, imagesOnPageCount)=>{
            commit(galleryMutations.setImagesOnPageCount, imagesOnPageCount);
        },
        [galleryActions.setTags]: ({commit}, tags)=>{
            commit(galleryMutations.setTags, tags);
        },
        [galleryActions.setSortBy]: ({commit}, sortBy)=>{
            commit(galleryMutations.setSortBy, sortBy);
        },
        [galleryActions.setReverseSort]: ({commit}, reverseSort)=>{
            commit(galleryMutations.setReverseSort, reverseSort);
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
