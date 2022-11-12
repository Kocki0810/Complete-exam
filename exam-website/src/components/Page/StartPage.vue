<template>
    <div>
        <div class="row">
            <p class="col-lg-12  d-flex justify-content-center">Ekspedient navn: {{bruger.navn}}</p>
        </div>
        <div class="row">
            <p class="col-lg-12 d-flex justify-content-center">Email: {{bruger.email}}</p>
        </div>
    </div>
    <div>
        <div col="row">
            <div class="col-lg-12 d-flex justify-content-center">
                <SalgProdukter v-if="this.Produkterloaded" :ordre="ordre" :linjer="linjer"></SalgProdukter>
                <SalgEkspedient v-if="this.EkspedienterLoaded" :Ekspedienter="Ekspedienter" :ordre="ordre"></SalgEkspedient>
            </div>
        </div>
    </div>
</template>

<script>

import axios from 'axios'
import SalgProdukter from '../Rapporter/SalgProdukter.vue'
import SalgEkspedient from '../Rapporter/SalgEkspedient.vue'
import { toRaw } from 'vue';
export default {
    created() {
        this.getSalgsData();
        this.getEkspedienter();
    },
    props: {
        bruger: Array,
    },
    data() {
    return {
        Produkterloaded: false,
        EkspedienterLoaded: false,
        ordre: [],
        linjer: [],
        Ekspedienter: []
        }
    },
    methods: {
        getSalgsData()
        {
            axios
            .get('https://examwebsite.azurewebsites.net/api/v1/ordre?firma_id[eq]='+this.bruger.firma_id+'&includelinje=1')
            .then(response => {
                this.ordre = response?.data
                this.ordre.data.forEach(element => {
                    element.linje.forEach(linje => (this.linjer.push(linje)))
                    this.Produkterloaded = true;
                });
            });
        },
        getEkspedienter: function() {
            axios
            .get('https://examwebsite.azurewebsites.net/api/v1/ekspedient?firma_id[eq]='+this.bruger.firma_id)
            .then(response => {
                this.Ekspedienter = response?.data
                this.EkspedienterLoaded = true;
            });
        },
    },
    components: {
        SalgProdukter,
        SalgEkspedient,
    }
}
</script>