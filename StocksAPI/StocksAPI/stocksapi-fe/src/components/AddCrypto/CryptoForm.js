import { Button } from "@material-ui/core";
import React, { Component } from "react";
import axios from "axios";
import { Constants } from "../../constants/Constants";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

class CryptoForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      name: "",
      purchaseDate: null,
      formattedDate: "",
      price: 0,
      amount: 0,
      totalCost: 0,
      visible: true,
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleDate = this.handleDate.bind(this);
    this.handleInterfaceUpdate = this.handleInterfaceUpdate.bind(this);
  }

  async AddCyrptoData() {
    try {
      var result = axios({
        method: "post",
        url: `${Constants.addCrypto}`,
        data: {
          name: `${this.state.name}`,
          purchaseDate: `${this.state.formattedDate}`,
          purchasePrice: `${this.state.price}`,
          amount: `${this.state.amount}`,
          totalCost: `${this.state.totalCost}`,
        },
      });

      return result;
    } catch (error) {
      console.log(error);
    }
  }

  handleChange(event) {
    let nam = event.target.name;
    let val = event.target.value;
    this.setState({ [nam]: val });
  }

  handleInterfaceUpdate(state) {
    let data = this.props.PortfolioData;

    console.log(this.props.Current);

    let portfolio = this.props.CurrentCryptoPortfolio.map((crypto, index) => {
      if (crypto.name === this.state.name) {
        let updateAmount =
          parseInt(this.state.amount) + parseInt(crypto.amount);

        let updatePrice = (
          updateAmount * parseFloat(crypto.currentPrice)
        ).toFixed(3);

        let updateProfit = (
          parseFloat(crypto.profit) +
          (parseFloat(crypto.currentPrice) - parseFloat(this.state.price)) *
            parseInt(this.state.amount)
        ).toFixed(3);

        crypto.amount = updateAmount;
        crypto.currentValue = updatePrice;
        crypto.profit = updateProfit;

        data.currentTotal = (
          parseFloat(this.props.PortfolioData.currentTotal) +
          parseFloat(crypto.currentPrice) * parseInt(this.state.amount)
        ).toFixed(2);

        data.purchaseTotal = (
          parseFloat(this.props.PortfolioData.purchaseTotal) +
          parseFloat(this.state.totalCost)
        ).toFixed(2);
      }
      return crypto;
    });

    return { data: data, portfolio: portfolio };
  }

  async handleSubmit(event) {
    let totalCost = this.state.price * this.state.amount;

    await this.setState({ totalCost: totalCost });

    var result = await this.AddCyrptoData();

    if (result.status === 200) {
      // update interface
      let propsObj = this.handleInterfaceUpdate(this.state);
      // close the modal
      this.props.handler();
      // update the state in parent component
      await this.props.Update(propsObj.portfolio, propsObj.data);
    }
  }

  handleDate(date) {
    let formatted_date =
      date.getFullYear() +
      "-" +
      (date.getMonth() + 1) +
      "-" +
      date.getDate() +
      " 00:00:00.000000";

    this.setState({ purchaseDate: date, formattedDate: formatted_date });
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
          <h6>Coin Name</h6>
        </label>
        <input type="text" name="name" onChange={this.handleChange} />
        <label style={{ marginTop: "10px" }}>
          <h6>Purchase Date</h6>
        </label>
        <DatePicker
          selected={this.state.purchaseDate}
          onChange={(date) => this.handleDate(date)}
        />
        <label style={{ marginTop: "10px" }}>
          <h6>Purchase Price</h6>
        </label>
        <input type="number" name="price" onChange={this.handleChange} />
        <label style={{ marginTop: "10px" }}>
          <h6>Amount</h6>
        </label>
        <input type="number" name="amount" onChange={this.handleChange} />
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

export default CryptoForm;
