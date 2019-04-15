<template>
    <div>
        <div class="galery-header">
            <v-btn
                @click="loadImage">
                <v-icon>fas fa-file-image</v-icon>
                <span class="hidden-xs-only button-text">Загрузить изображения</span>
            </v-btn>
        </div>
    </div>
</template>

<script>
import {routeNames} from '@/support';
export default {
    methods:{
        loadImage(){
            this.$router.push({name: routeNames.LoadImages});
        }
    }
}
</script>

<style scoped>
.galery-header{
    display: flex;
    box-sizing: border-box;
    padding: 20px; 
}
.button-text{
    margin-left: 10px;
}
</style>
