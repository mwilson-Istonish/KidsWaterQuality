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
                                 user{{ getNumberWaiting != 1 ? "s" : "" }} awaiting review
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="input-group mb-3">
                                <span class="input-group-text" id="basic-addon1"><i class="fa-solid fa-magnifying-glass"></i></span>
                                <input type="text" class="form-control" placeholder="search users...">
                            </div>
                        </div>
                    </div>
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">Email</th>
                                <th scope="col">Role</th>
                                <th scope="col">Status</th>
                                <th scope="col">Module</th>
                                <th scope="col">Active</th>
                                <th scope="col">Last Sign In</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="user in this.Users">
                                <td>{{ user.Email }}</td>
                                <td>{{ user.Role }}</td>
                                <td :class="{'text-danger': user.Status == 'Attention Needed'}">{{ user.Status }}</td>
                                <td>{{ user.Module }}</td>
                                <td>{{ user.Active ? "✓" : "X" }}</td>
                                <td>{{ user.LastSignIn }}</td>
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
        Users:
        [
            {
                Email: "test@test.com",
                Role: "Admin",
                Status: "Revision Requested",
                Module: "Water Quality",
                Active: true,
                LastSignIn: new Date().toLocaleString('en-US')
            },
            {
                Email: "test123@test.com",
                Role: "User",
                Status: "Clear",
                Module: "Water Quality",
                Active: true,
                LastSignIn: new Date().toLocaleString('en-US')
            },
            {
                Email: "test321@test.com",
                Role: "User",
                Status: "Clear",
                Module: "Water Quality",
                Active: false,
                LastSignIn: new Date().toLocaleString('en-US')
            },
        ]
      }
    },
      
    methods: {},
    computed: {
        getNumberWaiting() {
            var numberWaiting = 0;
            for (var i = 0; i < this.Users.length; i++){
                numberWaiting += (this.Users[i].Status == "Attention Needed") ? 1 : 0
            }
            return numberWaiting;
        }
    }
  }
</script>