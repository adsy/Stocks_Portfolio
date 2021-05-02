import { Button } from "@material-ui/core";
import React, { Component } from "react";
import axios from "axios";
import { Constants } from "../../constants/Constants";

class SellStockForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      name: this.props.stock.name,
      sellPrice: 0,
      amount: 0,
      visible: true,
      errorMessage: false,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async SellStockData() {
    try {
      var result = await axios({
        method: "post",
        url: `${Constants.sellStock}`,
        data: {
          name: `${this.state.name}`,
          sellPrice: `${this.state.sellPrice}`,
          amount: `${this.state.amount}`,
        },
      });

      return result;
    } catch (error) {
      return error;
    }
  }

  handleChange(event) {
    let nam = event.target.name;
    let val = event.target.value;
    this.setState({ [nam]: val });
  }

  async handleSubmit(event) {
    this.setState({ errorMessage: false });
    var result = await this.SellStockData();

    if (result.status === 200) {
      this.props.handler();
      this.props.Update();
    } else {
      this.setState({
        errorMessage: true,
      });
    }
  }

  render() {
    return (
      <form
        style={{
          display: "flex",
          justifyContent: "center",
          flexDirection: "column",
          width: "90%",
        }}
      >
        {this.state.errorMessage ? (
          <span style={{ marginBottom: "20px", textAlign: "center" }}>
            There was an error submitting your request.
          </span>
        ) : (
          <span></span>
        )}
        <label style={{ width: "100%" }}>
          <h6>Ticker</h6>
        </label>
        <input type="text" name="name" value={this.props.stock.name} readOnly />
        <label style={{ marginTop: "10px" }}>
          <h6>Amount</h6>
        </label>
        <input type="number" name="amount" onChange={this.handleChange} />
        <label style={{ marginTop: "8px" }}>
          <h6>Sell Price</h6>
        </label>
        <input type="number" name="sellPrice" onChange={this.handleChange} />
        <br />
        <Button
          type="button"
          onClick={this.handleSubmit}
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
