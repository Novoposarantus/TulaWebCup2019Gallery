<template>
    <div class="tile-of-images">
        <image-container
            v-for="image in images"
            :key="image.imageContent"
            :image="image"
            @nameChange="nameChange"
        ></image-container>
    </div>
</template>

<script>
import ImageContainer from '@/components/common/LoaderImageContainer';
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
        nameChange({image, newName}){
            this.$emit('nameChange', {image, newName});
        }
    }
}
</script>

<style scoped>
.tile-of-images{
    padding: 0 10px;
}
</style>
