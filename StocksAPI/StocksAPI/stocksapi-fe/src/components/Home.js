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

      portfolioDataJSON.portfolioProfit = JSON.parse(
        JSON.stringify(portfolioDataJSON.portfolioProfit),
        (key, value) =>
          typeof value === "number" && key !== "amount"
            ? value.toFixed(2)
            : value
      );

      var cleanedStocks = [];

      for (let stock in portfolioDataJSON.currentStockPortfolio) {
        var newItem = JSON.parse(
          JSON.stringify(portfolioDataJSON.currentStockPortfolio[stock]),
          (key, value) => {
            return typeof value === "number" ? value.toFixed(3) : value;
          }
        );
        cleanedStocks.push(newItem);
      }

      portfolioDataJSON.currentStockPortfolio = cleanedStocks;

      console.log(portfolioDataJSON);

      this.setState({
        portfolioData: portfolioDataJSON.portfolioProfit,
        stockPortfolio: portfolioDataJSON.currentStockPortfolio,
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
            <h5 style={{ marginTop: "10px" }}>Getting Stock Data...</h5>
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
              CurrentStockPortfolio={this.state.stockPortfolio}
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
                CurrentStockPortfolio={this.state.stockPortfolio}
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
