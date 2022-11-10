<template>
    <div class="row mh-100">
        
        <div class="col-lg-1 text-start">
            <SideBar :firma_id="this.firma_id" @Update-Page="ChangePage"></SideBar>
        </div>
        <div class="col-lg-11 d-flex justify-content-center text-start" v-if="this.firma_id == ''">
            <LoginPage @Login="Login"></LoginPage>
        </div>
        <div class="col-lg-11 justify-content-center text-start" v-if="this.firma_id != '' && this.MainPage == 'StartPage'">
            <StartPage :bruger="this.bruger"></StartPage>
        </div>
        <div v-if="this.MainPage == 'ProduktgruppePage'" class="col-lg-11 d-flex justify-content-center">
            <ProduktgruppePage :bearerToken="'FinalExamBearerToken'" :firma_id="this.firma_id"/>
        </div>
        <div v-if="this.MainPage == 'EkspedientPage'" class="col-lg-11 d-flex justify-content-center">
            <BrugerPage :bearerToken="'FinalExamBearerToken'" :firma_id="this.firma_id"></BrugerPage>
        </div>
    </div>
</template>

<script>
import ProduktgruppePage from './Page/ProduktgruppePage.vue';
import BrugerPage from './Page/BrugerPage.vue';
import SideBar from './Util/SideBar.vue';
import LoginPage from './Page/LoginPage.vue';
import StartPage from './Page/StartPage.vue';
export default {
    components: {
        LoginPage,
        ProduktgruppePage,
        BrugerPage,
        SideBar,
        StartPage
    },
    data() {
        return {
            MainPage: "",
            firma_id: "",
            bruger: ""
        }
    },
    methods: {
        ChangePage(PageToShow)
        {
            this.MainPage = PageToShow;
        },
        Login(bruger)
        {
            this.firma_id = bruger.firma_id;
            this.MainPage = "StartPage";
            this.bruger = bruger;
        }
    }
}
</script>

<style scoped>

</style>
<!-- Add "scoped" attribute to limit CSS to this component only -->
