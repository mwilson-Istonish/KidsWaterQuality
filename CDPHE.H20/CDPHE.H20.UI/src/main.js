import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import mitt from 'mitt';
import store from './store'

import './assets/styles/main.css'
import 'bootstrap/dist/css/bootstrap.css'
import bootstrap from 'bootstrap/dist/js/bootstrap.bundle.js'

const app = createApp(App)
const emitter = mitt();

app.config.globalProperties.API_URL = 'https://cdpheh2owebapi-dev.us-east-2.elasticbeanstalk.com/'

app.use(router)
app.use(bootstrap)
app.use(store)

app.provide('emitter', emitter);

app.mount('#app')