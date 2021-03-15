import React from "react";
import Card from "react-bootstrap/esm/Card";

const AmountComponent = ({ Amount }) => {
    return (
        <Card>
            <Card.Body><span class="stockAmount-text">{Amount}</span></Card.Body>
        </Card>)
}

export default AmountComponent;