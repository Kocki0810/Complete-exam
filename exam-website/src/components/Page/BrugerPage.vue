<template :key="this.reload">
    <div>
        <div class="sticky-top">
            <button type="button" class="btn btn-primary col-md-auto m-2" @click="SubmitEkspedientForm">Gem</button>
        </div>
        <div>
            <form class="form-group" @submit.prevent v:model="BrugerForm">
                <div class="form-group row m-2">
                    <input
                    :value="NewEkspedient.navn"
                    @input="event => NewEkspedient.navn = event.target.value"
                    type="navn"
                    placeholder="Navn"
                    class="form-control col-sm"
                    />
                    <input
                    :value="NewEkspedient.kortnummer"
                    @input="event => NewEkspedient.kortnummer = event.target.value"
                    placeholder="Kortnummer"
                    class="form-control col-sm"
                    />
                    <button type="button" class="btn btn-primary col-md-auto m-2" @click="OpretEkspedient">Opret Ekspedient</button>
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
export default {
    created() {
        this.getEkspedienter();
    },
    components: {
        BrugerItem
    },
    props: {
        firma_id: Number,
        bearerToken: String
    },
    data() {
        return {
            name: "Navn",
            Ekspedienter: [],
            NewEkspedient: {
                navn: "", 
                kortnummer: "",
            },
            reload: 0
        }
    },
    methods: {
        getEkspedienter: function() {
            axios
            .get('https://examwebsite.azurewebsites.net/api/v1/ekspedient?firma_id[eq]='+this.firma_id+'&includebruger=1')
            .then(response => (this.Ekspedienter = response?.data));
        },
        SubmitEkspedientForm: function(){
            var EkspedientToSubmit = [];
            var BrugerToSubmit = [];
            var BrugerToCreate = {
                brugere: [],
                indexer: []
            };
            var data = toRaw(this.Ekspedienter.data);
            var firmaid = this.firma_id
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
                        navn: value.navn,
                        password: value.bruger.password,
                        telefon: value.bruger.telefon,
                        firma_id: firmaid,
                        ekspedient_id: value.id
                    }
                    if(value.bruger?.newBruger)
                    {
                        BrugerToCreate.brugere.push(bruger);
                        BrugerToCreate.indexer.push(key);
                    }
                    else
                    {
                        BrugerToSubmit.push(bruger);
                    }
                }
            })
            if(EkspedientToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/ekspedient/update',
                    headers: {Authorization: 'Bearer '+this.bearerToken}, 
                    data: EkspedientToSubmit
                });
            }
            if(BrugerToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/bruger/update',
                    headers: {Authorization: 'Bearer '+this.bearerToken}, 
                    data: BrugerToSubmit
                });
            }
            if(BrugerToCreate.brugere.length > 0)
            {
                var brugerID = [];
                axios.post('https://examwebsite.azurewebsites.net/api/v1/bruger', {
                    data: BrugerToCreate.brugere,
                },
                {
                    headers: {Authorization: 'Bearer ' + this.bearerToken}
                })
                .then(response => {
                    brugerID = response?.data
                    brugerID.map((value, key) => {
                        this.Ekspedienter.data[BrugerToCreate.indexer[key]].bruger.id = value;
                    })
                });
            }
            this.reload++;
        },
        DeletBruger(index){
            var brugerID = this.Ekspedienter.data[index].bruger.id;
            this.Ekspedienter.data[index].bruger.email = "";
            this.Ekspedienter.data[index].bruger.password = "";
            this.Ekspedienter.data[index].bruger.telefon = "";
            this.Ekspedienter.data[index].bruger.id = null;
            this.Ekspedienter.data[index].bruger = null;

            axios({
                method: 'DELETE',
                url: 'https://examwebsite.azurewebsites.net/api/v1/bruger/delete',
                headers: {Authorization: 'Bearer '+this.bearerToken}, 
                data: [{id: brugerID}]
            });
            this.reload++;
        },
        DeletEkspedient(ekspedientID, index){
            this.Ekspedienter.data.splice(index, 1);

            axios({
                method: 'DELETE',
                url: 'https://examwebsite.azurewebsites.net/api/v1/ekspedient/delete',
                headers: {Authorization: 'Bearer '+this.bearerToken}, 
                data: [{id: ekspedientID}]
            });
            this.reload++;
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
                if(copy.bruger == null)
                {
                    copy.bruger = {newBruger: true, ekspedientID: copy.id}
                }
                
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
        },
        OpretEkspedient() {
            var ekspedient = {
                navn: this.NewEkspedient.navn,
                kortnummer: this.NewEkspedient.kortnummer,
                firma_id: this.firma_id
            }

            axios.post('https://examwebsite.azurewebsites.net/api/v1/ekspedient', {
                0 :ekspedient
            },
            {
                headers: {Authorization: 'Bearer ' + this.bearerToken}
            })
            .then((response) => {
                ekspedient.id = response.data[0];
                this.Ekspedienter.data.push(ekspedient);
            })
            this.NewEkspedient.navn = "";
            this.NewEkspedient.kortnummer = "";
            this.reload++;
        }
    }
}

</script>