<template>
    <div class="competition">
        <h1>Enter competition!</h1>
        <p>Win prizes - be wild!</p>
        <form id="competition">
        <table>
        
            <tr><td>First Name:</td><td> <input type="text" v-model="formdata.firstName" /></td></tr>
            <tr><td>Last Name:</td><td> <input type="text" v-model="formdata.lastName" /></td></tr>
            <tr><td>Email:</td><td> <input type="text" v-model="formdata.email" /></td></tr>
            <tr><td>Serial Number:</td><td> <input type="text" v-model="formdata.serialNumber" /></td></tr>
            <tr><td>Confirm you are over 18 years old</td><td> <input type="checkbox" v-model="formdata.confirmsCorrectAge" /></td></tr>
            <tr><td>Accept terms and conditions</td><td> <input type="checkbox" v-model="formdata.acceptsTerms" /></td></tr>
            <tr><td></td><td><button v-on:click.prevent="sendForm">Send</button></td></tr>
        </table></form>
        <pre>{{errors}}</pre>
    </div>
</template>
<script>
import axios from 'axios'
import sampleConfig from '../config'

export default {
    name: 'competition',
    data() {
        return {
            formdata :{
                firstName: '',
                lastName: '',
                email: '',
                serialNumber: '',
                confirmsCorrectAge: false,
                acceptsTerms : false
            },
            errors :[]
        }
    },
    methods: {
        async sendForm(){
            console.log(this.formdata)
            try {
                await axios.post(
                sampleConfig.resourceServer.messagesUrl,
                this.formdata
                )
            } catch (e) {
                this.errors = e.response.data.errors
                console.log(e.response)
            }
        }
    }
}
</script>
<style scoped>

</style>