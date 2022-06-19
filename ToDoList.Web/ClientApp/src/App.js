import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import { AuthContextComponent } from './AuthContext';
import Signup from './Pages/SignUp';
import Login from './Pages/Login';
import Logout from './Pages/Logout';
import Home from './Pages/Home'
import PrivateRoute from './components/PrivateRoute';






export default class App extends Component {
  static displayName = App.name;
  render() {
    return (
  <AuthContextComponent>
      <Layout>
          <Route exact path='/signup' component={Signup} />
          <Route exact path='/login' component={Login} />
          <Route exact path='/logout' component={Logout} />
          <PrivateRoute exact path='/' component={Home}/>
      </Layout>
  </AuthContextComponent>
 
    
    );
  }
}