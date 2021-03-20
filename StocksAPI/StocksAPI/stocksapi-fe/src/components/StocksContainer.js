﻿import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from "react";
import { Constants } from "../constants/Constants";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';

class StocksContainer extends Component {
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
            // api calls
            //const stocks = await this.GetStocks();

            //var cleanedStocks = [];

            //stocks.forEach((stock) => {
            //    var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
            //        typeof value === "number" ? Math.round(value * 100) / 100 : value
            //    );
            //    cleanedStocks.push(newItem);
            //})

            //this.setState({ stocks: cleanedStocks, loading: false });

            const mockStocks = [
                { name: 'BB', amount: '25', currentPrice: 10.05, currentValue: 800, profit: 200.35762345 },
                { name: "SENS", amount: 200.1234, currentPrice: 10.05, currentValue: 200.35762345, profit: 200.35762345 },
                { name: "LOT.AX", amount: 25, currentPrice: 10.05, currentValue: 800, profit: 200.35762345 }
            ];

            var cleanedStocks = [];

            mockStocks.forEach((stock) => {
                var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
                    typeof value === "number" && key != 'amount' ? value.toFixed(2) : value
                );
                cleanedStocks.push(newItem);
            })

            const timer = await setTimeout(() => {
                console.log('timer done');
                this.setState({
                    stocks: cleanedStocks,
                    loading: false
                });
            }, 2000);
        } catch (error) {
            console.log(error)
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
                <Container body className="stock-container-css" style={{ width: "110%", marginTop: "10px", marginBottom: "10px", paddingLeft: '-15px' }}>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "center" }} className="col-xs-12">
                        <h3 style={{ paddingTop: '5px' }}>{stock.name} 🚀</h3>
                    </Row>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "space-around" }}>
                        <div className="col-xs-3">
                            <h6 style={{ color: "yellow" }}>Amount</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6 style={{ color: "yellow" }}>Current Price</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6 style={{ color: "yellow" }}>Current Value</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6 style={{ color: "yellow" }}>Total Profit</h6>
                        </div>
                    </Row>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "space-around" }}>
                        <div className="col-xs-3">
                            <h6> {stock.amount}</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6> ${(stock.currentPrice)}</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6> ${stock.currentValue}</h6>
                        </div>
                        <div className="col-xs-3">
                            <h6> ${stock.profit}</h6>
                        </div>
                    </Row>

                </Container>
            ))}
        </div>);
    }
}

export default StocksContainer;