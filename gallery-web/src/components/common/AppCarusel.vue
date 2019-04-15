<template>
    <v-img
    v-if="!!image"
    :src="image.imageContent"
    class="white--text"
    contain
    >
    <div class="carusel-arrows">
        <v-icon 
            class="icon-hover"
            v-if="!disablePrev"
            @click="prev">
            fas fa-chevron-left
        </v-icon>
        <v-spacer></v-spacer>
        <v-icon 
            class="icon-hover"
            @click="next"
            v-if="!disableNext">
            fas fa-chevron-right
        </v-icon>
    </div>
    </v-img>
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
    padding: 20px;
    height: 100%;
}
.icon-hover{
    background-color: white;
    padding: 5px 20px;
    opacity: 0.3;
}
.icon-hover:hover{
    opacity: 0.7;
    box-shadow: 0 0 10px rgba(0,0,0,0.5);
}
</style>

