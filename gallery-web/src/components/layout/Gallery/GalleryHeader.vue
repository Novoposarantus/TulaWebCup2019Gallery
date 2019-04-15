<template>
    <div>
        <div class="galery-header">
            <v-btn
                v-if="isAuthenticated"
                @click="loadImage">
                <v-icon>fas fa-file-image</v-icon>
                <span class="hidden-xs-only button-text">Загрузить изображения</span>
            </v-btn>
        </div>
    </div>
</template>

<script>
import {mapGetters} from 'vuex';
import {routeNames, authGlobalGetters} from '@/support';

export default {
    methods:{
        loadImage(){
            this.$router.push({name: routeNames.LoadImages});
        }
    },
    computed:{
        ...mapGetters({
            isAuthenticated: authGlobalGetters.isAuthenticated
        })
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
