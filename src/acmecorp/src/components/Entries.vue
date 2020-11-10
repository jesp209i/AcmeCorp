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
  <div class="submissions">
    <h1 class="ui header">
      <i
        aria-hidden="true"
        class="mail outline icon"
      >
      </i>
      Entries to the contest
    </h1>
    <div
      v-if="failed"
      class="ui error message"
    >
      <div class="content">
        <div class="header">Failed to fetch entries.</div>
      </div>
    </div>

    <div v-if="submissions.length">
      <div class="ui pagination menu">
        <div v-for="i in maxPage" v-bind:key="i" :class="['item', 'clickable',{'active': i === currentPage }]" v-on:click="fetchEntries(i)">{{i}}</div>
      </div>
      <table class="ui table" v-if="!loading">
        <thead>
          <tr>
            <th>Id</th>
            <th>Created At</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Serial Number</th>
            <th>Confirmed Age</th>
            <th>Accepted Terms</th>

          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(submission) in submissions"
            :key="submission.id"
            :id="'submission-' + submission.id"
          >
            <td>{{submission.id}}</td>
            <td>{{prettyDate(submission.createdAt)}}</td>
            <td>{{submission.firstName}}</td>
            <td>{{submission.lastName}}</td>
            <td>{{submission.email}}</td>
            <td>{{submission.serialNumber}}</td>
            <td><i class="check icon"></i></td>
            <td><i class="check icon"></i></td>
          </tr>
        </tbody>
      </table>
      <div class="ui fluid placeholder" v-if="loading">
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
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import sampleConfig from '../config'

export default {
  name: 'Entry',
  data () {
    return {
      failed: false,
      submissions: [],
      currentPage : 1,
      maxPage : 1,
      loading : false
    }
  },
  methods: {
    async fetchEntries(page){
      this.loading = true
      try {
      const accessToken = await this.$auth.getAccessToken()
      const response = await axios.get(
        sampleConfig.resourceServer.submissionsUrl+page,
        {
          headers: {
            Authorization: `Bearer ${accessToken}`
          }
        }
      )
      this.currentPage = response.data.page
      this.submissions = response.data.submissions
      this.maxPage = response.data.maxPage
      this.loading = false
      } catch (e) {
        console.error(e)
        this.failed = true
      }
    },
    prettyDate(date){
      if (date === undefined) {
        return "";
      }
      const mydate = new Date(date);
      return mydate.toLocaleDateString("en-GB", {
        year: "numeric",
        month: "short",
        day: "numeric"
      }) +' '+ mydate.toLocaleTimeString("en-GB")
    }
  },
  async created () {
    await this.fetchEntries(this.currentPage)
  },
}
</script>
<style scoped>
.clickable:hover { cursor: pointer;}
</style>