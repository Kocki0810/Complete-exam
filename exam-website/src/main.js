import { createApp } from 'vue'
import App from './App.vue'
// Import Bootstrap and BootstrapVue CSS files (order is important)
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

// Make BootstrapVue available throughout your project

// Optionally install the BootstrapVue icon components plugin

createApp(App).mount('#app')
