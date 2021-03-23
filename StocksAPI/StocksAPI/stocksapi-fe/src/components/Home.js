import React, { Component } from 'react';
import Container from 'react-bootstrap/Container';
import StocksContainer from './StocksContainer';
import DetailsContainer from './DetailsContainer';
import { Constants } from "../constants/Constants";

class Home extends Component {
    state = { portfolioData: {}, loading: true };

    async GetPortfolioData() {
        try {
            const res = await fetch
                (`${Constants.getPortfolio}`);
            return await res.json()
        } catch (error) {
            console.log(error);
            return [];
        }
    }

    async componentDidMount() {
        try {
            //const portfolioDataJSON = await this.GetPortfolioData();

            //portfolioDataJSON._PortfolioProfit = JSON.parse(JSON.stringify(portfolioDataJSON._PortfolioProfit), (key, value) =>
            //    typeof value === "number" && key != 'amount' ? value.toFixed(2) : value
            //);

            //var cleanedStocks = [];

            //portfolioDataJSON._CurrentStockPortfolio.forEach((stock) => {
            //    var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
            //        typeof value === "number" ? value.toFixed(3) : value
            //    );
            //    cleanedStocks.push(newItem);
            //})

            //portfolioDataJSON._CurrentStockPortfolio = cleanedStocks;

            //this.setState({ portfolioData: portfolioDataJSON, loading: false });

            const fakeData = {
                _PortfolioProfit: {
                    currentTotal: 1702.17,
                    purchaseTotal: 1507.63
                },
                _CurrentStockPortfolio: [
                    {
                        amount: 15.000,
                        averagePrice: 0.000,
                        country: "US",
                        currentPrice: 10.950,
                        currentValue: 212.236,
                        id: 1.000,
                        name: "BB",
                        profit: -59.116,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 14.000,
                        totalCost: 271.352,
                    },
                    {
                        amount: 214.624,
                        averagePrice: 0.000,
                        country: "US",
                        currentPrice: 3.280,
                        currentValue: 909.634,
                        id: 2.000,
                        name: "SENS",
                        profit: 173.326,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 2.650,
                        totalCost: 736.308
                    },
                    {
                        amount: 3517.000,
                        averagePrice: 0.000,
                        country: "AU",
                        currentPrice: 0.165,
                        currentValue: 580.305,
                        id: 3.000,
                        name: "LOT.AX",
                        profit: 80.335,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 0.148,
                        totalCost: 499.970
                    },
                    {
                        amount: 15.000,
                        averagePrice: 0.000,
                        country: "US",
                        currentPrice: 10.950,
                        currentValue: 212.236,
                        id: 1.000,
                        name: "APPL ",
                        profit: -59.116,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 14.000,
                        totalCost: 271.352,
                    },
                    {
                        amount: 214.624,
                        averagePrice: 0.000,
                        country: "US",
                        currentPrice: 3.280,
                        currentValue: 909.634,
                        id: 2.000,
                        name: "PLUG",
                        profit: 173.326,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 2.650,
                        totalCost: 736.308
                    },
                    {
                        amount: 3517.000,
                        averagePrice: 0.000,
                        country: "US",
                        currentPrice: 0.165,
                        currentValue: 580.305,
                        id: 3.000,
                        name: "TSLA",
                        profit: 80.335,
                        purchaseDate: "0001-01-01T00:00:00",
                        purchasePrice: 0.148,
                        totalCost: 499.970
                    }
                ]
            }

            fakeData._PortfolioProfit = JSON.parse(JSON.stringify(fakeData._PortfolioProfit), (key, value) =>
                typeof value === "number" && key != 'amount' ? value.toFixed(2) : value
            );

            var cleanedStocks = [];

            fakeData._CurrentStockPortfolio.forEach((stock) => {
                var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
                    typeof value === "number" ? value.toFixed(3) : value
                );
                cleanedStocks.push(newItem);
            })

            fakeData._CurrentStockPortfolio = cleanedStocks;

            this.setState({ portfolioData: fakeData, loading: false })
        } catch (e) {
            console.log(e);
        }
    }

    render() {
        if (this.state.loading) {
            return (<h1>Loading...</h1>)
        }

        return (<div className="App">
            <header className="App-header">
                <h1 className="page-title">🚀🚀🚀🚀🚀🚀</h1>
            </header>
            <div style={{ display: 'flex', justifyContent: 'space-around', flexWrap: 'wrap' }}>
                <div style={{ border: "5px solid black", marginTop: "20PX", backgroundColor: "#171123", borderRadius: "2px 16px", width:"95%" }} className="col-md-3 portfolio-summary">
                    <DetailsContainer PortfolioProfit={this.state.portfolioData._PortfolioProfit} />
                </div>
                <div className="col-md-7 cool-shadow stock-summary" style={{width:"95%", height: "80vh", marginTop: "20PX", overflowY: "auto", borderRadius: "4px 4px", paddingLeft: "20px", paddingRight: "20px", paddingTop: "10px", paddingBottom: "10px", backgroundColor: "#f27450" }}>
                    <StocksContainer CurrentStockPortfolio={this.state.portfolioData._CurrentStockPortfolio} />
                </div>
            </div>
        </div>)
    }
}

export default Home;