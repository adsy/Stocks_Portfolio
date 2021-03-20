import React, { Component } from "react";
import Container from "react-bootstrap/esm/Container";
import { Constants } from "../constants/Constants";

export default class DetailsContainer extends Component {

    constructor(props) {
        super(props);
    }

    state = { PortfolioData: {}, loading: true };

    async GetPortfolio() {
        try {
            console.log('getting stocks');
            const res = await fetch
                (`${Constants.getPortfolioProfit}`);
            console.log(res)
            return await res.json()
        } catch (error) {
            console.log(error);
            return [];
        }
    }

    async componentDidMount() {
        try {
            //const portfolio = await this.GetPortfolio()

            //const cleanedPortfolio = JSON.parse(JSON.stringify(portfolio), (key, value) =>
            //    typeof value === "number" ? Math.round(value * 100) / 100 : value
            //);

            //Object.assign(cleanedPortfolio, { "profit": cleanedPortfolio.currentTotal - cleanedPortfolio.purchaseTotal });
            //console.log(cleanedPortfolio);
            //this.setState({ portfolio: cleanedPortfolio, loading: false })

            const mockPortfolio = { currentTotal: 1635.2898314092213, purchaseTotal: 1506.307724266056 };

            const cleanedPortfolio = JSON.parse(JSON.stringify(mockPortfolio), (key, value) =>
                typeof value === "number" ? Math.round(value * 100) / 100 : value
            );

            console.log(cleanedPortfolio);

            const timer = await setTimeout(() => {
                console.log('timer done');
                this.setState({
                    PortfolioData: cleanedPortfolio,
                    loading: false
                });
            }, 2000);

        } catch (e) {
            console.log(e);
        }
    }

    render() {
        if (this.state.loading) {
            return (< p > loading</p >)
        }

        return (<div className="container-css" >
            <h3 className="portfolio-summary-resize" style={{ paddingBottom: "10px", marginTop: "-15px" }} >Portfolio Summary</h3>

            <h6 style={{ paddingBottom: "10px" }}>Current Value: ${this.state.PortfolioData.currentTotal} </h6>
            <h6 style={{ paddingBottom: "10px" }}>Purchase Value: ${this.state.PortfolioData.purchaseTotal} </h6>
        </div>);
    }
}