<template>
    <div class="image-container">
        <v-card
            class="image-container-card">
            <v-img
            :src="image.imageContent"
            height="270"
            contain
            ></v-img>
            <div class="image-name-container">
                <v-text-field
                    class="text-item"
                    @change="nameChange($event)"
                    :value="image.name"
                    label="Название"
                    required
                ></v-text-field>
            </div>
        </v-card>
    </div>
</template>

<script>
export default {
    props:{
        image: Object
    },
    methods:{
        nameChange(newName){
            this.$emit('nameChange',{
                image: this.image,
                newName
            })
        }
    }
}
</script>

<style scoped>
.image-container{
    display: inline-block;
    margin: 3px;
    min-width: 250px;
    max-width: 300px;
}
.image-name-container{
    display: flex;
    justify-content: center;
    box-sizing: border-box;
    padding: 0 5px;
}
.text-item{
    min-width: 270px;
}
</style>
