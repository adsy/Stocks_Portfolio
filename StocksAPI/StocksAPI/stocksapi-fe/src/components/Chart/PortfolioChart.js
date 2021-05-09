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

  const MINUTE_MS = 60000;

  useEffect(() => {
    realData().then((obj) => {
      setData(obj);
    });

    const interval = setInterval(() => {
      console.log("Getting updated portfolioValue");
      realData().then((obj) => {
        setData(obj);
      });
    }, MINUTE_MS * 5);

    return () => clearInterval(interval); // This represents the unmount function, in which you need to clear your interval to prevent memory leaks.
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
        <Line
          type="monotone"
          dataKey="portfolioTotal"
          stroke="#000000"
          dot={false}
        />
      </LineChart>
    </ResponsiveContainer>
  );
};

export default Chart;
