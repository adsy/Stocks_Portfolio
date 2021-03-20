import 'bootstrap/dist/css/bootstrap.min.css';
import React, { Component } from "react";
import { Constants } from "../constants/Constants";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';

const StocksContainer = ({ CurrentStockPortfolio }) => {
    return (<div >
        {
            CurrentStockPortfolio.map((stock, index) => (
                <div className="stock-container-css" style={{marginTop: "10px", marginBottom: "10px"}}>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "center"}} className="col-xs-12">
                        <h3 style={{ paddingTop: '5px' }}>{stock.name} 🚀</h3>
                    </Row>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "space-around", paddingLeft: "10px" }}>
                        <div style={{width:"25%"}}>
                            <h6 className="stock-column-names">Amount</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6 className="stock-column-names">Current Price</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6 className="stock-column-names">Current Value</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6 className="stock-column-names">Total Profit</h6>
                        </div>
                    </Row>
                    <Row style={{ width: "100%", display: "flex", justifyContent: "space-around", paddingLeft: "10px"  }}>
                        <div style={{ width: "25%" }}>
                            <h6> {stock.amount}</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6> ${(stock.currentPrice)}</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6> ${stock.currentValue}</h6>
                        </div>
                        <div style={{ width: "25%" }}>
                            <h6> ${stock.profit}</h6>
                        </div>
                    </Row>

                </div>
            ))
        }
    </div>);
}

export default StocksContainer;

