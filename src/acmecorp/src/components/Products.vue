<template>
<div class="products">
    <h1>Products</h1>
    <div>
<table>
    <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Serial Number</th>
    </tr>
    <tr v-for="(product) in products"
            :key="product.id"
            :id="'product-' + product.id"
          >
        <td>{{product.id}}</td>
        <td>{{product.name}}</td>
        <td>{{product.serialNumber}}</td>
    </tr>
    </table>

    </div>
</div>
</template>
<script>
import axios from 'axios'
import sampleConfig from '../config'

export default {
    name : 'products',
    data (){
        return{
            products: []
        }
    },
    async created(){
        try {
      const accessToken = await this.$auth.getAccessToken()
      const response = await axios.get(
        sampleConfig.resourceServer.messagesUrl+"/products",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`
          }
        }
      )
      
      this.products = response.data

      console.log(response)
    } catch (e) {
      console.error(e)
      this.failed = true
    }
    }
}
</script>
<style scoped>

</style>