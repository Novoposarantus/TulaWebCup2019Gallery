<template>
    <div class="gallery-container">
        <div class="gallery-content">
            <gallery-header></gallery-header>
            <gallery-content></gallery-content>
        </div>
        <div class="gallery-footer">
            <gallery-footer></gallery-footer>
        </div>
    </div>
</template>

<script>
import GalleryContent from '@/components/layout/Gallery/GalleryContent';
import GalleryHeader from '@/components/layout/Gallery/GalleryHeader';
import GalleryFooter from '@/components/layout/Gallery/GalleryFooter';

export default {
    components:{
        GalleryContent,
        GalleryHeader,
        GalleryFooter
    }
}
</script>

<style scoped>
.gallery-container{
  display: flex;
  flex-direction: column;
  height: 100%;
  padding: 20px;
}
.gallery-content{
  flex: 1 0 auto;
}
.gallery-footer{
  flex: 0 0 auto;
}
</style>