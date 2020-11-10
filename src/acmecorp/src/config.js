const { CLIENT_ID, ISSUER, OKTA_TESTING_DISABLEHTTPSCHECK } = process.env

export default {
  oidc: {
    clientId: CLIENT_ID,
    issuer: ISSUER,
    redirectUri: 'http://localhost:8080/login/callback',
    scopes: ['openid', 'profile', 'email'],
    pkce: true,
    testing: {
      disableHttpsCheck: OKTA_TESTING_DISABLEHTTPSCHECK
    }
  },
  resourceServer: {
    competitionUrl: 'https://localhost:5001/api/competition',
    submissionsUrl: 'https://localhost:5001/api/competition/submissions?page=',
    productsUrl: 'https://localhost:5001/api/competition/products',

  }
}
