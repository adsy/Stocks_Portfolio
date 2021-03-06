import axios from "axios";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";
import { useLocation } from "react-router-dom";
import { Constants } from "../../constants/Constants";
import StockChart from "../Chart/StockChart";
import CryptoPurchaseList from "./PurchaseInfo/CryptoPurchaseList";
import CryptoFinancialsContainer from "./CryptoFinancialsContainer/CryptoFinancialsContainer";
import AppHeader from "../Header/AppHeader";

const CryptoInfo = () => {
  const [cryptoTimeData, setCryptoTimeData] = useState([]);
  const [cryptoSummaryData, setCryptoSummaryData] = useState({});
  const [loading, setLoading] = useState(true);
  let cryptoData = useLocation();

  const chartURL = Constants.getCryptoChartData.replace(
    "{id}",
    cryptoData.state.crypto.fullName.toLowerCase()
  );
  const summaryURL = Constants.getCryptoSummaryData.replace(
    "{id}",
    cryptoData.state.crypto.fullName.toLowerCase()
  );

  useEffect(async () => {
    const [summaryResponse, chartResponse] = await Promise.all([
      axios.get(summaryURL),
      axios.get(chartURL),
    ]);

    setCryptoSummaryData(summaryResponse.data);
    setCryptoTimeData(chartResponse.data.priceChartData);
    setLoading(false);
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
              style={{ marginTop: "15px", width: "90%" }}
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
                width: "90%",
              }}
            >
              <CryptoFinancialsContainer
                cryptoSummaryData={cryptoSummaryData}
                cryptoData={cryptoData.state.crypto}
              />
            </div>
          </div>
        </div>

        <div className="col-xl-6">
          <div
            style={{
              display: "flex",
              flexDirection: "column",
              alignItems: "center",
            }}
          >
            <div className="cool-shadow stock-summary instrument-container">
              <h4>Purchases</h4>
              <CryptoPurchaseList
                cryptoList={cryptoData.state.crypto.coinList}
              />
            </div>{" "}
            <div className="cool-shadow stock-summary instrument-container">
              <h2>News</h2>
              {/* <NewsContainer stockNewsData={stockNewsData} /> */}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CryptoInfo;
