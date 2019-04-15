<template>
    <div>
        <tile-of-images
            @openImage="openImage($event)"
            :images="images"
        ></tile-of-images>
        <image-modal
          @closeDialog="closeDialog"
          :image="selectedImage"></image-modal>
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
    data(){
      return{
        selectedImage: null
      }
    },
    computed:{
        ...mapGetters({
            images : galleryGlobalGetters.images
        })
    },
    methods:{
      closeDialog(){
        this.selectedImage = null;
      },
      openImage(image){
          this.selectedImage = {
              ...image
          }
      }
    }
}
</script>

<style scoped>

</style>
