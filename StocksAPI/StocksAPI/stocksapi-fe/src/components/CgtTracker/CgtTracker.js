import AppHeader from "../Header/AppHeader";
import CgtSummary from "./CgtSummary/CgtSummary";

const CgtTracker = () => {
  return (
    <div>
      <AppHeader />
      <div
        style={{
          display: "flex",
          flexWrap: "wrap",
          justifyContent: "space-around",
        }}
      >
        <div className="col-lg-4 portfolio-summary">
          <div
            style={{
              border: "5px solid black",
              marginTop: "20PX",
              backgroundColor: "white",
              borderRadius: "2px 16px",
              justifyContent: "center",
              display: "flex",
            }}
          >
            <CgtSummary />
          </div>
          <div className="cgt-chart">cgt chart</div>
        </div>

        <div className="col-md-6">
          <div className="cgt-sales-list">sales-list</div>
        </div>
      </div>
    </div>
  );
};

export default CgtTracker;
