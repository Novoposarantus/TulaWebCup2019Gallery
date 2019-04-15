<template>
    <v-dialog
        v-model="showDialog"
        max-width="700">
      <v-card v-if="!!image">
        <app-carusel></app-carusel>
        <v-card-title primary-title>
            <div>
              <h3 class="headline mb-0">{{image.name}}</h3>
            </div>
          </v-card-title>
        <div v-if="isAuthenticated">
            
        </div>
      </v-card>
      </v-dialog>
</template>

<script>
import {mapGetters, mapActions} from 'vuex';
import {
    authGlobalGetters, 
    caruselGlobalGetters,
    caruselGlobalActions
} from '@/support';
import AppCarusel from '@/components/common/AppCarusel';

export default {
    components:{
        AppCarusel
    },
    computed:{
        ...mapGetters({
            isAuthenticated:  authGlobalGetters.isAuthenticated,
            image: caruselGlobalGetters.image
        }),
        showDialog:{
            get(){
                return !!this.image;
            },
            set(value){
                if(value) return;
                this.$emit('closeDialog', value);
                this.setImage(null);
            }
        }

    },
    methods:{
        ...mapActions({
            setImage: caruselGlobalActions.setImage
        })
    }
}
</script>

<style scoped>

</style>
