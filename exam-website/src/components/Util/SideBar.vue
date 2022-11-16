<template>
    <div>
        <div>
            <p :class = "(MainPage == 'StartPage')?'btn-success':'btn-primary'" class="btn btn-primary rounded-0" @click="this.$emit('Update-Page', 'StartPage')">En action er kørt</p>
            <p :class = "(MainPage == 'ProduktgruppePage')?'btn-success':'btn-primary'" class="btn btn-primary rounded-0" v-if="this.firma_id != ''" @click="this.$emit('Update-Page', 'ProduktgruppePage')">Produktgrupper</p>
            <p :class = "(MainPage == 'EkspedientPage')?'btn-success':'btn-primary'" class="btn btn-primary rounded-0" v-if="this.firma_id != ''" @click="this.$emit('Update-Page', 'EkspedientPage')">Ekspedienter</p>
        </div>
    </div>
</template>
<script>
export default {

    props: {
        firma_id: String,
        MainPage: String
    }
}
</script>