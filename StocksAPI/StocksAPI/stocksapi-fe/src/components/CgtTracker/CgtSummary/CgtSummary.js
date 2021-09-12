const CgtSummary = ({ capitalGains, capitalLosses, cgtPayable }) => {
  return (
    <div className="container-css" style={{ marginLeft: "20px" }}>
      <h3
        className="portfolio-summary-resize"
        style={{ paddingBottom: "10px", marginTop: "-15px" }}
      >
        CGT Summary
      </h3>

      <h6 style={{ paddingBottom: "10px" }}>
        Capital Gains: ${capitalGains.toFixed(2)}
      </h6>
      <h6 style={{ paddingBottom: "10px" }}>
        Capital Losses: ${capitalLosses.toFixed(2)}
      </h6>
      <h6 style={{ paddingBottom: "10px" }}>
        <span className={cgtPayable > 0 ? "green-text" : "red-text"}>
          CGT Payable: ${cgtPayable.toFixed(2)}
        </span>
      </h6>
    </div>
  );
};

export default CgtSummary;
