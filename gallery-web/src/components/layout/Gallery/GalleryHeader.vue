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
            ]
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
            set(value){
                this.setReverseSort(value);
            }
        }
    },
    methods:{
        ...mapActions({
            setSort: galleryGlobalActions.setSortBy,
            setReverseSort: galleryGlobalActions.setReverseSort
        }),
        loadImage(){
            this.$router.push({name: routeNames.LoadImages});
        },
        changeSortBy(sortId){
            this.setSort(sortId);
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
    display: inline-block;
    max-width: 150px;
    margin: 5px 20px;
}
</style>
