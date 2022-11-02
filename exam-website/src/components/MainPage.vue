<template>
    <div class="container-fluid row">
        
        <div class="col-sm-1 text-start">
            <SideBar @Update-Page="ChangePage"></SideBar>
        </div>
        <div v-if="this.MainPage == 'ProduktgruppePage'" class="col-sm-11 d-flex justify-content-center">
            <div class="ProduktgruppePage">
                <ProduktgruppePage :firma_id="1"/>
            </div>
        </div>
        <div v-if="this.MainPage == 'EkspedientPage'" class="col-sm-11 d-flex justify-content-center">
            <div  class="BrugePage">
                <BrugerPage :firma_id="1"></BrugerPage>
            </div>
        </div>
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
