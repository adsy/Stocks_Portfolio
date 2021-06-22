import { Button } from "@material-ui/core";
import React, { Component } from "react";
import axios from "axios";
import { Constants } from "../../constants/Constants";

class SellCryptoForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      name: this.props.crypto.coinName,
      sellPrice: 0,
      amount: 0,
      visible: true,
      errorMessage: false,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  async SellCryptoData() {
    try {
      var result = await axios({
        method: "post",
        url: `${Constants.sellCrypto}`,
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
    var result = await this.SellCryptoData();

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
        <h3>{this.state.name}</h3>
        {this.state.errorMessage ? (
          <span
            style={{
              marginTop: "8px",
              textAlign: "center",
            }}
          >
            There was an error submitting your request.
          </span>
        ) : (
          <span></span>
        )}
        <label style={{ marginTop: "15px" }}>
          <h6>Amount</h6>
        </label>
        <input
          style={{ border: "2px black solid" }}
          type="number"
          name="amount"
          onChange={this.handleChange}
        />
        <label style={{ marginTop: "15px" }}>
          <h6>Sell Price</h6>
        </label>
        <input
          style={{ border: "2px black solid" }}
          type="number"
          name="sellPrice"
          onChange={this.handleChange}
        />
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

export default SellCryptoForm;
