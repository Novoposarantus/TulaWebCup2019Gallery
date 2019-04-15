<template>
    <div>
        <v-card>
          <v-img
            class="white--text"
            height="400px"
            :src="image.ImageContent"
          >
            <div class="carusel-arrows">
                <v-icon 
                    v-if="!disablePrev"
                    @click="prev">
                    fas fa-chevron-left
                </v-icon>
                <v-spacer></v-spacer>
                <v-icon 
                    @click="next"
                    v-if="!disableNext">
                    fas fa-chevron-right
                </v-icon>
            </div>
          </v-img>
        </v-card>
    </div>
</template>

<script>
import {mapGetters} from 'vuex';
import {galleryGlobalGetters} from '@/support';
export default {
    props:{
        image: Object
    },
    computed:{
        ...mapGetters({
            images: galleryGlobalGetters.images
        }),
        disablePrev(){
            return this.images.indexOf(this.currentImage) == 0;
        },
        disableNext(){
            return this.images.indexOf(this.currentImage) == this.images.length - 1;
        }
    },
    methods:{
        next(){
            this.$emit('next');
        },
        prev(){
            this.$emit('prev');
        }
    },
}
</script>

<style scoped>
.carusel-arrows{
    display: flex;
    align-items: center;
}
</style>

