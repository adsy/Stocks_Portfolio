import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import Row from "react-bootstrap/Row";
import { Link } from "react-router-dom";
import SellStockModal from "../SellStock/SellStockModal";
import SellCryptoModal from "../SellCrypto/SellCryptoModal";

const CryptoContainer = ({ CurrentCryptoPortfolio, Update }) => {
  return (
    <div>
      {CurrentCryptoPortfolio.map((crypto, index) => (
        <div
          className="stock-container-css"
          style={{ marginTop: "10px", marginBottom: "10px" }}
          key={index}
        >
          <div className="col-xl-10">
            <Link to={`crypto/${crypto.coinName}`}>
              <Row
                style={{
                  width: "100%",
                  display: "flex",
                  justifyContent: "center",
                }}
                className="col-xs-12"
              >
                <h3 style={{ paddingTop: "5px" }}>{crypto.fullName} 🚀</h3>
              </Row>
              <Row
                style={{
                  width: "100%",
                  display: "flex",
                  justifyContent: "space-around",
                  paddingLeft: "10px",
                }}
              >
                <div style={{ width: "20%" }}>
                  <h6 className="stock-column-names">Amount</h6>
                </div>
                <div style={{ width: "20%" }}>
                  <h6 className="stock-column-names">Current Price</h6>
                </div>
                <div className="hide-when-small" style={{ width: "20%" }}>
                  <h6 className="stock-column-names">Current Value</h6>
                </div>
                <div className="hide-when-small" style={{ width: "20%" }}>
                  <h6 className="stock-column-names">Average Price</h6>
                </div>
                <div style={{ width: "20%" }}>
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
                <div style={{ width: "20%" }}>
                  <h6> {crypto.totalAmount}</h6>
                </div>
                <div style={{ width: "250" }}>
                  <h6> ${crypto.currentPrice}</h6>
                </div>
                <div className="hide-when-small" style={{ width: "20%" }}>
                  <h6> ${crypto.currentValue}</h6>
                </div>
                <div className="hide-when-small" style={{ width: "20%" }}>
                  <h6> ${crypto.avgPrice}</h6>
                </div>
                <div style={{ width: "20%" }}>
                  <h6> ${crypto.totalProfit}</h6>
                </div>
              </Row>
            </Link>
          </div>
          <div className="col-xl-1">
            <SellCryptoModal crypto={crypto} Update={Update} />
          </div>
        </div>
      ))}
    </div>
  );
};

export default CryptoContainer;
