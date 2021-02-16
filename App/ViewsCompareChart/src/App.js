import React from 'react';
import { connect } from "react-redux";
import { setData } from './redux';  
import { Chart } from "react-google-charts";
import moment from 'moment';

const options = {
  title: "Cumulative job views vs. prediction",
  titleTextStyle: { 
                    color: '#3a6f8c',
                    fontSize: 25,
                    bold: true 
                  },
  width: '100%',
  height: '700',
  pointSize: 12,  
  series: {
    //job views
    0 : {
      targetAxisIndex: 0, 
      color: '#9bb75a',
    }, 
    //job predicted views
    1 : {
      targetAxisIndex: 0, 
      color: '#88c2c5', 
      lineDashStyle: [1, 3]
  }, 
  // jobs
  2 : {
      targetAxisIndex: 1, 
      color: '#dddddd', 
      type: 'bars'
    } 
  },
  hAxis: {
    slantedText: true, 
    slantedTextAngle: 45,
    type: 'date',
    viewWindow: {
      min: new Date('4 May, 2018'),
      max: new Date('3 Jun, 2018')
    },
    gridlines: {
        color: 'transparent'
    },
    
  },
  vAxes: {
    0 : {
            title: 'Job Views',
            position: 'bottom',           
            maxValue: 1500,
            minValue: 0,
            ticks: [0, 500, 1000, 1500],
            logScale: false,
            viewWindowMode: 'explicit',
        },
    1 : {
            title: 'Jobs',           
            maxValue: 100,
            minValue: 0,
            ticks: [0, 50, 100],
            viewWindowMode: 'explicit',
            gridlines: {
              color: 'transparent'
            },
            logScale: false
    },
    2 : { 
            title: 'none',
            maxValue: 100,
            minValue: 0,
            ticks: [0, 50, 100],
            logScale: false,
            viewWindowMode: 'explicit',
            gridlines: {
              color: 'transparent'
            },
        }
  },
  timeline: {
          groupByRowLabel: true
  },
  legend: { 
    position: 'bottom', 
  },
  focusTarget: 'category',
  tooltip: {
     isHtml: true,
     format: 'M/d/yy' 
  }
};

class App extends React.Component {

    createDataArray(dataArray, data) {
      var types = this.getTypeArray();
    
      dataArray.push(types);
      
      data.items.forEach(element => {
        var row = this.mapData(element);
        dataArray.push(row);
      });
    }   

  getTypeArray() {
    return [{ type: 'string', id: 'Date' },
    { type: 'number', id: 'Jobs views', label: 'Cumulative job views' },
    { type: 'number', id: 'Predicted job views', label: 'Cumulative predicted job views' },
    { type: 'number', id: 'Active jobs', label: 'Active jobs' }
    ];
  }

  mapData(element) {
    var row = [];

    var date = moment(element['date'], 'YYYY-MM-DDThh:mm:ss').format('MMM D');
    row.push(date);

    var numberOfActiveJobs = element['numberOfActiveJobs'];
    row.push(numberOfActiveJobs === 0 ? null : numberOfActiveJobs);

    var numberOfJobViews = element['numberOfJobViews'];
    row.push(numberOfJobViews === 0 ? null : numberOfJobViews);

    var numberOfPredictedJobViews = element['numberOfPredictedJobViews'];
    row.push(numberOfPredictedJobViews === 0 ? null : numberOfPredictedJobViews);
    return row;
  }

    async componentDidMount() {
      const url = 'https://localhost:44362/Job';
      const response = await fetch(url);
      const data = await response.json();
      var dataArray = [ ]; 
      this.createDataArray(dataArray, data);
      this.props.setData(dataArray);
    }

  render() {
    return (
      <div className={'chart-container'}>
        <Chart
          chartType="LineChart"
          loader={<div>Loading Chart</div>}
          data={this.props.data}
          options={options}
          width={'100%'}
          height='200px'
          legendToggle
        />
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    data: state.data
  };
}

const mapDispatchToProps = {
  setData
}

const AppContainer = connect(
  mapStateToProps,
  mapDispatchToProps
)(App);

export default AppContainer;
