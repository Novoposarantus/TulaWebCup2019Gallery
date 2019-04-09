<template>
    <div>
        <div class="box">
        <form @submit.prevent="submit" class="form">
            <div class="error font-size-18 margin-bottom-5 text-center" v-if="error">@error</div>
            <div class="form-item">
                <div>{{error.userName}}</div>
                <label for="login" class="form-label">Логин</label>
                <input id="login" v-model="form.userName" class="form-input-text" type="text">
            </div>
            <div class="form-item">
                <div>{{error.password}}</div>
                <label for="password" class="form-label">Пароль</label>
                <input if="password" v-model="form.password" class="form-input-text" type="password">
            </div>
            <div class="form-item">
                <div>{{error.passwordConfirm}}</div>
                <label for="passwordConfirm" class="form-label">Подтвердите пароль</label>
                <input id="passwordConfirm" v-model="form.passwordConfirm" class="form-input-text" type="password">
            </div>
            <div class="btn-box">
                <button class="btn">Войти</button>
            </div>
            <div class="link-box">
                <a @click="toLogin">Авторизация</a>
            </div>
        </form>
        </div>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex';
import {routeNames} from '../../support';

export default {
    data(){
        return{
            form:{
                userName: '',
                password: '',
                passwordConfirm: '',
            },
            error:{
                userName: '',
                password: '',
                passwordConfirm: ''
            }
        }
    },
    computed:{
        ...mapGetters({
            error: 'auth/error',
        })
    },
    methods:{
        ...mapActions({
            register : 'auth/register'
        }),
        async submit(){
            if(!this.validate()) return;
            await this.register({...this.form});
            if(!this.error){
                this.toLogin();
            }
        },
        toLogin(){
            this.$router.push({name : routeNames.Login});
        },
        validate(){
            let isEmpty = false;
            for(let key in this.form){
                if(this.form[key].length > 0){
                    continue;
                }
                this.$set(this.error, key, "Поле не может быть пустым");
                isEmpty = true;
            }
            if(isEmpty){
                return false;
            }
            if(this.form.password != this.form.passwordConfirm){
                this.$set(this.error, "password", "Пароли не совпадают");
                this.$set(this.error, "passwordConfirm", "Пароли не совпадают");
                return false;
            }
            return true;
        }
    }
}
</script>

<style scoped>
</style>

