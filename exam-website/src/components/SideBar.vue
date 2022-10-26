<template>
    <div>
        <div>

            <li>
                <ul>Start</ul>
                <ul>Produktgrupper</ul>
                <ul>Ekspedienter</ul>
                <ul>Rapporter</ul>
                <ul>Start</ul>
                <ul>Start</ul>
            </li>
        </div>
        
        <!-- Produktgrupper -->
        <div>
            <form>
                <div>
                    <span>Navn</span>
                    <input
                    v-model="name"
                    type="navn"
                    placeholder="Navn"
                    />
                </div>
                <div>
                    <span>Navn</span>
                    <input
                    v-model="name"
                    type="navn"
                    placeholder="Navn"
                    />
                </div>
            </form>
            <button @click="getProdukter()"></button>
        </div>
    </div>
</template>


<script>

import axios from 'axios'
export default {
    data() {
        return {
            name: "Navn",
            response: ""
        }
    },
    methods: {
        getProdukter: function() {
            axios
            .get('http://localhost:8000/api/v1/bruger?id[gt]=0')
            .then(response => (this.info = response.data.bpi))
        }
    }
}

</script>