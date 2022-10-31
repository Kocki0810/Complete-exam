<template>
    <div>
        <div>
            <button @click="getEkspedienter">Get Ekspedienter</button>
            <button @click="SubmitEkspedientForm">submit test</button>
        </div>
        <div>
            <form @submit.prevent v:model="BrugerForm">
                <div>
                    <BrugerItem :p_Empty="1"></BrugerItem>
                </div>
                <br>
                <div v-for="(ekspedient, index) in Ekspedienter.data" v-bind:key="ekspedient.id">
                    <BrugerItem :p_Bruger="ekspedient.bruger" :p_Index="index" :p_Eks_Navn="ekspedient.navn" :p_Eks_Kortnummer="ekspedient.kortnummer" :p_Eks_ID="ekspedient.id"
                    @Delete-ekspedient="DeletEkspedient"
                    @Delete-bruger="DeletBruger"
                    @Ekspedient-Input="ChangeInputBruger"></BrugerItem>
                </div>
            </form>
        </div>
    </div>
</template>


<script>

import BrugerItem from '../Item/BrugerItem.vue';
import axios from 'axios'
import { toRaw } from 'vue';
const controller = new AbortController();
export default {
    created() {
        this.getEkspedienter();
    },
    unmounted() {
        controller.abort();
    },
    components: {
        BrugerItem
    },
    data() {
        return {
            name: "Navn",
            Ekspedienter: []
        }
    },
    methods: {
        getEkspedienter: function() {
            axios
            .get('http://localhost:8000/api/v1/ekspedient?id[gt]=0&includebruger=1', controller.signal)
            .then(response => (this.Ekspedienter = response?.data));
        },
        SubmitEkspedientForm: function(){
            var EkspedientToSubmit = [];
            var BrugerToSubmit = [];
            var data = toRaw(this.Ekspedienter.data);
            console.log(data)
            data.map(function(value, key)
            {
                if(value.Changed)
                {
                    var ekspedient = {
                        id: value.id,
                        navn: value.navn,
                        kortnummer: value.kortnummer
                    }
                    EkspedientToSubmit.push(ekspedient);
                }

                if(value.bruger?.Changed)
                {
                    var bruger = {
                        id: value.bruger.id,
                        email: value.bruger.email,
                        password: value.bruger.password,
                        telefon: value.bruger.telefon
                    }
                    BrugerToSubmit.push(bruger);
                }
            })
            if(EkspedientToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'http://localhost:8000/api/v1/ekspedient/update',
                    headers: {}, 
                    data: EkspedientToSubmit
                });
            }
            if(BrugerToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'http://localhost:8000/api/v1/bruger/update',
                    headers: {}, 
                    data: BrugerToSubmit
                });
            }
        },
        DeletBruger(brugerID, index){
            this.Ekspedienter.data[index].bruger.showBruger = 0;
            axios.delete('http://localhost:8000/api/v1/bruger/' + brugerID)
        },
        DeletEkspedient(ekspedientID, index){
            this.Ekspedienter.data.splice(index, 1);
            axios.delete('http://localhost:8000/api/v1/ekspedient/' + ekspedientID)
        },
        ChangeInputBruger(index, type, value){
            var copy = toRaw(this.Ekspedienter.data[index]);
            if(type == "eks")
            {
                copy.Changed = true;
                if(value == "navn")
                {
                    copy.navn = event.target.value;
                }
                else if(value == "kortnummer")
                {
                    copy.kortnummer = event.target.value;
                }
            }
            else if(type == "bruger")
            {
                copy.bruger.Changed = true;
                if(value == "telefon")
                {
                    copy.bruger.telefon = event.target.value;
                }
                else if(value == "email")
                {
                    copy.bruger.email = event.target.value;
                }
                else if(value == "password")
                {
                    copy.bruger.password = event.target.value;
                }
            }
        }
    }
}

</script>