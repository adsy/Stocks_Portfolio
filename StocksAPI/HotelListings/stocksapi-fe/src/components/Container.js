import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'
import React, { Component } from "react";
import { Constants } from "../constants/Constants";
import StockProfile from "./stockProfile/StockProfile";
import Card from 'react-bootstrap/Card'

class Container extends Component {
    state = { stocks: [], loading: true };

    async GetStocks() {
        try {
            console.log('getting stocks');
            const res = await fetch
                (`${Constants.getStockProfilesAPIUrl}`);
            return await res.json()
        } catch (error) {
            console.log(error);
            return [];
        }
    }

    async componentDidMount() {
        try {
            const stocks = await this.GetStocks();
            this.setState({ stocks: stocks, loading: false });
            console.log(stocks);
        } catch (error) {
        }
    }

    render() {
        if (this.state.loading) {
            return (
                <div>
                    <h1 class="AppHeader">
                        Loading...
                    </h1>
                </div>
            );
        }

        return (<div className="container-css">
            {this.state.stocks.map((stock, index) => (
                <Card body style={{ width: "80%"}}>
                                <StockProfile stock={stock} />
                            </Card>
                     ))}
                </div>);
    }
}

export default Container;