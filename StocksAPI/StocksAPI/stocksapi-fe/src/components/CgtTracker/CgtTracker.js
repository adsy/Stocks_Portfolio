import axios from "axios";
import { useEffect, useState } from "react";
import { Constants } from "../../constants/Constants";
import AppHeader from "../Header/AppHeader";
import CgtPieChart from "./CgtPieChart/CgtPieChart";
import CgtSalesList from "./CgtSalesList/CgtSalesList";
import CgtSummary from "./CgtSummary/CgtSummary";

const CgtTracker = () => {
  const [cgtData, setCgtData] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    axios.get(Constants.getCgtData).then((data) => {
      setCgtData(data.data);
      setLoading(false);
      console.log(data.data);
    });
  }, []);

  if (loading) {
    return <div>loading...</div>;
  }

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
            <CgtSummary
              capitalGains={cgtData.capitalGains}
              capitalLosses={cgtData.capitalLosses}
              cgtPayable={cgtData.cgtPayable}
            />
          </div>
          <div className="mt-4">
            <CgtPieChart
              capitalGains={cgtData.capitalGains}
              capitalLosses={cgtData.capitalLosses}
            />
          </div>
        </div>

        <div className="col-md-6">
          <CgtSalesList salesList={cgtData.salesList} />
        </div>
      </div>
    </div>
  );
};

export default CgtTracker;
