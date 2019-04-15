<template>
    <v-dialog
        v-model="showDialog"
        max-width="700"
      >
        <app-carusel
            :image="image"
        ></app-carusel>
        
      </v-dialog>
</template>

<script>
import AppCarusel from '@/components/common/AppCarusel';
export default {
    components:{
        AppCarusel
    },
    props:{
        image: Object
    },
    computed:{
        showDialog:{
            get(){
                return !!this.image;
            },
            set(value){
                if(value) return;
                this.$emit('closeDialog', value);
            }
        }
    }
}
</script>

<style scoped>

</style>
