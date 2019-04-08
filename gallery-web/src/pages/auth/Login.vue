<template>
    <div>
        <div class="box">
            <form @submit.prevent="" class="form">
                <div class="form-item">
                    <label class="form-label">Логин</label>
                    <input class="form-input-text" type="text">
                </div>
                <div class="form-item">
                    <label class="form-label">Пароль</label>
                    <input class="form-input-text" type="password">
                </div>
                <div class="btn-box">
                    <button class="btn">Войти</button>
                </div>
                <div class="link-box">
                    <a @click="toRegistration">Зарегестрироваться</a>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    methods:{
        toRegistration(){
            this.$router.push("/registration");
        }
    }
}
</script>

<style scoped>
.box{
    display: flex;
}
.form{
    margin: 20px auto;
}
.link-box{
    margin-top: 10px;
    text-align: center;
}
</style>

