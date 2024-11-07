import React, { useState } from 'react';
import axios from 'axios';

const Login = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
      await axios.post('http://localhost:5187/api/account/login', {
        username,
        password,
      }).then((response) => {
        localStorage.setItem('token', response.data.token);
        window.location.reload();
      }).catch(error => {
      setError(error.response?.data?.message || 'Login failed');
    });
  };

  const handleRegister = async () => {
      axios.post('http://localhost:5187/api/account/register', {
        userName: username,
        emailAddress: `${username}@gmail.com`,
        password: password,
      }).then(() => {
        setUsername('');
        setPassword('');
        setSuccess('Registration successful');
      }).catch((error) => {
      setError(error.response?.data?.message || 'Register failed');
    });
  }

  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
      <div style={{ width: '300px', textAlign: 'center' }}>
        <form onSubmit={handleLogin}>
          <div style={{ marginBottom: '15px' }}>
            <label htmlFor="username">Корисник:</label>
            <input
              type="text"
              id="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
              style={{ width: '100%', padding: '8px', boxSizing: 'border-box' }}
            />
          </div>
          <div style={{ marginBottom: '15px' }}>
            <label htmlFor="password">Лозинка:</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              style={{ width: '100%', padding: '8px', boxSizing: 'border-box' }}
            />
          </div>
          {error && <p style={{ color: 'red' }}>{error}</p>}
          {success && <p style={{ color: 'green' }}>{success}</p>}
          <button type="submit" style={{ width: '100%', padding: '10px', backgroundColor: '#4CAF50', color: 'white', border: 'none', cursor: 'pointer' }}>
            Најави се
          </button>
          <button type="button" onClick={handleRegister} style={{ width: '100%', padding: '10px', backgroundColor: '#007BFF', color: 'white', border: 'none', cursor: 'pointer', marginTop: '10px' }}>
          Регистрација
        </button>
        </form>
      </div>
    </div>
  );
};

export default Login;