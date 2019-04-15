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
    }
}
</script>

<style scoped>

</style>
