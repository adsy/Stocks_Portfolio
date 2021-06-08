import React, { useEffect } from "react";
import { Row } from "react-bootstrap";

const FinancialsContainer = ({ stockSummaryData, stockData }) => {
  return (
    <div className="container-css">
      <h3 className="portfolio-summary-resize" style={{ marginTop: "-5px" }}>
        Financial Summary
      </h3>
      <hr className="striped-border" />
      <Row>
        <div
          className="col-lg-6"
          style={{ fontSize: "16px", marginBottom: "-3px" }}
        >
          % CHANGE:{" "}
          <span
            className={
              stockSummaryData.percentChange > 0 ? "green-text" : "red-text"
            }
            style={{ fontWeight: "1000", float: "right" }}
          >
            {(stockSummaryData.percentChange * 100).toFixed(2)}%
          </span>
        </div>
        <div className="col-lg-6" style={{ fontSize: "16px" }}>
          CURRENT PRICE:
          <span style={{ float: "right" }}>${stockData.currentPrice}</span>
        </div>
      </Row>

      <hr className="striped-border" />
      <div
        className="row"
        style={{
          display: "flex",
          justifyContent: "space-between",
        }}
      >
        <div className="col-lg-6">
          Avg Price:{" "}
          <span style={{ float: "right" }}>${stockData.avgPrice}</span>
        </div>
        <div className="col-lg-6">
          Total Owned:{" "}
          <span style={{ float: "right" }}>{stockData.totalAmount}</span>
        </div>
      </div>
      <hr className="striped-border" />
      <div className="row">
        <div className="col-lg-6">
          Day Return:
          <span
            className={
              stockSummaryData.percentChange > 0 ? "green-text" : "red-text"
            }
            style={{ float: "right", fontWeight: "1000" }}
          >
            $
            {(stockData.totalAmount * stockSummaryData.percentChange).toFixed(
              2
            )}
          </span>
        </div>
        <div className="col-lg-6">
          <div>
            Total Return:
            <span
              className={
                stockSummaryData.percentChange > 0 ? "green-text" : "red-text"
              }
              style={{ float: "right", fontWeight: "1000" }}
            >
              ${stockData.totalProfit}
            </span>
          </div>
        </div>
      </div>
      <hr className="striped-border" />
      <div className="row">
        <div className="col-lg-6">
          <div>
            Open:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.openPrice.toFixed(2)}
            </span>
          </div>
          <div>
            High:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.dayHigh.toFixed(2)}
            </span>
          </div>
          <div>
            Low:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.dayLow.toFixed(2)}
            </span>
          </div>
        </div>
        <div className="col-lg-6" style={{ marginBottom: "10px" }}>
          <div>
            52w High:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.yearHigh.toFixed(2)}
            </span>
          </div>
          <div>
            52w Low:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.yearLow.toFixed(2)}
            </span>
          </div>
          <div>
            Mkt Cap:{" "}
            <span style={{ float: "right" }}>
              ${stockSummaryData.marketCap}
            </span>
          </div>
        </div>
      </div>

      <hr className="striped-border" style={{ marginBottom: "10px" }} />
    </div>
  );
};

export default FinancialsContainer;
