import axios from "axios";
import React, { useState, useEffect } from "react";
import {
  CartesianGrid,
  Legend,
  Line,
  LineChart,
  ResponsiveContainer,
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
      });

    return valueArray.data;
  };

  useEffect(() => {
    var key = realData().then((obj) => {
      setData(obj);
    });
  }, []);

  return (
    <ResponsiveContainer width="100%" height={300}>
      <LineChart data={data} margin={{ top: 25, right: 20 }}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="timeString" />
        <YAxis
          type="number"
          tickFormatter={(value) => "$" + value.toFixed(0)}
        />
        <Tooltip formatter={(value) => "$" + value.toFixed(2)} />
        <Line type="monotone" dataKey="portfolioTotal" stroke="#000000" />
      </LineChart>
    </ResponsiveContainer>
  );
};

export default Chart;
