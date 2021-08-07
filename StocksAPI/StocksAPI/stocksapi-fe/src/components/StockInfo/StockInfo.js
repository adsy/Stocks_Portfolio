import axios from "axios";
import React, { useEffect, useState } from "react";
import { useLocation, useParams } from "react-router";
import { Constants } from "../../constants/Constants";
import StockChart from "../Chart/StockChart";
import AppHeader from "../Header/AppHeader";
import NewsContainer from "../NewsContainer/NewsContainer";
import FinancialsContainer from "./FinancialsContainer/FinancialsContainer";
import StockPurchaseList from "./PurchaseList/StockPurchaseList";

const StockInfo = () => {
  const [stockTimeData, setStockTimeData] = useState([]);
  const [stockSummaryData, setStockSummaryData] = useState({});
  const [stockNewsData, setStockNewsData] = useState([]);
  const [loading, setLoading] = useState(true);
  const { stock } = useParams();
  let stockData = useLocation();

  const chartURL = Constants.getStockChartData.replace("{id}", stock);
  const summaryURL = Constants.getStockSummaryData.replace("{id}", stock);
  const newsURL = Constants.getStockNewsData.replace("{id}", stock);

  const GetChartData = () => {
    axios
      .get(chartURL)
      .then((res) => res.data.data)
      .then((data) => {
        setStockTimeData(data.chartDataList);
      });
  };

  const GetSummaryData = () => {
    axios
      .get(summaryURL)
      .then((res) => res.data)
      .then((data) => {
        setStockSummaryData(data);
      });
  };

  const GetNewsData = () => {
    axios
      .get(newsURL)
      .then((res) => res.data)
      .then((data) => {
        setStockNewsData(data);
        setLoading(false);
      });
  };

  useEffect(() => {
    GetChartData();
    GetSummaryData();
    GetNewsData();
  }, []);

  if (loading) return <h1 className="loading-center row">🚀</h1>;

  return (
    <div>
      <AppHeader />
      <div
        style={{
          display: "flex",
          justifyContent: "space-around",
          flexWrap: "wrap",
        }}
      >
        <div className="col-xl-6">
          <div
            style={{
              marginTop: "20px",
              marginLeft: "3.85vw",
              backgroundColor: "rgba(255, 255, 255, .85)",
              width: "80%",
            }}
            className="col-lg-12"
          >
            <h1 style={{ fontSize: "36px", opacity: "1" }}>
              {stockSummaryData.fullName}
            </h1>
          </div>
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <div
              style={{ marginTop: "15px", width: "90%" }}
              className="portfolio-chart cool-shadow"
            >
              <StockChart stockTimeData={stockTimeData} />
            </div>
            <div
              style={{
                border: "5px solid black",
                marginTop: "20PX",
                backgroundColor: "white",
                borderRadius: "2px 16px",
                justifyContent: "center",
                display: "flex",
                width: "90%",
              }}
            >
              <FinancialsContainer
                stock={stock}
                stockSummaryData={stockSummaryData}
                stockData={stockData.state.stock}
              />
            </div>
          </div>
        </div>

        <div
          className="col-xl-6"
          style={{ display: "flex", flexDirection: "column" }}
        >
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <div className="cool-shadow stock-summary instrument-container">
              <h4>Purchases</h4>
              <StockPurchaseList stockList={stockData.state.stock.stockList} />
            </div>{" "}
            <div className="cool-shadow stock-summary instrument-container">
              <h2>News</h2>
              <NewsContainer stockNewsData={stockNewsData} />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default StockInfo;
