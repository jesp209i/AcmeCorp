<template>
<div>
    <h1 class="heading">Generate Fake Contestants</h1>
    <p>
        This is a simple form to quickly create some fake contestants.
    </p>
    <ul>
        <li>
            You can still create: {{remaining}} contestants
        </li>
    </ul>
    <form id="fakes" class="ui form">
        <div class="field">
            <label>Amount of fakes:</label>
            <input type="number" v-model.number="amount" />
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
    data(){
        return{
            amount: 0,
            remaining: 0
        }
    },
    methods: {
        async sendForm() {
            try {
                const accessToken = await this.$auth.getAccessToken()
                var response = await axios.post(
                  sampleConfig.resourceServer.generateUrl,
                  { amount : this.amount },
                  { headers: {
                        Authorization: `Bearer ${accessToken}`
                    }
                    }
                  
                )
                if (response.status === 200){
                    this.remaining = response.data.remaining
                }
            } catch (e) {
                console.log(e)
            }
        }
    },
    async created () {
        await this.sendForm()
  },
}
</script>