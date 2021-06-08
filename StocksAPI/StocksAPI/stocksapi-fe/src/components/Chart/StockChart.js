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

const StockChart = ({ stockTimeData }) => {
  return (
    <ResponsiveContainer width="100%" height={300}>
      <LineChart data={stockTimeData} margin={{ top: 25, right: 20 }}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="time" />
        <YAxis
          type="number"
          tickFormatter={(value) => "$" + value.toFixed(0)}
        />
        <Tooltip formatter={(value) => "$" + value.toFixed(4)} />
        <Line type="monotone" dataKey="price" stroke="#000000" dot={false} />
      </LineChart>
    </ResponsiveContainer>
  );
};

export default StockChart;
