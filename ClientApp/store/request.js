import axios from 'axios'
import store from './index'
import { getToken, baseUrl } from './common'

// create an axios instance
const service = axios.create({
  baseURL: baseUrl(),
  timeout: 10000 // request timeout
})
// request interceptor
service.interceptors.request.use(config => {
  if (store.state.token !== undefined) {
    config.headers['Authorization'] = "Bearer " + getToken()
  }
  return config
}, error => {
  console.log(error) // for debug
})

// respone interceptor
service.interceptors.response.use(
  response => response.data,
  error => {
    let errorResponse = error.response
    console.log('err' + error) // for debug
  })

export default service
