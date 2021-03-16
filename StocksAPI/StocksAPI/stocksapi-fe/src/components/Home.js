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
                <div style={{display:'flex',justifyContent:'space-around'}}>
                    <div className="col-lg-6" >
                        <StocksContainer />
                    </div>
                    <div style={{ border:"5px solid black", marginTop:"20PX"}} className="col-lg-4">
                        <DetailsContainer />
                    </div>
                </div>
            </div>
        );
    }
}

export default Home;