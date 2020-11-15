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
  <div id="home">
    <h1 class="ui header">Welcome to Acme Corp</h1>
    <div v-if="!this.$parent.authenticated">
      <p>You can enter the <router-link to="/competition">competition by following this link</router-link>.</p>
      
    </div>

    <div v-if="this.$parent.authenticated">
      <p>Welcome back, {{claims && claims.name}}!</p>
      <p>
        You can se entries to the competition and all valid serial numbers by clicking on the the relevant link in the top or just below:
      </p>
      <ul class="ui list">
        <li class="ui list-item"><router-link to="/entries">Entries</router-link></li>
        <li class="ui list-item"><router-link to="/products">Products</router-link></li>
      </ul>
      <!--<pre>{{claims}}</pre>-->
    </div>
  </div>
</template>

<script>
export default {
  name: 'home',
  data: function () {
    return {
      claims: '',
      token : ''
    }
  },
  created () {
    this.setup()
    },
  methods: {
    async setup () {
      this.claims = await this.$auth.getUser()
    },
    login () {
      this.$auth.loginRedirect('/')
    }
  }
}
</script>
