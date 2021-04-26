import React from "react";
import { Switch, Route } from "react-router-dom";
import Dashboard from "./Pages/Dashboard/dashboard";
import Product from "./Pages/Products/product";
import Category from "./Pages/Categories/category";
import Brand from "./Pages/Brands/brand";
import User from "./Pages/Users/user";
import NotMatch from "./Pages/Errors/error";
import PrivateRouter from './Helpers/PrivateRouter'

export default function Routes() {
  return (
    <Switch>
      <PrivateRouter path="/home" component={Dashboard} />
      <PrivateRouter path="/users" component={User} />
      <PrivateRouter path="/products" component={Product} />
      <PrivateRouter path="/categories" component={Category} />
      <PrivateRouter path="/brands" component={Brand} />
      <Route path="*">
        <NotMatch />
      </Route>
    </Switch>
  );
}