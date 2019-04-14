y<template>
    <div>
        <div class="box">
            <v-form
                ref="form"
                class="form-center"
                v-model="valid"
                lazy-validation
            >   
                <v-layout>
                    <v-flex 
                        class="error-text"
                        text-xs-center
                        v-if="error"
                    >
                        {{error}}
                    </v-flex>
                </v-layout>
                <v-text-field
                    class="form-item"
                    v-model="form.userName"
                    :rules="[v => !!v || 'Поле не моет быть пустым']"
                    label="Логин"
                    required
                ></v-text-field>
            
                <v-text-field
                    class="form-item"
                    type="password"
                    v-model="form.password"
                    :rules="[v => !!v || 'Поле не моет быть пустым']"
                    label="Пароль"
                    required
                ></v-text-field>
                <div class="text-center">
                    <v-btn
                        :disabled="!valid"
                        color="success"
                        @click="submit"
                    >
                        Войти
                    </v-btn>
                </div>
            </v-form>
        </div>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex';
import {
    routeNames,
    authGlobalGetters,
    authGlobalActions,
} from '../../support';

export default {
    data(){
        return{
            valid: false,
            form:{
                userName: '',
                password: '',
            }
        }
    },
    computed:{
        ...mapGetters({
            error: authGlobalGetters.error,
        })
    },
    methods:{
        ...mapActions({
            authentication : authGlobalActions.authentication
        }),
        async submit () {
            if (!this.$refs.form.validate()) {
                return;
            }
            await this.authentication({...this.form});
            if(!this.error){
                this.$router.push({name: routeNames.Gallery});
            }
        },
    }
}
</script>

<style scoped>
.box{
    display: flex;
}
.form-center{
    margin: 0 auto;
}
.text-center{
    text-align: center;
}
.form-item{
    min-width: 250px;
}
.error-text{
    color:red;
}
</style>

