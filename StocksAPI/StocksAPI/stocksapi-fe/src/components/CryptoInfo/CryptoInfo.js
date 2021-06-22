import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { useParams } from "react-router";
import { useLocation } from "react-router-dom";
import { Constants } from "../../constants/Constants";
import StockPurchaseList from "../StockInfo/PurchaseList/StockPurchaseList";
import StockChart from "../Chart/StockChart";
import CryptoPurchaseList from "./PurchaseInfo/CryptoPurchaseList";

const CryptoInfo = () => {
  const [cryptoTimeData, setCryptoTimeData] = useState([]);
  const [loading, setLoading] = useState(true);
  let cryptoData = useLocation();

  console.log(cryptoData.state.crypto.fullName.toLowerCase());

  const chartURL = Constants.getCryptoChartData.replace(
    "{id}",
    cryptoData.state.crypto.fullName.toLowerCase()
  );
  const GetChartData = () => {
    axios
      .get(chartURL)
      .then((res) => res.data)
      .then((data) => {
        setCryptoTimeData(data.priceChartData);
        setLoading(false);
      });
  };

  console.log(cryptoData);

  useEffect(() => {
    GetChartData();
  }, []);

  if (loading) return <h1 className="loading-center row">🚀</h1>;

  return (
    <div>
      <div style={{ display: "flex", justifyContent: "space-around" }}>
        <div
          className="col-lg-5"
          style={{
            marginLeft: "20px",
          }}
        >
          <div
            style={{
              marginTop: "20px",
              marginLeft: "3.85vw",
              backgroundColor: "rgba(255, 255, 255, .85)",
              width: "80%",
            }}
            className="col-lg-10"
          >
            <h1 style={{ fontSize: "36px", opacity: "1" }}>
              {cryptoData.state.crypto.fullName}
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
              style={{ marginTop: "15px", width: "80%" }}
              className="portfolio-chart cool-shadow"
            >
              <StockChart stockTimeData={cryptoTimeData} />
            </div>
            <div
              style={{
                border: "5px solid black",
                marginTop: "20PX",
                backgroundColor: "white",
                borderRadius: "2px 16px",
                justifyContent: "center",
                display: "flex",
                width: "80%",
              }}
            >
              {/* <FinancialsContainer
                  stock={stock}
                  stockSummaryData={stockSummaryData}
                  stockData={stockData.state.stock}
                /> */}
            </div>
          </div>
        </div>

        <div
          className="col-lg-6"
          style={{ display: "flex", flexDirection: "column" }}
        >
          <div className="cool-shadow stock-summary instrument-container">
            <h4>Purchases</h4>
            <CryptoPurchaseList cryptoList={cryptoData.state.crypto.coinList} />
          </div>{" "}
          <div className="cool-shadow stock-summary instrument-container">
            <h2>News</h2>
            {/* <NewsContainer stockNewsData={stockNewsData} /> */}
          </div>
        </div>
      </div>
    </div>
  );
};

export default CryptoInfo;
