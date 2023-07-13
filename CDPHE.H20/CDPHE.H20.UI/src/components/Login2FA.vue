<template>
    <div class="modal modal-lg" tabindex="-1" id="Login2FAModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col"></div>
                        <div class="col text-center h20-modal-header">
                            Confirm Your Identity
                            <span>Please provide the login code that was sent to your email:</span>
                        </div>
                        <div class="col text-right text-end">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                    </div>
                    <div class="row h20-modal-spacing">
                        <div class="col-12">
                            <label>First Name</label>
                            <div class="input-group mb-3">
                                <input v-model="firstName" type="text" class="form-control">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  import { inject } from 'vue'
  export default {
    data() {
        return {
            firstName: "John",
            lastName: "Doe",
            email: "johndoe@gmail.com",
            createdDate: new Date().toLocaleString('en-US')
        }
    },
    created () {
        const emitter = inject('emitter');
        emitter.on('openProfileModal', (value) => {
            console.log('event fired', `value: ${value}`);
        });
    }
  }
</script>