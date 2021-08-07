import React, { useEffect } from "react";
import { Row } from "react-bootstrap";

const CryptoFinancialsContainer = ({ cryptoSummaryData, cryptoData }) => {
  console.log(cryptoSummaryData);
  console.log(cryptoData);
  return (
    <div className="container-css" style={{ height: "100%" }}>
      <div className="col-lg-12">
        <h3 className="portfolio-summary-resize" style={{ marginTop: "-5px" }}>
          Financial Summary
          <span
            style={{
              marginTop: "-6px",
              fontWeight: "1000",
              float: "right",
              borderStyle: "dotted",
              padding: "2px 2px",
            }}
          >
            {cryptoSummaryData.marketCapRank}
          </span>
        </h3>
      </div>
      <hr className="striped-border" />
      <Row style={{ display: "flex", justifyContent: "space-between" }}>
        <div className="col-lg-12" style={{ fontSize: "16px" }}>
          Price:
          <span style={{ float: "right" }}>${cryptoData.currentPrice}</span>
        </div>
      </Row>
      <hr className="striped-border" />
      <Row>
        <div className="col-lg-12" style={{ fontSize: "16px" }}>
          Price Change:
          <span style={{ float: "right" }}>
            {cryptoSummaryData.priceChangePercentage24H.toFixed(4)}%
          </span>
        </div>
      </Row>
      <hr className="striped-border" />
      <Row style={{ display: "flex", justifyContent: "space-between" }}>
        <div className="col-lg-5" style={{ fontSize: "16px" }}>
          24H High:
          <span style={{ float: "right" }}>
            ${cryptoSummaryData.high24H.toFixed(4)}
          </span>
        </div>
        |
        <div className="col-lg-5" style={{ fontSize: "16px" }}>
          24H Low:
          <span style={{ float: "right" }}>
            ${cryptoSummaryData.low24H.toFixed(4)}
          </span>
        </div>
      </Row>
      <hr className="striped-border" style={{ marginBottom: "10px" }} />
      <Row>
        <div className="col-lg-12" style={{ fontSize: "16px" }}>
          Market Cap $ Change 24H:
          <span style={{ float: "right" }}>
            ${cryptoSummaryData.marketCapChange24H.toFixed(4)}
          </span>
        </div>
      </Row>
      <hr className="striped-border" style={{ marginBottom: "10px" }} />
      <Row>
        <div className="col-lg-12" style={{ fontSize: "16px" }}>
          Market Cap % Change 24H:
          <span style={{ float: "right" }}>
            {cryptoSummaryData.marketCapChangePercentage24H.toFixed(4)}%
          </span>
        </div>
      </Row>
      <hr className="striped-border" style={{ marginBottom: "10px" }} />
    </div>

    // </div> <hr className="striped-border" />
    // <div
    //   className="row"
    //   style={{
    //     display: "flex",
    //     justifyContent: "space-between",
    //   }}
    // >
    //   <div className="col-lg-6">
    //     Avg Price:{" "}
    //     <span style={{ float: "right" }}>${stockData.avgPrice}</span>
    //   </div>
    //   <div className="col-lg-6">
    //     Total Owned:{" "}
    //     <span style={{ float: "right" }}>{stockData.totalAmount}</span>
    //   </div>
    // </div>
    // <hr className="striped-border" />
    // <div className="row">
    //   <div className="col-lg-6">
    //     Day Return:
    //     <span
    //       className={
    //         stockData.totalAmount * stockSummaryData.percentChange > 0
    //           ? "green-text"
    //           : "red-text"
    //       }
    //       style={{ float: "right", fontWeight: "1000" }}
    //     >
    //       $
    //       {(stockData.totalAmount * stockSummaryData.percentChange).toFixed(
    //         2
    //       )}
    //     </span>
    //   </div>
    //   <div className="col-lg-6">
    //     <div>
    //       Total Return:
    //       <span
    //         className={stockData.totalProfit > 0 ? "green-text" : "red-text"}
    //         style={{ float: "right", fontWeight: "1000" }}
    //       >
    //         ${stockData.totalProfit}
    //       </span>
    //     </div>
    //   </div>
    // </div>
    // <hr className="striped-border" />
    // <div className="row">
    //   <div className="col-lg-6">
    //     <div>
    //       Open:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.openPrice.toFixed(2)}
    //       </span>
    //     </div>
    //     <div>
    //       High:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.dayHigh.toFixed(2)}
    //       </span>
    //     </div>
    //     <div>
    //       Low:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.dayLow.toFixed(2)}
    //       </span>
    //     </div>
    //   </div>
    //   <div className="col-lg-6" style={{ marginBottom: "10px" }}>
    //     <div>
    //       52w High:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.yearHigh.toFixed(2)}
    //       </span>
    //     </div>
    //     <div>
    //       52w Low:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.yearLow.toFixed(2)}
    //       </span>
    //     </div>
    //     <div>
    //       Mkt Cap:{" "}
    //       <span style={{ float: "right" }}>
    //         ${stockSummaryData.marketCap}
    //       </span>
    //     </div>
    //   </div>
    // </div>
  );
};

export default CryptoFinancialsContainer;
