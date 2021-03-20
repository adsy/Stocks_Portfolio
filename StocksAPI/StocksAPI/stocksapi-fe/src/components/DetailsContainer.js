﻿import React, { Component } from "react";
import Container from "react-bootstrap/esm/Container";
import { Constants } from "../constants/Constants";

const DetailsContainer = ({PortfolioProfit}) => {

    
    return (<div className="container-css" >
        <h3 className="portfolio-summary-resize" style={{ paddingBottom: "10px", marginTop: "-15px"}} >Portfolio Summary</h3>

        <h6 style={{ paddingBottom: "10px" }}>Current Value: ${PortfolioProfit.currentTotal} </h6>
        <h6 style={{ paddingBottom: "10px" }}>Purchase Value: ${PortfolioProfit.purchaseTotal} </h6>
    </div>);
}

export default DetailsContainer;