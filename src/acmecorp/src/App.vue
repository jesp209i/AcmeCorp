/*!
 * Copyright (c) 2018, Okta, Inc. and/or its affiliates. All rights reserved.
 * The Okta software accompanied by this notice is provided pursuant to the Apache License, Version 2.0 (the "License.")
 *
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0.
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *
 * See the License for the specific language governing permissions and limitations under the License.
 */

<template>
  <div id="app">
    <div class="ui top menu">
      <div class="ui container">
        <router-link
          to="/"
          class="header item"
        >
          <img
            class="ui mini image"
            src="./assets/logo.png" title="Acme Corp"
          >
        </router-link>
        <router-link
          to="/competition"
          :class="['item', {'active': currentPage =='/competition'}]"
          id="competition-button"
          v-if="!authenticated"
        >
          <i
            aria-hidden="true"
            class="mail outline icon">
          </i>
          Competition
        </router-link>
        <a
          class="item"
          v-if="!authenticated"
          v-on:click="login()"
        >
        Login
        </a>
        <router-link
          to="/entries"
          :class="['item', {'active': currentPage =='/entries'}]"
          id="entries-button"
          v-if="authenticated"
        >
          <i
            aria-hidden="true"
            class="mail outline icon">
          </i>
          Entries
        </router-link>
        <router-link
          to="/products"
          :class="['item', {'active': currentPage =='/products'}]"
          id="products-button"
          v-if="authenticated"
        >
          <i
            aria-hidden="true"
            class="barcode icon">
          </i>
          Products
        </router-link>
        <router-link
          to="/fakes"
          :class="['item', {'active': currentPage =='/fakes'}]"
          id="fakes-button"
          v-if="authenticated"
        >
          <i
            aria-hidden="true"
            class="barcode icon">
          </i>
          Fake Contestants
        </router-link>
        <router-link
          to="/"
          id="logout-button"
          class="item"
          v-if="authenticated"
          v-on:click.native="logout()"
        >
        Logout
        </router-link>
      </div>
    </div>
    <div class="ui container" >
      <router-view/>
    </div>
  </div>
</template>

<script>
export default {
  name: 'app',
  data: function () {
    return { 
      authenticated: false,
      currentPage: ""
     }
  },
  created () { 
    this.isAuthenticated() 
    },
  watch: {
    // Everytime the route changes, check for auth status
    '$route': 'routeChange'
  },
  methods: {
    async routeChange(){
      await this.isAuthenticated()
      var path = this.$route.path
      this.currentPage = path.toLowerCase()
    },
    async isAuthenticated () {
      this.authenticated = await this.$auth.isAuthenticated()
    },
    login () {
      this.$auth.loginRedirect('/entries')
    },
    async logout () {
      await this.$auth.logout()
      await this.isAuthenticated()
    },
  }
}
</script>
