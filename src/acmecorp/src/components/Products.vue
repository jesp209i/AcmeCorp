<template>
<div class="products">
    <h1 class="ui header"><i
        aria-hidden="true"
        class="barcode icon"
      >
      </i>Products</h1>
      <div v-if="loading" class="ui fluid placeholder">
  <div class="image header">
    <div class="line"></div>
    <div class="line"></div>
  </div>
  <div class="paragraph">
    <div class="line"></div>
    <div class="line"></div>
    <div class="line"></div>
  </div>
</div>
    <div>
<table class="ui table" v-if="!loading">
  <thead>
    <tr>
        <th>Id</th>
        <th>Name</th>
        <th>Serial Number</th>
    </tr></thead>
    <tbody>
    <tr v-for="(product) in products"
            :key="product.id"
            :id="'product-' + product.id"
          >
        <td>{{product.id}}</td>
        <td>{{product.name}}</td>
        <td>{{product.serialNumber}}</td>
    </tr>
    </tbody>
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
            products: [],
            loading : true
        }
    },
    async created(){
        try {
      const accessToken = await this.$auth.getAccessToken()
      const response = await axios.get(
        sampleConfig.resourceServer.productsUrl,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`
          }
        }
      )
      
      this.products = response.data

      //console.log(response)
      this.loading = false
    } catch (e) {
      console.error(e)
      this.failed = true
    }
    }
}
</script>
<style scoped>
tr:hover { background : #ccc;}
</style>