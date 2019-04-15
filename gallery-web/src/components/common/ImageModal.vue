<template>
    <v-dialog
        v-model="showDialog"
        max-width="700">
      <v-card class="card" v-if="!!image">
        <app-carusel></app-carusel>
        <v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">{{image.name}}</h3>
            </div>
          </v-card-title>
        <div class="image-rating-container" v-if="isAuthenticated">
            <v-rating
                v-model="rating"
                color="yellow darken-3"
                background-color="grey darken-1"
                empty-icon="$vuetify.icons.ratingFull"
                size="20"
                hover
            ></v-rating>
        </div>
        <div class="image-rating-container" v-if="!isAuthenticated">
            <v-rating
                v-model="rating"
                color="yellow darken-3"
                background-color="grey darken-1"
                empty-icon="$vuetify.icons.ratingFull"
                size="20"
                readonly
            ></v-rating>
        </div>
      </v-card>
      </v-dialog>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
    authGlobalGetters, 
    caruselGlobalGetters,
    caruselGlobalActions,
    galleryGlobalActions
} from '@/support';
import AppCarusel from '@/components/common/AppCarusel';

export default {
    components:{
        AppCarusel
    },
    computed:{
        ...mapGetters({
            isAuthenticated:  authGlobalGetters.isAuthenticated,
            image: caruselGlobalGetters.image
        }),
        showDialog:{
            get(){
                return !!this.image;
            },
            async set(value){
                if(value) return;
                this.setImage(null);
                await this.loadImages();
            }
        },
        rating:{
            get(){
                return this.image.userRating;
            },
            async set(value){
                await this.setRating({
                    scoreValue: value,
                    imageId: this.image.id
                });

            }
        }

    },
    methods:{
        ...mapActions({
            setImage: caruselGlobalActions.setImage,
            setRating : caruselGlobalActions.setRating,
            loadImages: galleryGlobalActions.loadImages
        }),
    }
}
</script>

<style scoped>
.image-rating-container{
    display: flex;
    justify-content: center;
}
.card{
    padding-bottom: 20px; 
}
</style>
