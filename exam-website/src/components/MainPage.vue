<template>
    <div class="SideBar">
        <SideBar @Update-Page="ChangePage"></SideBar>
    </div>
    <div v-if="this.MainPage == 'ProduktgruppePage'" class="ProduktgruppePage">
        <ProduktgruppePage :firma_id="1"/>
    </div>
    <div v-if="this.MainPage == 'EkspedientPage'" class="BrugePage">
        <BrugerPage :firma_id="1"></BrugerPage>
    </div>
</template>

<script>
import ProduktgruppePage from './Page/ProduktgruppePage.vue';
import BrugerPage from './Page/BrugerPage.vue';
import SideBar from './Util/SideBar.vue';

export default {
    components: {
        ProduktgruppePage,
        BrugerPage,
        SideBar
    },
    data() {
        return {
            MainPage: "StartPage"
        }
    },
    methods: {
        ChangePage(PageToShow)
        {
            this.MainPage = PageToShow;
        }
    }
}
</script>

<style scoped>

</style>
<!-- Add "scoped" attribute to limit CSS to this component only -->
