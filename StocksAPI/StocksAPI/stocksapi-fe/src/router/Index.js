import React from "react";
import { Router, Route, Switch } from "react-router-dom";
import * as createHistory from "history";
import Home from "../components/Home";
import LoginPage from "../components/Login/Login";
import AuthenticatedRoute from "./AuthenticatedRoute";
import StockInfo from "../components/StockInfo/StockInfo";

// Instead of BrowserRouter, we use the regular router,
// but we pass in a customer history to it.
export const history = createHistory.createBrowserHistory();

const AppRouter = () => (
  <Router history={history}>
    <div>
      <Switch>
        <Route path="/login" component={LoginPage} />
        <AuthenticatedRoute exact path="/" component={Home} />
        <AuthenticatedRoute path="/dashboard" component={Home} />
        <AuthenticatedRoute path="/:stock" component={StockInfo} />
      </Switch>
    </div>
  </Router>
);

export default AppRouter;
