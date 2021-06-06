import React from "react";
import { useParams } from "react-router";

const StockInfo = () => {
  const { stock } = useParams();

  return <div>{stock}</div>;
};

export default StockInfo;
