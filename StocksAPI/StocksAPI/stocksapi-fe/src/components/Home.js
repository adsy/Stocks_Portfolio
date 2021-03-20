import React, { Component } from 'react';
import Container from 'react-bootstrap/Container';
import StocksContainer from './StocksContainer';
import DetailsContainer from './DetailsContainer';

class Home extends Component {
    render() {
        return (
            <div className="App">
                <header className="App-header">
                    <h1 class="page-title">WHATCHU WORTH?</h1>
                </header>
                <div style={{ display: 'flex', justifyContent: 'space-around', flexWrap: 'wrap' }}>
                    <div className="col-sm-7" >
                        <StocksContainer />
                    </div>
                    <div style={{ border: "5px solid black", marginTop: "20PX", backgroundColor: "#1C2541", borderRadius: "2px 16px", width: "90%" }} className="col-sm-3">
                        <DetailsContainer />
                    </div>
                </div>
            </div>
        );
    }
}

export default Home;