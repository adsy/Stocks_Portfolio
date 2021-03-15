import React from "react";
import Card from "react-bootstrap/Card"
import AmountComponent from "./Amount"

{/* <span class="stockAmount-text">{stock.amount}</span> of {stock.name} @ ${stock.currentPrice} */ }


const StockProfile = ({ stock }) => {
    return (

        <div >
            <AmountComponent Amount={stock.amount} />
        </div>
    )
}

export default StockProfile;