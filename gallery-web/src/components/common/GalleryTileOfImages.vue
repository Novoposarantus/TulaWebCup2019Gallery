<template>
    <div>
        <image-container
            @openImage="openImage($event)"
            v-for="image in images"
            :key="image.id"
            :image="image"
        ></image-container>
    </div>
</template>

<script>
import ImageContainer from '@/components/common/GalleryImageContainer';
export default {
    components:{
        ImageContainer
    },
    props:{
        images: {
            type: Array,
            default: function(){
                return [];
            }
        }
    },
    methods:{
        openImage(image){
            this.$emit('openImage', image);
        }
    }
}
</script>

<style scoped>

</style>
