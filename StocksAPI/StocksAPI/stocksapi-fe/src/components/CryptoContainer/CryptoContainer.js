import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import Row from "react-bootstrap/Row";
import SellStockModal from "../SellStock/SellStockModal";

const CryptoContainer = ({ CurrentStockPortfolio, Update }) => {
  return (
    <div>
      {CurrentStockPortfolio.map((stock, index) => (
        <div
          className="stock-container-css"
          style={{ marginTop: "10px", marginBottom: "10px" }}
          key={index}
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
                <h6> {stock.totalAmount}</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6> ${stock.currentPrice}</h6>
              </div>
              <div className="hide-when-small" style={{ width: "25%" }}>
                <h6> ${stock.currentValue}</h6>
              </div>
              <div style={{ width: "25%" }}>
                <h6> ${stock.totalProfit}</h6>
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

export default CryptoContainer;
