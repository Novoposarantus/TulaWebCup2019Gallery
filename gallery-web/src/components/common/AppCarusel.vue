<template>
    <v-img
    :src="image.imageContent"
    class="white--text carusel-image"
    contain
    >
    <div class="carusel-arrows">
        <v-icon 
            class="icon-hover"
            v-if="!disablePrev"
            @click="prev">
            fas fa-chevron-left
        </v-icon>
        <v-icon 
            class="icon-hover-disable"
            v-if="disablePrev">
            fas fa-chevron-left
        </v-icon>
        <v-spacer></v-spacer>
        <v-icon 
            class="icon-hover"
            @click="next"
            v-if="!disableNext">
            fas fa-chevron-right
        </v-icon>
        <v-icon 
            class="icon-hover-disable"
            v-if="disableNext">
            fas fa-chevron-right
        </v-icon>
    </div>
    </v-img>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {caruselGlobalGetters, caruselGlobalActions} from '@/support';
export default {
    computed:{
        ...mapGetters({
            image: caruselGlobalGetters.image
        }),
        disablePrev(){
            return this.image.isFirst;
        },
        disableNext(){
            return this.image.isLast;
        }
    },
    methods:{
        ...mapActions({
            nextImage: caruselGlobalActions.next,
            prevImage: caruselGlobalActions.prev
        }),
        next(){
            this.nextImage(this.image.id);
        },
        prev(){
            this.prevImage(this.image.id);
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
.carusel-image{
    max-height: 400px;
}
</style>

