import React from "react";
import { Pie } from "react-chartjs-2";

const CgtPieChart = ({ capitalLosses, capitalGains }) => {
  const data = {
    labels: ["Capital Gains", "Capital Losses"],
    datasets: [
      {
        label: "CGT Payable",
        data: [capitalGains, capitalLosses],
        backgroundColor: ["rgba(85, 197, 17, 0.2)", "rgba(255, 99, 132, 0.2)"],
        borderColor: ["rgba(85, 197, 17, 1)", "rgba(255, 99, 132, 1)"],
        borderWidth: 1,
      },
    ],
  };

  return <Pie data={data} />;
};

export default CgtPieChart;
