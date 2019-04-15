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
    },
    getters:{
        ...defaultGetters,
        [caruselGetters.image] : (state) => state[caruselState.image]
    },
    mutations:{
        ...defaultMutations,
        [caruselMutations.setImage]: (state, image) => {
            if(image == null){
                state[caruselState.image] = null;
                return;
            }
            state[caruselState.image] = {
                ...image
            };
        },
        [caruselMutations.clear]: (state) => {
            defaultClear(state);
            state[caruselState.image] = null;
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
        [caruselActions.next]: async ({commit, rootState}, imageId) => {
            commit(caruselMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_IMAGE + '/GetNext', 'POST', {
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
        [caruselActions.prev]: async ({commit, rootState}, imageId) => {
            commit(caruselMutations.startLoading);
            try {
                const {json} = await request(process.env.VUE_APP_IMAGE + '/GetPrev', 'POST', {
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
        [caruselActions.setImage]: ({commit}, image) => {
            commit(caruselMutations.setImage, image);
        },
        [caruselActions.setRating]: async ({commit, state}, ratingData) => {
            commit(caruselMutations.startLoading);
            try {
                let {json} =  await request(process.env.VUE_APP_SCORE, 'POST', ratingData);
                commit(caruselMutations.setImage, {
                    ...json,
                    userRating: ratingData.scoreValue,
                    isFirst: state[caruselState.image].isFirst,
                    isLast: state[caruselState.image].isLast
                })
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
