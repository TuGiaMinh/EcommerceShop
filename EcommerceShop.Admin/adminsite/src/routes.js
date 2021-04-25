import React from "react";
import { Switch, Route } from "react-router-dom";
import Dashboard from "./Pages/Dashboard/dashboard";
import Product from "./Pages/Products/product";
import Category from "./Pages/Categories/category";
import Brand from "./Pages/Brands/brand";
import User from "./Pages/Users/user";
import NotMatch from "./Pages/Errors/error";

export default function Routes() {
  return (
    <Switch>
      <Route exact path="/home">
        <Dashboard />
      </Route>
      
      <Route path="/users">
        <User />
      </Route>
      <Route path="/products">
        <Product />
      </Route>
      <Route path="/categories">
        <Category />
      </Route>
      <Route path="/brands">
        <Brand />
      </Route>
      <Route path="*">
        <NotMatch />
      </Route>
    </Switch>
  );
}