<template>
    <div>
        <tile-of-images
            :images="images"
        ></tile-of-images>
    </div>
</template>

<script>
import {mapGetters} from 'vuex';
import TileOfImages from '@/components/common/GalleryTileOfImages';
import {galleryGlobalGetters} from "@/support";

export default {
    components:{
        TileOfImages
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
