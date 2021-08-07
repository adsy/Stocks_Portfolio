import { Button } from "@material-ui/core";
import axios from "axios";
import React, { Component } from "react";
import { Constants } from "../../constants/Constants";

class LoginForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      email: "",
      password: "",
      errorMessage: null,
      loading: false,
    };

    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleEmailChange = this.handleEmailChange.bind(this);
    this.handlePasswordChange = this.handlePasswordChange.bind(this);
  }

  async Login() {
    fetch(`${Constants.login}`, {
      method: "POST",
      credentials: "include",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: `${this.state.email}`,
        password: `${this.state.password}`,
      }),
    })
      .then((response) => response.json())
      .then((response) => {
        localStorage.removeItem("token");

        const token = response.token;

        localStorage.setItem("token", token);
      })
      .then(() => {
        this.props.history.push("/dashboard");
      })
      .catch((error) => {
        console.log(error);
        this.setState({
          errorMessage: "INCORRECT EMAIL OR PASSWORD",
          loading: false,
        });
      });
  }

  handleEmailChange(event) {
    this.setState({ email: event.target.value });
  }

  handlePasswordChange(event) {
    this.setState({ password: event.target.value });
  }

  async handleSubmit(event) {
    this.setState({ errorMessage: null });
    event.preventDefault();

    this.setState({ loading: true });

    await this.Login();
  }

  render() {
    return (
      <form
        onSubmit={this.handleSubmit}
        className="centered-box col-lg-12"
        style={{
          borderRadius: "0px 50px",
          backgroundColor: "white",
          width: "400px",
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          justifyContent: "center",
          border: "5px solid black",
        }}
      >
        <h3
          style={{
            marginTop: "20px",
            marginBottom: "20px",
            fontSize: "48px",
          }}
        >
          LOGIN
        </h3>

        <label>
          <h6>Email</h6>
        </label>
        <input
          type="email"
          name="email"
          value={this.state.email}
          onChange={this.handleEmailChange}
          style={{ width: "300px" }}
        />
        <label style={{ marginTop: "10px" }}>
          <h6>Password</h6>
        </label>
        <input
          type="password"
          name="password"
          value={this.state.password}
          onChange={this.handlePasswordChange}
          style={{ width: "300px" }}
        />
        <br />
        {this.state.errorMessage != null ? (
          <h6
            style={{ color: "red", textAlign: "center", marginBottom: "30px" }}
          >
            {this.state.errorMessage}
          </h6>
        ) : (
          ""
        )}
        {this.state.loading == false ? (
          <Button
            type="submit"
            variant="contained"
            value="Submit"
            color="secondary"
            style={{ width: "10vw", marginBottom: "10px" }}
          >
            Login
          </Button>
        ) : (
          <div className="div">
            <h5 className="login-loading-center row">🚀</h5>
            <br />
          </div>
        )}
      </form>
    );
  }
}

export default LoginForm;
