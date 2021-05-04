import React, { useEffect } from 'react'
import { loadUserFromStorage,signinRedirectCallback } from '../../Services/authService'
import { useHistory } from 'react-router-dom'

function SigninOidc() {
  const history = useHistory()
  useEffect(() => {
    async function signinAsync() {
      await signinRedirectCallback()
       loadUserFromStorage().then(token => {
          localStorage.setItem("token", token?.access_token);
          localStorage.setItem("name", token?.profile?.name);
        });
      history.push('/')
    }
    signinAsync()
  }, [history])

  return (
    <div>
      Redirecting...
    </div>
  )
}

export default SigninOidc