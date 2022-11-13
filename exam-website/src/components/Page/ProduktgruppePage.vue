<template>
    <div :key="this.reRender">
        <Teleport to="body">
        <modalPgDelete @click="ModalTjek" :p_show="showModal" @Flyt-Produkter="FlytProdukter" @Delete-Produktgruppe="DeleteProduktgruppe" @close="showModal = false">
        <template #header>
            <h3>Der er {{modalProduktAntal}} produkter i denne gruppe</h3>
        </template>
        <template #select>
            <select class="form-control col-sm" @change="modalProduktgruppeValues">
                <option disabled selected>Flyt Produktgruppe</option>
                <option v-for="(produktgruppe) in produktgrupper?.data" v-bind:key="produktgruppe?.id"
                :value="produktgruppe?.id"
                type="navn"
                placeholder="Pris">{{produktgruppe.navn}}
                </option>
            </select>
        </template>
        </modalPgDelete>
        </Teleport>
        <div class="sticky-top">
            <button type="button" class="btn btn-primary col-md-auto m-2" @click="SubmitProduktForm">Gem</button>
        </div>
        <div>
            <form class="form-group" @submit.prevent v:model="ProduktForm">
                <div class="form-group row m-2">
                    <input
                    :value="NewProduktgruppe.navn"
                    @input="event => NewProduktgruppe.navn = event.target.value"
                    type="navn"
                    placeholder="Navn"
                    class="form-control col-sm"
                    />
                    <button type="button" class="btn btn-primary col-md-auto m-2" @click="OpretProduktgruppe">Opret Produktgruppe</button>
                </div>
                <div class="form-group row m-2">
                    <input
                    :value="NewProdukt.navn"
                    @input="event => NewProdukt.navn = event.target.value"
                    type="navn"
                    placeholder="Navn"
                    class="form-control col-sm"
                    />
                    <input
                    :value="NewProdukt.pris"
                    @input="event => NewProdukt.pris = event.target.value"
                    type="navn"
                    placeholder="Pris"
                    class="form-control col-sm"
                    />
                    <select @change="newProduktValues" class="form-control col-sm">
                        <option disabled selected>Vælg Produktgruppe</option>
                        <option v-for="(produktgruppe) in produktgrupper?.data" v-bind:key="produktgruppe?.id"
                        :value="produktgruppe?.id"
                        type="navn"
                        placeholder="Pris">{{produktgruppe.navn}}
                    </option>
                    </select>
                    <button type="button" class="btn btn-primary col-md-auto m-2" @click="OpretProdukt">Opret Produkt</button>
                </div>
                <br>
                <div v-for="(produktgruppe, gruppeIndex) in produktgrupper.data" v-bind:key="produktgruppe.id">
                    <ProduktgruppeItem :p_Navn="produktgruppe.navn" :p_index="gruppeIndex" :p_produktgruppeID="produktgruppe.id" @delete-produktgruppe="Check_Deleteproduktgruppe" @produktgruppe-navn="ChangeInputProduktgruppe"></ProduktgruppeItem>
                    <div v-for="(produkt, index) in produktgruppe.produkt" v-bind:key="produkt.id">
                        <ProduktItem :p_Produktgruppe_id="produktgruppe.id" :p_GruppeIndex="gruppeIndex" :p_Index="index" :p_ProduktID="produkt.id" :p_Navn="produkt.navn" :p_Produktgruppe="produktgrupper.data" :p_Pris="produkt.pris" 
                        @delete-produkt="DelteProdukt"
                        @produkt-navn="ChangeInputProdukt"></ProduktItem>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import ProduktItem from '../Item/ProduktItem.vue';
import ProduktgruppeItem from '../Item/ProduktgruppeItem.vue';
import modalPgDelete from '../Util/ModalProduktgruppe.vue';


import axios from 'axios'
import { toRaw } from 'vue';
const controller = new AbortController();
export default {
    created() {
        this.getProdukter();
    },
    unmounted() {
        controller.abort();
    },
    props: {
        firma_id: Number,
        bearerToken: String
    },
    components: {
        ProduktItem,
        ProduktgruppeItem,
        modalPgDelete
    },
    data() {
        return {
            produktgrupper: [],
            NewProdukt: {
                navn: "", 
                pris: "",
                produktgruppe_id: 0
            },
            NewProduktgruppe: {
                navn: "", 
            },
            showModal: false,
            ModalValues: {
                ProduktAntal: 0,
                FlytProdukterTilIndex: -1,
                FlytProdukterID: -1,
                FlytFraIndeks: -1,
                FlytFraGruppeID: -1
            },
            reRender: 0
        }
    },
    methods: {
        ModalTjek(){
            if(event.target.classList.contains("modal-wrapper"))
            {
                this.showModal = false;
            }
        },
        modalProduktgruppeValues()
        {
            this.ModalValues.FlytProdukterTilIndex = (event.target.selectedIndex-1);
            this.ModalValues.FlytProdukterID = event.target.value;
        },
        newProduktValues()
        {
            this.NewProdukt.produktgruppe_id = event.target.value
            this.NewProdukt.produktgruppe_flytTilIndex = (event.target.selectedIndex-1)
        },
        getProdukter: function() {
            axios
            .get('https://examwebsite.azurewebsites.net/api/v1/produktgruppe?firma_id[eq]='+this.firma_id+'&includeprodukt=1', controller.signal)
            .then(response => (this.produktgrupper = response?.data));
        },
        SubmitProduktForm: function(){
            var ProduktValuesToSubmit = [];
            var ProduktGruppeValuesToSubmit = [];
            var ProduktgruppeChanges = [];
            var data = toRaw(this.produktgrupper.data);
            data.map(function(value, key)
            {
                if(value.Changed == true)
                {
                    var temp = {
                        id: value.id,
                        navn: value.navn
                    }
                    ProduktGruppeValuesToSubmit.push(temp);
                }
                data[key].produkt.map(function(produktValue, produktKey)
                {
                    if(produktValue.Changed)
                    {
                        var produktgruppeID = produktValue.produktgruppe_id;
                        if(produktValue.produktgruppe_flytTilIndex != undefined)
                        {
                            var produktToChange = {
                                fromGruppeIndex: key,
                                ToGruppeIndex: produktValue.produktgruppe_flytTilIndex,
                                produkt: produktValue
                            } 
                            ProduktgruppeChanges.push(produktToChange);
                        }
                        
                        var temp = {
                            id: produktValue.id,
                            produktgruppe_id: produktgruppeID,
                            navn: produktValue.navn,
                            pris: produktValue.pris,
                        }
                        ProduktValuesToSubmit.push(temp);
                    }
                })
            })
            if(ProduktgruppeChanges.length != 0)
            {
                ProduktgruppeChanges.forEach(element => {
                    this.produktgrupper.data[element.ToGruppeIndex].produkt.push(element.produkt);
                    this.produktgrupper.data[element.fromGruppeIndex].produkt.splice(element.produkt.p_Index, 1);
                    
                });
            }
            this.reRender++;
            if(ProduktValuesToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/produkt/update',
                    headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                    data: ProduktValuesToSubmit
                });
               
            }
            if(ProduktGruppeValuesToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/produktgruppe/update',
                    headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                    data: ProduktGruppeValuesToSubmit
                });
            }
        },
        Check_Deleteproduktgruppe(produktgruppeID, gruppeIndex)
        {
            var copy = this.produktgrupper;
            this.ModalValues.ProduktAntal = copy.data[gruppeIndex].produkt.length;
            this.ModalValues.FlytFraIndeks = gruppeIndex;
            this.ModalValues.FlytFraGruppeID = produktgruppeID
            if(copy.data[gruppeIndex].produkt.length != 0)
            {
                this.showModal = true;
            }
            else
            {
                this.DeleteProduktgruppe(false);
            }
        },
        DeleteProduktgruppe(DeleteProdukter = true){
            var copy = this.produktgrupper;
            var Index = this.ModalValues.FlytFraIndeks;
            var produkter = toRaw(copy.data[Index].produkt)
            toRaw(copy.data.splice(Index, 1));
            var pgID = [{id:this.ModalValues.FlytFraGruppeID}];
            
            if(DeleteProdukter)
            {
                var toDelete = [];
                
                produkter.forEach(element => {
                    var temp = {
                        id: element.id
                    }
                    toDelete.push(temp);
                })

                axios({
                    method: 'DELETE',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/produkt/delete',
                    headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                    data: toDelete
                });
            }
            
            axios({
                    method: 'DELETE',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/produktgruppe/delete',
                    headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                    data: pgID
            });
            this.showModal = false;
        },
        FlytProdukter()
        {
            var FlytProdukter = [];
            var produktgruppe = this.produktgrupper.data;
            var produkter = produktgruppe[this.ModalValues.FlytFraIndeks].produkt;
            var ProduktgruppeID = this.ModalValues.FlytFraGruppeID;
            produkter.forEach(element => {
                
                element.produktgruppeID = this.ModalValues.FlytProdukterID
                var moveProdukt = {
                    id: element.id,
                    produktgruppe_id: element.produktgruppeID
                }
                this.produktgrupper.data[this.ModalValues.FlytProdukterTilIndex].produkt.push(element)
                FlytProdukter.push(moveProdukt);
            });
            axios({
                    method: 'PATCH',
                    url: 'https://examwebsite.azurewebsites.net/api/v1/produkt/update',
                    headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                    data: FlytProdukter
            });
            this.showModal = false;
            this.DeleteProduktgruppe(false);
        },
        DelteProdukt(produktID, index, gruppeIndex){
            var copy = this.produktgrupper;
            toRaw(copy.data[gruppeIndex].produkt.splice(index, 1));

            axios({
                method: 'DELETE',
                url: 'https://examwebsite.azurewebsites.net/api/v1/produkt/delete',
                headers: {Authorization: 'Bearer ' + this.bearerToken}, 
                data: [{id:produktID}]
            });
        },
        OpretProdukt() {
            var gruppeIndex = this.NewProdukt.produktgruppe_flytTilIndex;
            var produkt = {
                navn: this.NewProdukt.navn,
                pris: this.NewProdukt.pris,
                produktgruppe_id: this.NewProdukt.produktgruppe_id,
                firma_id: this.firma_id
            }

            axios.post('https://examwebsite.azurewebsites.net/api/v1/produkt', {
                0 :produkt 
            }, 
            {
                headers: {Authorization: 'Bearer ' + this.bearerToken}
            })
            .then((response) => {
                produkt.id = response.data[0];
                if(this.produktgrupper.data[gruppeIndex].produkt == undefined)
                {
                    this.produktgrupper.data[gruppeIndex].produkt = [produkt];
                }
                this.produktgrupper.data[gruppeIndex].produkt.push(produkt);
            })
            this.NewProdukt.navn = "";
            this.NewProdukt.pris = "";
        },
        OpretProduktgruppe() {
            var produktgruppe = {
                navn: this.NewProduktgruppe.navn,
                firma_id: this.firma_id
            }

            axios.post('https://examwebsite.azurewebsites.net/api/v1/produktgruppe', {
                0 :produktgruppe
            },
            {
                headers: {Authorization: 'Bearer ' + this.bearerToken}
            })
            .then((response) => {
                produktgruppe.id = response.data[0];
                this.produktgrupper.data.push(produktgruppe);
            })
            this.NewProduktgruppe.navn = "";
        },
        ChangeInputProdukt(index, gruppeIndex, value){
            var produkt = toRaw(this.produktgrupper.data[gruppeIndex].produkt[index]);
            produkt.Changed = true;
            if(value == "navn")
            {
                produkt.navn = event.target.value;
            }
            else if(value == "pris")
            {
                produkt.pris = event.target.value;
            }
            else if(value == "gruppe")
            {
                produkt.produktgruppe_id = event.target.value;
                produkt.produktgruppe_flytTilIndex = (event.target.selectedIndex-1);
            }
        },
        ChangeInputProduktgruppe(gruppeIndex){
            var copy = toRaw(this.produktgrupper.data[gruppeIndex]);
            copy.Changed = true;
            copy.navn = event.target.value;
        }
    }
}

</script>