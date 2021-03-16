import 'bootstrap/dist/css/bootstrap.min.css';
import '../App.css'
import React, { Component } from "react";
import { Constants } from "../constants/Constants";
import StockProfile from "./stockProfile/StockProfile";
import Container from 'react-bootstrap/Container'
import Card from 'react-bootstrap/esm/Card';

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
            // const stocks = await this.GetStocks();
            // this.setState({ stocks: stocks, loading: false });

            const mockStocks = [
                { name: 'BB', amount: '25', currentPrice: 10.05 },
                { name: "SENS", amount: 25, currentPrice: 10.05 },
                { name: "LOT.AX", amount: 25, currentPrice: 10.05 }
            ];


            console.log(mockStocks)

            const timer = await setTimeout(() => {
                console.log('timer done');
                this.setState({
                    stocks: mockStocks,
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

        return (<div className="container-css" style={{ backgroundColor: "grey" }}>
            {this.state.stocks.map((stock, index) => (
                <Container body className="stock-container-css" style={{ width: "50%", marginTop:"10px", marginBottom:"10px", height:"7vh"}}>
                    <div className="col-lg-4">
                        <h3 style={{ paddingTop: '10px' }}>{stock.name}🚀</h3>
                    </div>
                    <StockProfile stock={stock} />
                    <Card body>
                        <h6> @ ${stock.currentPrice}</h6>
                    </Card>
                </Container>
                     ))}
                </div>);
    }
}

export default StocksContainer;