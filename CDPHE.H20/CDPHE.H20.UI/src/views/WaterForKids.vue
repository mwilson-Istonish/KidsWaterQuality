<template>
    <div class="h20-dashboard-view h20-full-height">
        <div class="row h20-full-height">
            <div class="col">
                <div class="card h20-full-height">
                    <div class="card-body" style="padding:2rem;">
                        <div class="row">
                            <div class="col text-end">
                                <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#NewRequestModal">New Request</button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <div class="card">
                                    <div class="card-body">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal modal-lg" tabindex="-1" id="NewRequestModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <input type="file">
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  import RequestsMixin from '../mixins/RequestsMixin'
  import { useStore } from "vuex";
  export default {
    mixins: [RequestsMixin],
    async created() {
    },
    data () {
      return {
        store: useStore(),
      }
    },
      
    methods: {
        async getRequestDetails(requestId) {
            await this.getRequestDetailsAPI(requestId);
        }, 
    },
    computed: {
    }
  }
</script>