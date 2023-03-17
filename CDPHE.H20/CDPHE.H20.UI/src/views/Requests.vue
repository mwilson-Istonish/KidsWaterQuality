<template>
    <div class="h20-dashboard-view h20-full-height">
    <div class="row h20-full-height">
        <div class="col">
            <div class="card h20-full-height">
                <div class="card-body" style="padding:2rem;">
                    <div class="row">
                        <div class="col-lg-9" style="font-size: 20px">
                            <div>
                                <span :class="{'text-danger': getNumberWaiting != 0}" style="font-weight:600; font-size:22px">
                                    {{ getNumberWaiting }}
                                </span>
                                 request{{ getNumberWaiting != 1 ? "s" : "" }} waiting
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="input-group mb-3">
                                <span class="input-group-text" id="basic-addon1"><i class="fa-solid fa-magnifying-glass"></i></span>
                                <input type="text" class="form-control" placeholder="search requests...">
                            </div>
                        </div>
                    </div>
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">School #</th>
                                <th scope="col">Status</th>
                                <th scope="col">Module</th>
                                <th scope="col">Updated</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="request in this.Requests">
                                <td>{{ request.SchoolNumber }}</td>
                                <td :class="{'text-danger': request.Status == 'Attention Needed'}">{{ request.Status }}</td>
                                <td>{{ request.Module }}</td>
                                <td>{{ request.Updated }}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    </div>
</template>

<script>
  export default {
    data () {
      return {
        Requests:
        [
            {
                SchoolNumber: 12345,
                Status: "Attention Needed",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
            {
                SchoolNumber: 54321,
                Status: "In Progress",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
            {
                SchoolNumber: 67890,
                Status: "Completed",
                Module: "Water Quality",
                Updated: new Date().toLocaleString('en-US')
            },
        ]
      }
    },
      
    methods: {},
    computed: {
        getNumberWaiting() {
            var numberWaiting = 0;
            for (var i = 0; i < this.Requests.length; i++){
                numberWaiting += (this.Requests[i].Status == "Attention Needed") ? 1 : 0
            }
            return numberWaiting;
        }
    }
  }
</script>