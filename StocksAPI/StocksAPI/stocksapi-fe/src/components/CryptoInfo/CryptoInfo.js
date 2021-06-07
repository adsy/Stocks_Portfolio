import React from "react";
import { useParams } from "react-router";

const CryptoInfo = () => {
  const { crypto } = useParams();

  return <div>{crypto}</div>;
};

export default CryptoInfo;
