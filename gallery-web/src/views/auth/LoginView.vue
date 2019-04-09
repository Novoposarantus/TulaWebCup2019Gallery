y<template>
    <div>
        <div class="box">
            <form @submit.prevent="submit" class="form">
                <div class="error font-size-18 margin-bottom-5 text-center" v-if="error">@error</div>
                <div class="form-item">
                    <label for="userName" class="form-label">Логин</label>
                    <input id="userName" v-model="form.userName" class="form-input-text" type="text">
                </div>
                <div class="form-item">
                <div>{{errorForm.password}}</div>
                    <label for="password" class="form-label">Пароль</label>
                    <input id="password" v-model="form.password" class="form-input-text" type="password">
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
import {mapActions, mapGetters} from 'vuex';
import {routeNames} from '../../support';

export default {
    data(){
        return{
            form:{
                userName: '',
                password: '',
            },
            errorForm:{
                userName: '',
                password: ''
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
            authentication : 'auth/authentication'
        }),
        async submit(){
            if(!this.validate()) return;
            await this.authentication({...this.form});
            if(!this.error){
                this.$router.push({name: routeNames.Gallery});
            }
        },
        toRegistration(){
            this.$router.push({name : routeNames.Registration});
        },
        validate(){
            this.errorForm = {
                userName : "",
                password : ""
            }
            let isEmpty = false;
            for(let key in this.form){
                if(this.form[key].length > 0){
                    continue;
                }
                this.$set(this.errorForm, key, "Поле не может быть пустым");
                isEmpty = true;
            }
            return !isEmpty;
        }
    }
}
</script>

<style scoped>
</style>

