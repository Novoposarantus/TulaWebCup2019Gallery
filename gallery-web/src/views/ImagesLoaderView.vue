<template>
    <div>
        <div class="image-loader-header">
            <input 
                class="file-input"
                @change="selectImage"
                ref="fileInput"
                type="file" 
                multiple
                accept="image/x-png,image/jpeg" />
            <v-btn
                @click="chooseImage">
                <v-icon>far fa-file-image</v-icon>
                <span class="hidden-xs-only button-text">Выбрать изображения</span>
            </v-btn>
            <v-btn 
                color="success"
                v-if="images.length > 0"
                @click="saveImages">
                <v-icon>fas fa-cloud-upload-alt</v-icon>
                <span class="hidden-xs-only button-text">Загрузить</span>
            </v-btn>
            <div
                :class="`${infoClass} hidden-xs-only`"
                v-if="showInfo">
                {{info}}
            </div>
        </div>
        <div
            :class="`${infoClass} hidden-sm-and-up text-sm`"
            v-if="showInfo">
            {{info}}
        </div>
        <tile-of-images
            @nameChange="nameChange($event)"
            :images="images"
        ></tile-of-images>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex';
import TileOfImages from '@/components/common/LoaderTileOfImages';
import {galleryGlobalActions, galleryGlobalGetters} from '@/support';

export default {
    components:{
        TileOfImages
    },
    data(){
        return{
            images: [],
            info: null,
            infoClass: null
        }
    },
    computed:{
        ...mapGetters({
            saveError: galleryGlobalGetters.error
        }),
        showInfo(){
            return this.info && this.images && this.images.length > 0;
        }
    },
    methods:{
        ...mapActions({
            sendImages: galleryGlobalActions.saveImages,
        }),
        selectImage(event){
            for(let image of Array.from(event.target.files)){
                this._encodeImageFileAsURL(image);
            }
        },
        async saveImages(){
            this.resetInfo();
            await this.sendImages(this.images);
            if(this.saveError){
                this.info = this.saveError;
                this.infoClass = "text-error";
                return;
            }
            this.info = "Загрузка завершена";
            this.infoClass = "text-success";
            this.images = [];
        },
        nameChange({image, newName}){
            this.resetInfo();
            var index = this.images.indexOf(image);
            this.images.splice(index, 1, {
                ...image,
                name: newName
            });
        },
        chooseImage(){
            this.resetInfo();
            this.$refs.fileInput.click();
        },
        _encodeImageFileAsURL(file) {
            var reader = new FileReader();
            reader.onloadend = () => {
                this.images.push({
                    name: '',
                    imageContent : reader.result.toString()
                });
            }
            reader.readAsDataURL(file);
        },
        resetInfo(){
            this.info = null;
            this.infoClass = null;
        }
    },

}
</script>

<style scoped>
.image-loader-header{
    display: flex;
    box-sizing: border-box;
    padding: 20px; 
}
.button-text{
    margin-left: 10px;
}
.file-input{
    display: none;
}
.text-error,
.text-success{
    font-size: 20px;
    font-weight: 500;
    letter-spacing: 0.02em;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    display: flex;
    align-items: center;
}
.text-error{
    color: rgb(252, 72, 72);
}
.text-success{
    color: rgb(65, 214, 65);
}
.text-sm{
    margin: 0 20px 20px 26px;
}
</style>
