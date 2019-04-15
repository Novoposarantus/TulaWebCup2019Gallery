<template>
    <div>
        <tile-of-images
            :images="images"
        ></tile-of-images>
        <image-modal></image-modal>
    </div>
</template>

<script>
import {mapGetters} from 'vuex';
import TileOfImages from '@/components/common/GalleryTileOfImages';
import ImageModal from '@/components/common/ImageModal';
import {galleryGlobalGetters} from "@/support";

export default {
    components:{
        TileOfImages,
        ImageModal
    },
    computed:{
        ...mapGetters({
            images : galleryGlobalGetters.images
        })
    }
}
</script>

<style scoped>

</style>
