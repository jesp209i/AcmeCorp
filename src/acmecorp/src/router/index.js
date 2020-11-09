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

import Vue from 'vue'
import Router from 'vue-router'
import 'semantic-ui-css/semantic.min.css'

import Auth from '@okta/okta-vue'

import HomeComponent from '@/components/Home'
import EntryComponent from '@/components/Entries'
import CompetitionComponent from '@/components/Competition'
import ProductsComponent from '@/components/Products'

import sampleConfig from '@/config'

Vue.use(Router)
Vue.use(Auth, sampleConfig.oidc)

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: HomeComponent
    },
    {
      path: '/login/callback',
      component: Auth.handleCallback()
    },
    {
      path: '/entries',
      component: EntryComponent,
      meta: {
        requiresAuth: true
      }
    },
    {
      path: '/competition',
      component: CompetitionComponent
    },
    {
      path: '/products',
      component: ProductsComponent,
      meta: {
        requiresAuth: true
        }
    }
  ]
})

router.beforeEach(Vue.prototype.$auth.authRedirectGuard())

export default router
