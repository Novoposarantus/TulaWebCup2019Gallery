<template>
    <div class="image-container">
        <v-hover>
            <v-card
                slot-scope="{ hover }"
                :class="`elevation-${hover ? 12 : 2}`"
                @click="openImage"
                class="image-container-card">
                <v-img
                :src="image.imageContent"
                height="170"
                contain
                ></v-img>
                <div class="image-rating-container">
                    <v-rating
                        v-model="image.rating"
                        color="yellow darken-3"
                        background-color="grey darken-1"
                        empty-icon="$vuetify.icons.ratingFull"
                        size="10"
                    ></v-rating>
                </div>
            </v-card>
        </v-hover>
    </div>
</template>

<script>
import {mapActions} from 'vuex';
import {caruselGlobalActions} from '@/support';

export default {
    props:{
        image: Object
    },
    methods:{
        ...mapActions({
            loadImage : caruselGlobalActions.loadImage
        }),
        openImage(){
            this.loadImage(this.image.id);
        }
    }
}
</script>

<style scoped>
.image-container{
    display: inline-block;
    width: 200px;
    cursor: pointer;
    margin: 3px;
}
.image-rating-container{
    display: flex;
    justify-content: center;
}
</style>
