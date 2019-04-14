<template>
    <div class="image-container">
        <v-card
            class="image-container-card">
            <v-img
            :src="'data:image/jpeg;base64,'+ image.image"
            height="200"
            contain
            ></v-img>
            <v-card-title class="title">{{image.name}}</v-card-title>
        </v-card>
    </div>
</template>

<script>
export default {
    props:{
        image: Object
    }
}
</script>

<style scoped>
.image-container{
    width: 200px;
    cursor: pointer;
}
.image-container-card:hover{
    background-color: rgb(240, 240, 240);
}
</style>
