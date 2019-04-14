<template>
    <div>
        <image-container
            v-for="image in images"
            :key="image.id"
            :image="image"
        ></image-container>
    </div>
</template>

<script>
import {mapGetters} from 'vuex';
import ImageContainer from '@/components/common/ImageContainer'
import {
    galleryGlobalGetters
} from '@/support';
export default {
    components:{
        ImageContainer
    },
    computed:{
        ...mapGetters({
            images: galleryGlobalGetters.images
        })
    }
}
</script>

<style scoped>
    
</style>