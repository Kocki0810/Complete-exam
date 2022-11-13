
<template>
    <div>
        <BarChart :chartData="this.chartData"/>
    </div>
</template>

<script>

import { toRaw } from 'vue';
import BarChart from './BarChart.vue'


export default {
    created() {
        this.CreateRapport();
    },
    props: {
        ordre: Array,
        Ekspedienter: Array,
    },
    data() {
    return {
        chartData: {
            labels: [],
            datasets: [ 
            { 
                label: 'Salg',
                backgroundColor: '#f87979',
                data: [] 
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
                label: 'Salg i kroner per ekspedient',
                backgroundColor: '#f87979',
                data: [] 
            }]
        }
        var FinalData = []
        var Data = [];
        var names = [];
        var labels = [];

        this.Ekspedienter.data.forEach((element) => {
            names[element.id] = element.navn;
        })
        
        this.ordre.data.forEach((element) => {
            var name = names[element.ekspedient_id];

            if(Data[name] == null)
            {
                labels.push(name);
                Data[name] = element.pris;
            }
            else
            {
                Data[name] += element.pris ;
            }
        })

        labels.forEach(element => {

        chartData.labels.push(element);
        FinalData.push(Data[element])
        })
        chartData.datasets[0].data = FinalData;
        this.chartData = chartData;
    },
  }
}
</script>