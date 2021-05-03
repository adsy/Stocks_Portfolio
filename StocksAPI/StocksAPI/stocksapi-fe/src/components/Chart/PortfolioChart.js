import axios from "axios";
import React, { useState, useEffect } from "react";
import {
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { Constants } from "../../constants/Constants";

const Chart = () => {
  const [data, setData] = useState([]);

  const realData = async () => {
    var valueArray = await axios
      .get(`${Constants.portfolioValueList}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      })
      .catch((error) => {
        console.log(error);
      })
      .then((valueArray) => {
        console.log(valueArray);
      });

    return valueArray;
  };

  useEffect(() => {
    var log = realData();

    // setTimeout(() => {
    //   setData();
    // }, 1000);

    // setData(realData());
  }, []);

  return (
    <LineChart
      width={400}
      height={300}
      data={data}
      margin={{ top: 25, right: 40 }}
      style={{ backgroundColor: "white" }}
    >
      <CartesianGrid strokeDasharray="3 3" />
      <XAxis dataKey="name" />
      <YAxis />
      <Tooltip />
      <Legend />
      <Line type="monotone" dataKey="portfolioTotal" stroke="#000000" />
    </LineChart>
  );
};

export default Chart;
