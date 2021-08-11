const CgtSummary = () => {
  return (
    <div className="container-css" style={{ marginLeft: "20px" }}>
      <h3
        className="portfolio-summary-resize"
        style={{ paddingBottom: "10px", marginTop: "-15px" }}
      >
        CGT Summary
      </h3>

      <h6 style={{ paddingBottom: "10px" }}>Capital Gains: ${}</h6>
      <h6 style={{ paddingBottom: "10px" }}>Capital Losses: ${}</h6>
      <h6 style={{ paddingBottom: "10px" }}>CGT Payable: ${}</h6>
    </div>
  );
};

export default CgtSummary;
