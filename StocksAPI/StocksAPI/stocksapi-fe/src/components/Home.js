import React, { Component } from "react";
import StocksContainer from "./StocksContainer/StocksContainer";
import DetailsContainer from "./DetailsContainer";
import AddStockModal from "./AddStock/AddStockModal";
import AddCryptoModal from "./AddCrypto/AddCryptoModal";
import AppHeader from "./Header/AppHeader";
import axios from "axios";
import { Constants } from "../constants/Constants";
import Chart from "./Chart/PortfolioChart";
import CryptoContainer from "./CryptoContainer/CryptoContainer";
import { client } from "../cache/baseClient";

class Home extends Component {
  constructor(props) {
    super(props);
    this.state = {
      portfolioData: {},
      stockPortfolio: {},
      cryptoPortfolio: {},
      loading: true,
    };

    this.UpdateFromStockApi = this.UpdateFromStockApi.bind(this);
  }

  async UpdateFromStockApi() {
    try {
      this.setState({ loading: true });

      let response;

      await client
        .get("/Portfolio/GetPortfolio")
        .then((res) => {
          response = res.data;
        })
        .catch((e) => {
          console.log(e);
        });

      var stocks = response.currentStockPortfolio.stocks;
      var cryptos = response.currentCryptoPortfolio.cryptocurrencies;
      var portfolioProfit = response.portfolioProfit;

      var cryptoArray = [];

      for (let crypto in cryptos) {
        cryptoArray.push(cryptos[crypto]);
      }

      stocks = JSON.parse(JSON.stringify(stocks), (key, value) =>
        typeof value === "number" && key !== "amount" ? value.toFixed(2) : value
      );

      var cleanedStocks = [];

      for (let stock in stocks) {
        var newItem = JSON.parse(
          JSON.stringify(stocks[stock]),
          (key, value) => {
            return typeof value === "number" ? value.toFixed(3) : value;
          }
        );
        cleanedStocks.push(newItem);
      }

      console.log(cryptoArray);

      this.setState({
        portfolioData: portfolioProfit,
        stockPortfolio: cleanedStocks,
        cryptoPortfolio: cryptoArray,
        loading: false,
      });
    } catch (e) {
      console.log(e);
    }
  }

  async componentDidMount() {
    this.UpdateFromStockApi();
  }

  async logout() {
    localStorage.removeItem("token");
  }

  render() {
    if (this.state.loading) {
      return (
        <div className="App">
          <div>
            <h1 className="loading-center row">🚀</h1>
            <h5 style={{ marginTop: "10px" }}>...Getting Portfolio Data...</h5>
          </div>
        </div>
      );
    }

    return (
      <div className="App">
        <AppHeader />
        <div
          style={{
            display: "flex",
            justifyContent: "space-around",
            flexWrap: "wrap",
          }}
        >
          <div className="col-lg-4 portfolio-summary">
            <div
              style={{
                border: "5px solid black",
                marginTop: "20PX",
                backgroundColor: "white",
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
            <br />
            <AddCryptoModal
              CurrentCryptoPortfolio={this.state.cryptoPortfolio}
              PortfolioData={this.state.portfolioData}
              Update={this.UpdateFromStockApi}
            />
            <br />
            <div className="portfolio-chart cool-shadow">
              <Chart />
            </div>
          </div>
          <div class="col-lg-7">
            <div className="cool-shadow stock-summary instrument-container">
              <StocksContainer
                CurrentStockPortfolio={this.state.stockPortfolio}
                PortfolioData={this.state.portfolioData}
                Update={this.UpdateFromStockApi}
              />
            </div>
            <div className="cool-shadow stock-summary instrument-container">
              <CryptoContainer
                CurrentCryptoPortfolio={this.state.cryptoPortfolio}
                Update={this.UpdateFromStockApi}
              />
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default Home;
