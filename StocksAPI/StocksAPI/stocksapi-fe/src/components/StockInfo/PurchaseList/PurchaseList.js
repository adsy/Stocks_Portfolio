import "bootstrap/dist/css/bootstrap.min.css";
import React, { useEffect } from "react";
import Row from "react-bootstrap/Row";

const PurchaseList = ({ stockList }) => {
  useEffect(() => {
    console.log(stockList);
  }, []);

  return (
    <div>
      {stockList.map((stock, index) => (
        <div
          style={{
            marginTop: "10px",
            marginBottom: "10px",
            border: "1px solid black",
            borderRadius: "8px 2px",
          }}
          key={index}
        >
          <div className="col-xl-12" style={{ marginTop: "8px" }}>
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
              <div className="hide-when-small" style={{ width: "20%" }}>
                <h6 className="stock-column-names">Current Value</h6>
              </div>
              <div className="hide-when-small" style={{ width: "20%" }}>
                <h6 className="stock-column-names">Purchase Price</h6>
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
                <h6> {stock.amount}</h6>
              </div>
              <div className="hide-when-small" style={{ width: "20%" }}>
                <h6> ${stock.currentValue}</h6>
              </div>
              <div className="hide-when-small" style={{ width: "20%" }}>
                <h6> ${stock.purchasePrice}</h6>
              </div>
              <div style={{ width: "20%" }}>
                <h6> ${(stock.currentValue - stock.totalCost).toFixed(2)}</h6>
              </div>
            </Row>
          </div>
          <div className="col-xl-1"></div>
        </div>
      ))}
    </div>
    // <div>test</div>
  );
};

export default PurchaseList;
