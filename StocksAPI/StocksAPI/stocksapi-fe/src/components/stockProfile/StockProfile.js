import React from "react";
import Card from "react-bootstrap/Card"

const StockProfile = ({ stock }) => {
    return (
        <p >
            {/* <span class="stockAmount-text">{stock.amount}</span> of {stock.name} @ ${stock.currentPrice} */}

            <span class="stockAmount-text">25</span> of BB @ $25
        </p>
    )
}

export default StockProfile;