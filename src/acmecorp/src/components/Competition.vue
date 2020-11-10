<template>
    <div class="competition">
        <h1>Enter competition!</h1>
        <p>Win prizes - Just enter a qualifying serial number, and you will get two entries in the draw.</p>
        
        
        <div v-if="success" class="ui success icon message" :class="{'hidden': !success}">
          <i class="inbox icon"></i>
          <i class="close icon" v-on:click="toggleSuccess()"></i>
          <div class="content">
          <div class="header">Congratulations!</div>
          <p>You have entered the competition!</p>
          </div>
        </div>
        
        
        <form v-if="!success" id="competition" :class="['ui','form',{'error': errors}]">
        <div :class="['field', {'error': errorResponse.SerialNumber}]">
            <label>Serial Number:</label>
            <input type="text" v-model="formdata.serialNumber" />
        </div>
        <div class="ui error message" :class="{'hidden': !errorResponse.SerialNumber}">
          <div class="header">Check the Serial Number</div>
          <ul>
            <li v-for="(err, idx) in errorResponse.SerialNumber" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <div :class="['field', {'error': errorResponse.FirstName}]">
            <label>First Name:</label>
            <input type="text" v-model="formdata.firstName" />
        </div>
                <div class="ui error message" :class="{'hidden': !errorResponse.FirstName}">
          <div class="header">Error in First Name Field</div>
          <ul>
            <li v-for="(err, idx) in errorResponse.FirstName" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <div :class="['field', {'error': errorResponse.LastName}]">
          <label>Last Name:</label>
          <input type="text" v-model="formdata.lastName" />
        </div>
                <div class="ui error message" :class="{'hidden': !errorResponse.LastName}">
          <div class="header">Error in Last Name Field</div>
          <ul>
            <li v-for="(err, idx) in errorResponse.LastName" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <div :class="['field', {'error': errorResponse.Email}]">
          <label>Email:</label>
          <input type="text" v-model="formdata.email" />
        </div>
                <div class="ui error message" :class="{'hidden': !errorResponse.Email}">
          <div class="header">Error in Email Field</div>
          <ul>
            <li v-for="(err, idx) in errorResponse.Email" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <div class="two fields">
        <div :class="['field', {'error': errorResponse.ConfirmsCorrectAge}]">
          <div class="ui checkbox" >
            <input type="checkbox" v-model="formdata.confirmsCorrectAge" />
          <label>Confirm you are over 18 years old</label>
          </div>
        </div>
          <div :class="['field', {'error': errorResponse.AcceptsTerms}]">
            <div class="ui checkbox">
              <input type="checkbox" v-model="formdata.acceptsTerms" />
              <label>Accept terms and conditions</label>
            </div>
          </div>
        </div>
        <div class="ui error message" :class="{'hidden': !errorResponse.ConfirmsCorrectAge}">
          <ul>
            <li v-for="(err, idx) in errorResponse.ConfirmsCorrectAge" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <div class="ui error message" :class="{'hidden': !errorResponse.AcceptsTerms}">
          <ul>
            <li v-for="(err, idx) in errorResponse.AcceptsTerms" :key="idx">{{ err}}</li>
          </ul>
        </div>
        <button type="submit" class="ui blue right labeled icon button" v-on:click.prevent="sendForm">
          <i class="right arrow icon"></i>
          Send</button>
        </form>
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
            success: false,
            errors :false,
            errorResponse: []
        }
    },
    methods: {
        async sendForm(){
            this.errors = []
            try {
                var response = await axios.post(
                  sampleConfig.resourceServer.competitionUrl,
                  this.formdata
                )
                if (response.status === 200){
                    this.errors = false
                    this.errorResponse = []
                    this.success = true
                    this.clearForm()
                }
            } catch (e) {
                this.errorResponse = e.response.data.errors
                this.errors = true;
                this.success = false;
            }
        },
        clearForm() {
            this.formdata.firstName = ''
            this.formdata.lastName = ''
            this.formdata.email = ''
            this.formdata.serialNumber = ''
            this.formdata.confirmsCorrectAge = false
            this.formdata.acceptsTerms = false
        },
        toggleSuccess(){
          this.success = !this.success
        }
    }
}
</script>
<style scoped>

</style>