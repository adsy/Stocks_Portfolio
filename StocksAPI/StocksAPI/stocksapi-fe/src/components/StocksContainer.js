import "bootstrap/dist/css/bootstrap.min.css";
import React, { Component } from "react";
import { Constants } from "../constants/Constants";
import Container from "react-bootstrap/Container";
import Row from "react-bootstrap/Row";
import { Button } from "@material-ui/core";
import SellStockModal from "./SellStock/SellStockModal";

const StocksContainer = ({ CurrentStockPortfolio, PortfolioData, Update }) => {
  return (
    <div>
      {CurrentStockPortfolio.map((stock, index) => (
        <div
          className="stock-container-css"
          style={{ marginTop: "10px", marginBottom: "10px" }}
        >
          <div className="col-xl-10">
            <Row
              style={{
                width: "100%",
                display: "flex",
                justifyContent: "center",
              }}
              className="col-xs-12"
            >
              <h3 style={{ paddingTop: "5px" }}>{stock.name} 🚀</h3>
            </Row>
            <Row
              style={{
                width: "100%",
                display: "flex",
                justifyContent: "space-around",
                paddingLeft: "10px",
              }}
            >
              <div style={{ width: "25%" }}>
                <h6 className="stock-column-names">Amount</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6 className="stock-column-names">Current Price</h6>
              </div>
              <div className="hide-when-small" style={{ width: "25%" }}>
                <h6 className="stock-column-names">Current Value</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6 className="stock-column-names">Total Profit</h6>
              </div>
            </Row>
            <Row
              style={{
                width: "100%",
                display: "flex",
                justifyContent: "space-around",
                paddingLeft: "10px",
              }}
            >
              <div style={{ width: "25%" }}>
                <h6> {stock.amount}</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6> ${stock.currentPrice}</h6>
              </div>
              <div className="hide-when-small" style={{ width: "25%" }}>
                <h6> ${stock.currentValue}</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6> ${stock.profit}</h6>
              </div>
            </Row>
          </div>
          <div className="col-xl-1">
            <SellStockModal stock={stock} Update={Update} />
          </div>
        </div>
      ))}
    </div>
  );
};

export default StocksContainer;
