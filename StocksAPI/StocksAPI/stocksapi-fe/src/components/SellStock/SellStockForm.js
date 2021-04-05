import { Button } from "@material-ui/core";
import React, { Component } from "react";
import axios from "axios";
import { Constants } from "../../constants/Constants";

class SellStockForm extends Component {
  constructor(props) {
    super(props);
    this.state = { value: "", visible: true };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async SellStockData() {
    try {
      axios({
        method: "post",
        url: `${Constants.sellStock}`,
        data: {
          name: ``,
          sellPrice: "",
          amount: 0,
        },
      });
    } catch (error) {
      console.log(error);
      return [];
    }
  }

  handleChange(event) {
    this.setState({ value: event.target.value });
  }

  handleSubmit(event) {
    this.props.handler();

    this.SellStockData();
  }

  render() {
    return (
      <form
        style={{
          display: "flex",
          justifyContent: "center",
          flexDirection: "column",
        }}
      >
        <label style={{ width: "100%" }}>
          <h6>Ticker</h6>
        </label>
        <input type="text" name="name" value={this.props.stock.name} />
        <label style={{ marginTop: "10px" }}>
          <h6>Amount</h6>
        </label>
        <input type="number" name="name" />
        <label style={{ marginTop: "10px" }}>
          <h6>Sell Price</h6>
        </label>
        <input type="number" name="name" />
        <br />
        <Button
          onClick={this.SellStockData}
          variant="contained"
          value="Submit"
          color="secondary"
        >
          Submit
        </Button>
      </form>
    );
  }
}

export default SellStockForm;
