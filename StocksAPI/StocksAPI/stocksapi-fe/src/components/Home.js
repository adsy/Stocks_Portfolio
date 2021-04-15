import React, { Component } from "react";
import StocksContainer from "./StocksContainer";
import DetailsContainer from "./DetailsContainer";
import { Constants } from "../constants/Constants";
import AddStockModal from "./AddStock/AddStockModal";
import axios from "axios";

class Home extends Component {
  constructor(props) {
    super(props);
    this.state = { portfolioData: {}, stockPortfolio: {}, loading: true };

    this.UpdateFromStockApi = this.UpdateFromStockApi.bind(this);
  }

  async UpdateFromStockApi() {
    try {
      this.setState({ loading: true });

      var portfolioDataJSON = await axios
        .get(`${Constants.getPortfolio}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        })
        .catch((error) => {
          console.log(error);
        });

      portfolioDataJSON = portfolioDataJSON.data;

      portfolioDataJSON._PortfolioProfit = JSON.parse(
        JSON.stringify(portfolioDataJSON._PortfolioProfit),
        (key, value) =>
          typeof value === "number" && key !== "amount"
            ? value.toFixed(2)
            : value
      );

      var cleanedStocks = [];

      portfolioDataJSON._CurrentStockPortfolio.forEach((stock) => {
        var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
          typeof value === "number" ? value.toFixed(3) : value
        );
        cleanedStocks.push(newItem);
      });

      portfolioDataJSON._CurrentStockPortfolio = cleanedStocks;

      this.setState({
        portfolioData: portfolioDataJSON._PortfolioProfit,
        stockPortfolio: portfolioDataJSON._CurrentStockPortfolio,
        loading: false,
      });

      // // fake stock data for portfolio
      // const fakeData = {
      //     _PortfolioProfit: {
      //         currentTotal: 1702.17,
      //         purchaseTotal: 1507.63
      //     },
      //     _CurrentStockPortfolio: [
      //         {
      //             amount: 15.000,
      //             averagePrice: 0.000,
      //             country: "US",
      //             currentPrice: 10.950,
      //             currentValue: 212.236,
      //             id: 1.000,
      //             name: "BB",
      //             profit: -59.116,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 14.000,
      //             totalCost: 271.352,
      //         },
      //         {
      //             amount: 214.624,
      //             averagePrice: 0.000,
      //             country: "US",
      //             currentPrice: 3.280,
      //             currentValue: 909.634,
      //             id: 2.000,
      //             name: "SENS",
      //             profit: 173.326,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 2.650,
      //             totalCost: 736.308
      //         },
      //         {
      //             amount: 3517.000,
      //             averagePrice: 0.000,
      //             country: "AU",
      //             currentPrice: 0.165,
      //             currentValue: 580.305,
      //             id: 3.000,
      //             name: "LOT.AX",
      //             profit: 80.335,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 0.148,
      //             totalCost: 499.970
      //         },
      //         {
      //             amount: 15.000,
      //             averagePrice: 0.000,
      //             country: "US",
      //             currentPrice: 10.950,
      //             currentValue: 212.236,
      //             id: 1.000,
      //             name: "APPL ",
      //             profit: -59.116,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 14.000,
      //             totalCost: 271.352,
      //         },
      //         {
      //             amount: 214.624,
      //             averagePrice: 0.000,
      //             country: "US",
      //             currentPrice: 3.280,
      //             currentValue: 909.634,
      //             id: 2.000,
      //             name: "PLUG",
      //             profit: 173.326,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 2.650,
      //             totalCost: 736.308
      //         },
      //         {
      //             amount: 3517.000,
      //             averagePrice: 0.000,
      //             country: "US",
      //             currentPrice: 0.165,
      //             currentValue: 580.305,
      //             id: 3.000,
      //             name: "TSLA",
      //             profit: 80.335,
      //             purchaseDate: "0001-01-01T00:00:00",
      //             purchasePrice: 0.148,
      //             totalCost: 499.970
      //         }
      //     ]
      // }

      // fakeData._PortfolioProfit = JSON.parse(JSON.stringify(fakeData._PortfolioProfit), (key, value) =>
      //     typeof value === "number" && key != 'amount' ? value.toFixed(2) : value
      // );

      // var cleanedStocks = [];

      // fakeData._CurrentStockPortfolio.forEach((stock) => {
      //     var newItem = JSON.parse(JSON.stringify(stock), (key, value) =>
      //         typeof value === "number" ? value.toFixed(3) : value
      //     );
      //     cleanedStocks.push(newItem);
      // })

      // fakeData._CurrentStockPortfolio = cleanedStocks;

      // this.setState({ portfolioData: fakeData, loading: false })
    } catch (e) {
      console.log(e);
    }
  }

  async componentDidMount() {
    this.UpdateFromStockApi();
  }

  render() {
    if (this.state.loading) {
      return (
        <div className="App">
          <div>
            <h1 className="loading-center">🚀</h1>
          </div>
        </div>
      );
    }

    return (
      <div className="App">
        <header className="App-header">
          <h1 className="page-title">Portfolio</h1>
        </header>
        <div
          style={{
            display: "flex",
            justifyContent: "space-around",
            flexWrap: "wrap",
          }}
        >
          <div className="col-md-3 portfolio-summary">
            <div
              style={{
                border: "5px solid black",
                marginTop: "20PX",
                backgroundColor: "#171123",
                borderRadius: "2px 16px",
                justifyContent: "center",
                display: "flex",
              }}
            >
              <DetailsContainer PortfolioProfit={this.state.portfolioData} />
            </div>
            <br />
            <AddStockModal
              CurrentStockPortfolio={this.state.stockPortfolio}
              PortfolioData={this.state.portfolioData}
              Update={this.UpdateFromStockApi}
            />
          </div>
          <div
            className="col-md-7 cool-shadow stock-summary"
            style={{
              width: "95%",
              height: "80vh",
              marginTop: "20PX",
              overflowY: "auto",
              borderRadius: "4px 4px",
              paddingLeft: "20px",
              paddingRight: "20px",
              paddingTop: "10px",
              paddingBottom: "10px",
              backgroundColor: "#f27450",
            }}
          >
            <StocksContainer
              CurrentStockPortfolio={this.state.stockPortfolio}
              PortfolioData={this.state.portfolioData}
              Update={this.UpdateFromStockApi}
            />
          </div>
        </div>
      </div>
    );
  }
}

export default Home;
