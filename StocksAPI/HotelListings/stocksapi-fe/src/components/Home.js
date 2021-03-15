import React, { Component } from 'react';
import Container from 'react-bootstrap/Container';
import AppContainer from './Container';

class Home extends Component {
    render() {
        return (
            <div className="App">
                <header className="App-header">
                    <h1 class="page-title">WHATCHU WORTH?</h1>
                </header>
                <AppContainer />
            </div>
        );
    }
}

export default Home;