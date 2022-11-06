<template>
    <div class="form-group row m-2">
        <div class="row">
            <span class="col-sm m-2">Du er ikke logget ind, log ind herunder</span>
        </div>
        <div class="row">
            <span class="col-sm m-2">Hvis du er en ny bruger, skal du logge ind med den information du har fået fra din salgsrepresentant</span>
        </div>
        <div class="row">
            <input
            :value="this.email"
            @input="event => this.email = event.target.value"
            type="navn"
            placeholder="Email"
            class="form-control col-sm m-2"
            />
        </div>
        <div class="row">
            <input
            :value="this.password"
            @input="event => this.password = event.target.value"
            type="Password"
            placeholder="Password"
            class="form-control col-sm m-2"
            />
        </div>
        <button type="button" class="btn btn-primary col-md-auto m-2" @click="attemptLogin">Login</button>
        <div>
            <span class="col-sm m-2">{{this.message}}</span>
        </div>
    </div>
</template>
<script>

import axios from 'axios'
import { toRaw } from 'vue';
export default {
    data() {
        return {
            email: "",
            password: "",
            loginResult : "-1",
            message: ""
        }
    },
    methods: {
        attemptLogin () {
            if(this.email == "" || this.password == "")
            {
                this.message = "Alle felter skal udfyldes"
                return;
            }

            this.message = "Logger ind..."
            axios.get('http://localhost:8000/api/v1/bruger?email[eq]='+this.email+'&password[eq]='+this.password)
            .then((response) => {
                this.loginResult = response?.data;
                if(this.loginResult.data.length == 0)
                {
                    this.message = "Ugyldig email eller password"
                }
                else
                {
                    this.$emit("login", toRaw(this.loginResult.data[0]));
                }

            });

        },
    }
}


</script>