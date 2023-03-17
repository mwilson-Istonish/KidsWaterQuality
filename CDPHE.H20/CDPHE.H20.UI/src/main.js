import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import './assets/styles/main.css'
import 'bootstrap/dist/css/bootstrap.css'
import bootstrap from 'bootstrap/dist/js/bootstrap.bundle.js'

const app = createApp(App)

app.config.globalProperties.API_URL = 'https://localhost:7072/'

app.use(router)

app.use(bootstrap)

app.mount('#app')