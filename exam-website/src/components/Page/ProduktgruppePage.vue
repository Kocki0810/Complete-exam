<template>
    <div>
        <Teleport to="body">
        <modalPgDelete :p_show="showModal" @close="showModal = false">
        <template #header>
            <h3>Der er {{modalProduktAntal}} produkter i denne gruppe</h3>
        </template>
        <template #select>
            <select @change="test">
                <option disabled selected>Flyt Produktgruppe</option>
                <option v-for="(produktgruppe, index) in produktgrupper?.data" v-bind:key="produktgruppe?.id"
                :value="produktgruppe?.id + ',' + index"
                @input="m_Produktpris"
                type="navn"
                placeholder="Pris">{{produktgruppe.navn}}
                </option>
            </select>
        </template>
        </modalPgDelete>
        </Teleport>
        <div>
            <button @click="getProdukter">Get produkter</button>
            <button @click="SubmitProduktForm">submit test</button>
        </div>
        <div>
            <form @submit.prevent v:model="ProduktForm">
                <div>
                    <input
                    :value="NewProduktgruppe.navn"
                    @input="event => NewProduktgruppe.navn = event.target.value"
                    type="navn"
                    placeholder="Navn"/>
                    <button @click="OpretProduktgruppe">Opret Produktgruppe</button>
                </div>
                <div>
                    <input
                    :value="NewProdukt.navn"
                    @input="event => NewProdukt.navn = event.target.value"
                    type="navn"
                    placeholder="Navn"
                    />
                    <input
                    :value="NewProdukt.pris"
                    @input="event => NewProdukt.pris = event.target.value"
                    type="navn"
                    placeholder="Pris"
                    />
                    <select @change="event => NewProdukt.produktgruppe_id = event.target.value">
                        <option disabled selected>Vælg Produktgruppe</option>
                        <option v-for="(produktgruppe, index) in produktgrupper?.data" v-bind:key="produktgruppe?.id"
                        :value="produktgruppe?.id + ',' + index"
                        @input="m_Produktpris"
                        type="navn"
                        placeholder="Pris">{{produktgruppe.navn}}
                        </option>
                    </select>
                    <button @click="OpretProdukt">Opret Produkt</button>
                </div>
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
        firma_id: Number
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
            modalProduktAntal: 0,
            modalFlytProdukterTil: -1,
        }
    },
    methods: {
        test()
        {
            console.log(event.target.selectedIndex)
        },
        getProdukter: function() {
            axios
            .get('http://localhost:8000/api/v1/produktgruppe?id[gt]=0&includeprodukt=1', controller.signal)
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
                        var produktgruppeID = produktValue.produktgruppe_id.toString().split(",");
                        if(produktValue.produktgruppe_id.toString().includes(","))
                        {
                            var produktToChange = {
                                fromGruppeIndex: key,
                                ToGruppeIndex: produktgruppeID[1],
                                produkt: produktValue
                            } 
                            ProduktgruppeChanges.push(produktToChange);
                        }
                        
                        produktgruppeID = produktValue.produktgruppe_id[0];
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
            if(ProduktValuesToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'http://localhost:8000/api/v1/produkt/update',
                    headers: {}, 
                    data: ProduktValuesToSubmit
                });
            }
            if(ProduktGruppeValuesToSubmit.length > 0)
            {
                axios({
                    method: 'PATCH',
                    url: 'http://localhost:8000/api/v1/produktgruppe/update',
                    headers: {}, 
                    data: ProduktGruppeValuesToSubmit
                });
            }
        },
        Check_Deleteproduktgruppe(produktgruppeID, gruppeIndex)
        {
            var copy = this.produktgrupper;
            if(copy.data[gruppeIndex].produkt.length != 0)
            {
                this.modalProduktAntal = copy.data[gruppeIndex].produkt.length;
                this.showModal = true;
            }
        },
        DelteProduktgruppe(produktgruppeID, gruppeIndex){
            var copy = this.produktgrupper;
            if(copy.data[gruppeIndex].produkt.length != 0)
            {
                this.modalProduktAntal = copy.data[gruppeIndex].produkt.length;
                this.showModal = true;
            }
            // toRaw(copy.data.splice(gruppeIndex, 1));
            
            // axios
            // .delete('http://localhost:8000/api/v1/produktgruppe/' + produktgruppeID)
        },
        DelteProdukt(produktID, index, gruppeIndex){
            var copy = this.produktgrupper;
            toRaw(copy.data[gruppeIndex].produkt.splice(index, 1));
            
            axios
            .delete('http://localhost:8000/api/v1/produkt/' + produktID)
        },
        OpretProdukt() {
            var gruppeIndex = this.NewProdukt.produktgruppe_id.split(',')[1];
            var produkt = {
                navn: this.NewProdukt.navn,
                pris: this.NewProdukt.pris,
                produktgruppe_id: this.NewProdukt.produktgruppe_id.split(',')[0],
                firma_id: this.firma_id
            }

            axios.post('http://localhost:8000/api/v1/produkt', {
                0 :produkt 
            })
            .then((response) => {
                produkt.id = response.data[0];
                this.produktgrupper.data[gruppeIndex].produkt.push(produkt);
            })
            this.NewProdukt.navn = "";
            this.NewProdukt.pris = "";
            this.NewProdukt.produktgruppe_id = -1;
        },
        OpretProduktgruppe() {
            var produktgruppe = {
                navn: this.NewProduktgruppe.navn,
                firma_id: this.firma_id
            }

            axios.post('http://localhost:8000/api/v1/produktgruppe', {
                0 :produktgruppe
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