
<template>
    <div>
        <BarChart :chartData="this.chartData"/>
    </div>
</template>

<script>

import BarChart from './BarChart.vue'


export default {
    created() {
        this.CreateRapport();
    },
    props: {
        ordre: Array,
        linjer: Array,
    },
    data() {
    return {
        chartData: {
            labels: [ 'January', 'February', 'March' ],
            datasets: [ 
            { 
                label: 'Salg',
                backgroundColor: '#f87979',
                data: [40, 20, 12] 
            }]
        }
    }
  },
  components: {
    BarChart
  },
  methods: {
      CreateRapport()
      {
        var chartData = {
            labels: [],
            datasets: [ 
            { 
                label: 'Salg i kroner per produkt',
                backgroundColor: '#f87979',
                data: [] 
            }]
        }
        var FinalLabels = []
        var FinalData = []
        var Data = []
        var labels = [];
        this.linjer.forEach(linje => {
            if(Data[linje.bontekst] == null)
            {
                labels.push(linje.bontekst);
                Data[linje.bontekst] = linje.pris;
            }
            else
            {
                Data[linje.bontekst] += linje.pris ;
            }
        })
   
        labels.forEach(element => {

            chartData.labels.push(element);
            FinalData.push(Data[element])
        })
        chartData.datasets[0].data = FinalData;
        this.chartData = chartData;
        // this.chartData.datasets.data = FinalData;
    },
  }
}
</script>