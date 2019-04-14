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
    galleryNameSapace
} from '../support';

export const [galleryNameSapace] = {
    namespaced: true,
    state:{
        ...defaultState,
        [galleryState.images] : [],
        [galleryState.filter] : {}
    },
    getters:{
        ...defaultGetters,
        [galleryGetters.images] : (state) => state[galleryState.images],
        [galleryGetters.filter] : (state) => state[galleryState.filter]
    },
    mutations:{
        ...defaultMutations,
        [galleryMutations.setImages]: (state, images) => {
            state.images = [
                ...images
            ]
        },
        [galleryMutations.setFilter]: (state, filter) => {
            state.filter = {
                ...filter
            }
        },
        [galleryMutations.clear]: (state) => {
            state.images = [];
            state.isLoading = false;
            state.error = null;
        }
    },
    actions:{
        ...defaultActions,
        [galleryActions.loadImages]: async ({commit}, filter) => {
            commit(galleryMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_IMAGES, 'GET', filter);
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
        
    }
};
