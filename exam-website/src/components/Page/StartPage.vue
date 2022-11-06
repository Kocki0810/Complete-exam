<template>
    <div class="row">
        <p>Ekspedient navn: {{bruger.navn}}</p>
        <span>Email: {{bruger.email}}</span>
    </div>
    <div>
        <SalgProdukter v-if="this.Produkterloaded" :ordre="ordre" :linjer="linjer"></SalgProdukter>
    </div>
    <div>
        <SalgEkspedient v-if="this.EkspedienterLoaded" :Ekspedienter="Ekspedienter" :ordre="ordre"></SalgEkspedient>
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
            .get('http://localhost:8000/api/v1/ordre?firma_id[eq]='+this.bruger.firma_id+'&includelinje=1')
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
            .get('http://localhost:8000/api/v1/ekspedient?firma_id[eq]='+this.bruger.firma_id)
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