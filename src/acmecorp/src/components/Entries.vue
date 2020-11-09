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
        <div class="header">Failed to fetch messages. Please verify the following:</div>
        <ul class="list">
          <li class="content">You've downloaded one of our resource server examples, and it's running on port 8000.</li>
          <li class="content">Your resource server example is using the same Okta authorization server (issuer) that you have configured this Vue
            application to use.</li>
        </ul>
      </div>
    </div>

    <div v-if="submissions.length">
        <p>Page {{page}}</p>
      <table class="ui table">
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
            <td>{{submission.createdAt}}</td>
            <td>{{submission.firstName}}</td>
            <td>{{submission.lastName}}</td>
            <td>{{submission.email}}</td>
            <td>{{submission.serialNumber}}</td>
            <td>{{submission.confirmedAgeRequirement}}</td>
            <td>{{submission.acceptedTerms}}</td>
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
  name: 'Entry',
  data () {
    return {
      failed: false,
      submissions: [],
      page : 0
    }
  },
  async created () {
    try {
      const accessToken = await this.$auth.getAccessToken()
      console.log(accessToken)
      const response = await axios.get(
        sampleConfig.resourceServer.messagesUrl+"/submissions?page=1",
        {
          headers: {
            Authorization: `Bearer ${accessToken}`
          }
        }
      )
      this.page = response.data.page
      this.submissions = response.data.submissions

      console.log(response)
    } catch (e) {
      console.error(e)
      this.failed = true
    }
  }
}
</script>
