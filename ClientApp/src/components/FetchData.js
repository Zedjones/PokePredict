import React, { Component } from 'react';

function findCorrectName(move) {
  let correctName = "defaultName";
  move.names.forEach(name => {
    if (name.language.name === "en") {
      correctName = name.name;
    }
  })
  return correctName;
}

export class FetchData extends Component {
  static displayName = FetchData.name;

  constructor(props) {
    super(props);
    this.state = { forecasts: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderForecastsTable(moves) {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Power</th>
            <th>Accuracy</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {moves.map(move =>
            <tr key={move.name}>
              <td>{findCorrectName(move)}</td>
              <td>{move.power}</td>
              <td>{move.accuracy}</td>
              <td>{move.type.name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('pokemon?pokemon=pikachu');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
  }
}
