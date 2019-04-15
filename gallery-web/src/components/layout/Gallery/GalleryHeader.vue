<template>
    <div>
        <div class="galery-header">
            <v-btn
                class="choose-image"
                v-if="isAuthenticated"
                @click="loadImage">
                <v-icon>fas fa-file-image</v-icon>
                <span class="hidden-xs-only button-text">Загрузить изображения</span>
            </v-btn>
            <v-select
                @change="changeSortBy"
                class="filter-select"
                :items="filterItems"
                label="Фильтрация"
            ></v-select>
            <v-switch
                class="choose-image"
                v-model="reverseSort"
                :label="filterReverseName"
            ></v-switch>
            <v-select
                @change="changeImagesOnPage"
                class="images-on-page-select"
                :items="imageOnPage"
                label="Картинок на странице"
            ></v-select>
        </div>
    </div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
    routeNames, 
    authGlobalGetters, 
    galleryGlobalGetters, 
    galleryGlobalActions, 
    sort
} from '@/support';

export default {
    data(){
        return {
            filterItems:[
                {
                    text: 'По дате',
                    value: sort.none
                },
                {
                    text: 'По имени',
                    value: sort.name
                },
                {
                    text: 'По рейтингу',
                    value: sort.rating
                }
            ],
            imageOnPage:[
                {
                    text: '10',
                    value: 10
                },
                {
                    text: '20',
                    value: 20
                },
                {
                    text: '50',
                    value: 50
                },
                {
                    text: 'Все',
                    value: null
                }
            ],
        }
    },
    computed:{
        ...mapGetters({
            isAuthenticated: authGlobalGetters.isAuthenticated,
            filter: galleryGlobalGetters.filter
        }),
        filterReverseName(){
            if(!this.filter.reverseSort){
                return "По убыванию";
            }
            return "По возрастанию"
        },
        reverseSort:{
            get(){
                return this.filter.reverseSort;
            },
            async set(value){
                await this.setReverseSort(value);
            }
        }
    },
    methods:{
        ...mapActions({
            setSort: galleryGlobalActions.setSortBy,
            setReverseSort: galleryGlobalActions.setReverseSort,
            setImagesOnPageCount: galleryGlobalActions.setImagesOnPageCount,
        }),
        loadImage(){
            this.$router.push({name: routeNames.LoadImages});
        },
        async changeSortBy(sortId){
            this.setSort(sortId);
        },
        async changeImagesOnPage(imagesOnPage){
            this.setImagesOnPageCount(imagesOnPage);
        }
    }
}
</script>

<style scoped>
.galery-header{
    box-sizing: border-box;
    padding: 20px; 
}
.button-text{
    margin-left: 10px;
}
.choose-image{
    display: inline-block;
    margin: 5px 20px;
}
.filter-select{
    max-width: 150px;
}
.images-on-page-select{
    max-width: 200px;
}
.filter-select,
.images-on-page-select{
    display: inline-block;
    margin: 5px 20px;
}
</style>
