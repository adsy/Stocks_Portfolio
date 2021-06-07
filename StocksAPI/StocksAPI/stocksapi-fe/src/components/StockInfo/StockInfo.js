import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Constants } from "../../constants/Constants";

const StockInfo = () => {
  const [stockData, setStockData] = useState({});
  const { stock } = useParams();

  useEffect(() => {
    axios
      .get(`${Constants.getStockData}`)
      .then((res) => console.log(res))
      .then((res) => {
        setStockData(res);
        console.log(res);
      });
  }, []);

  return <div>{stock}</div>;
};

export default StockInfo;
