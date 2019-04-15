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
        [caruselState.image] : null,
        [caruselState.isFirst] : false,
        [caruselState.isLast] : false,
    },
    getters:{
        ...defaultGetters,
        [caruselGetters.image] : (state) => state[caruselState.image],
        [caruselGetters.imagesCount]: (state) => state[caruselState.imagesCount]
    },
    mutations:{
        ...defaultMutations,
        [caruselMutations.setImage]: (state, imagesData) => {
            state[caruselState.images] = [
                ...imagesData.images
            ];
        },
        [caruselMutations.clear]: (state) => {
            defaultClear(state);
            state[caruselState.image] = null;
            state[caruselState.isFirst] = false;
            state[caruselState.isLast] = false;
        }
    },
    actions:{
        ...defaultActions,
        [caruselActions.loadImage]: async ({commit, rootState}, imageId) => {
            commit(caruselMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_IMAGE + '/GetImage', 'POST', {
                    ...rootState[galleryGlobalGetters.filter],
                    id: imageId
                });
                commit(caruselMutations.setImage, json);
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
