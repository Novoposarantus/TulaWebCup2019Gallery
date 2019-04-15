import {
    request,
    caruselState,
    caruselGetters,
    caruselActions, 
    caruselMutations,
    defaultGetters,
    defaultState,
    defaultActions,
    defaultMutations,
    galleryGlobalGetters,
    defaultClear,
} from '../support';

export const carusel = {
    namespaced: true,
    state:{
        ...defaultState,
        [caruselState.images] : [],
        [caruselState.imagesCount]: 0
    },
    getters:{
        ...defaultGetters,
        [caruselGetters.images] : (state) => state[caruselState.images],
        [caruselGetters.imagesCount]: (state) => state[caruselState.imagesCount]
    },
    mutations:{
        ...defaultMutations,
        [caruselMutations.setImages]: (state, imagesData) => {
            state[caruselState.images] = [
                ...imagesData.images
            ];
            state[caruselState.imagesCount] = imagesData.imagesCount;
        },
        [caruselMutations.clear]: (state) => {
            defaultClear(state);
        }
    },
    actions:{
        ...defaultActions,
        [caruselActions.loadImages]: async ({commit, rootState}) => {
            commit(caruselMutations.startLoading);
            try {
                var filter = {
                    ...rootState[galleryGlobalGetters.filter],
                    imagesOnPageCount: 10000,   
                    pageNumber: 1,
                }
                const {json} = await request(process.env.VUE_APP_IMAGES, 'POST', filter);
                commit(caruselMutations.setImages, json);
            }
            catch (error) {
                if(!error.response || error.response.status !== 400){
                    commit(caruselMutations.setError);
                }
                else{
                    commit(caruselMutations.setError, error.response.data);
                }
                commit(caruselMutations.finishLoading);
            }
        },
    }
};
