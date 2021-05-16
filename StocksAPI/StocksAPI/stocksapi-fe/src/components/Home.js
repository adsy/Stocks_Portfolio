import React, { Component } from "react";
import StocksContainer from "./StocksContainer";
import DetailsContainer from "./DetailsContainer";
import AddStockModal from "./AddStock/AddStockModal";
import AddCryptoModal from "./AddCrypto/AddCryptoModal";
import AppHeader from "./Header/AppHeader";
import axios from "axios";
import { Constants } from "../constants/Constants";
import Chart from "./Chart/PortfolioChart";

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
            <div className="row">Getting Stock Data...</div>
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
          <div
            className="col-lg-7 cool-shadow stock-summary"
            style={{
              width: "95%",
              height: "43vh",
              marginTop: "20PX",
              overflowY: "auto",
              borderRadius: "4px 4px",
              paddingLeft: "20px",
              paddingRight: "20px",
              paddingTop: "10px",
              paddingBottom: "10px",
              backgroundColor: "white",
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
