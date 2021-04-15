import React from "react";

const DetailsContainer = ({ PortfolioProfit }) => {
  return (
    <div className="container-css">
      <h3
        className="portfolio-summary-resize"
        style={{ paddingBottom: "10px", marginTop: "-15px" }}
      >
        Portfolio Summary
      </h3>

      <h6 style={{ paddingBottom: "10px" }}>
        Current Value: ${PortfolioProfit.currentTotal}
      </h6>
      <h6 style={{ paddingBottom: "10px" }}>
        Purchase Value: ${PortfolioProfit.purchaseTotal}
      </h6>
      <h6 style={{ paddingBottom: "10px" }}>
        Profit: $
        {(PortfolioProfit.currentTotal - PortfolioProfit.purchaseTotal).toFixed(
          2
        )}
      </h6>
    </div>
  );
};

export default DetailsContainer;
