import React from "react";
import Card from "react-bootstrap/Card"

const StockProfile = ({ stock }) => {
    return (
        <p >
            <span class="stockAmount-text">{stock.amount}</span> of {stock.name} @ ${stock.currentPrice}
        </p>
    )
}

export default StockProfile;