<template>
    <div class="pagination-container">
      <v-pagination
        v-if="showPagination"
        v-model="pageNumber"
        :length="paginationLength"
        :total-visible="7"
      ></v-pagination>
    </div>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
    galleryGlobalGetters, 
    galleryGlobalActions,
    routeNames
    } from '@/support';
export default {
    computed:{
        ...mapGetters({
            images : galleryGlobalGetters.images,
            filter : galleryGlobalGetters.filter,
            imagesCount : galleryGlobalGetters.imagesCount
        }),
        paginationLength(){
            return Math.ceil(this.imagesCount / this.filter.imagesOnPageCount);
        },
        pageNumber: {
            get(){
                return this.filter.pageNumber;
            },
            set(value){
                this.setPageNumber(value);
                this.$router.push({
                    name: routeNames.Gallery,
                    params: {
                        pageNumber: value
                    }
                })
            }
        },
        showPagination(){
            return this.filter.imagesOnPageCount != null && this.paginationLength > 1;
        }
    },
    methods:{
        ...mapActions({
            setPageNumber: galleryGlobalActions.setPageNumber,
        }),
    }
}
</script>

<style scoped>
.pagination-container{
    display: flex;
    justify-content: center;
}
</style>
