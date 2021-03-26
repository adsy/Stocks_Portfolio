import { Button } from '@material-ui/core';
import React, { Component } from 'react';
import axios from 'axios';
import {Constants} from '../constants/Constants'

class StockForm extends Component {
    constructor(props) {
        super(props);
        this.state = { value: '', visible:true };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    async AddStockData() {
        try {
            axios({
                method: 'post',
                url: `${Constants.addStock}`,
                data: {
                    name: "string",
                    purchaseDate: "2021-03-25T11:30:56.023Z",
                    purchasePrice: 0,
                    amount: 0,
                    totalCost: 0,
                    country: "string"
                  }
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
        this.props.handler()
        this.AddStockData();
    }

    render() {
        return (
            <form style={{ display: 'flex', justifyContent: 'center', flexDirection: 'column' }}>
                <label style={{ width: '100%' }}>
                    <h6>Ticker</h6>
                </label>
                <input type="text" name="name" />
                <label style={{ marginTop: '10px' }}>
                    <h6>Purchase Date</h6>
                </label>
                <input type="text" name="name" />
                <label style={{ marginTop: '10px' }}>
                    <h6>Purchase Price</h6>
                </label>
                <input type="number" name="name" />
                <label style={{ marginTop: '10px' }}>
                    <h6>Amount</h6>
                </label>
                <input type="number" name="name" />
                <label style={{ marginTop: '10px' }}>
                    <h6>Country</h6>
                </label>
                <input type="text" name="name" />
                <br />
                <Button onClick={this.AddStockData} variant="contained" value="Submit" color='secondary' >
                    Submit
                </Button>
            </form>
        );
    }
}

export default StockForm;