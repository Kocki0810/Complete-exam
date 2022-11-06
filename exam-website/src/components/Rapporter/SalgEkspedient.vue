
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
        console.log("created");
    },
    props: {
        ordre: Array,
        Ekspedienter: Array,
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
                label: 'Salg i kroner per ekspedient',
                backgroundColor: '#f87979',
                data: [] 
            }]
        }
        var FinalData = []
        var Data = [];
        var names = [];
        var labels = [];
        console.log(toRaw(this.Ekspedienter));
        // this.ordre.forEach(ordre => {
        //     Data[ordre.ekspedient_id]
        // })
        this.Ekspedienter.data.forEach((element) => {
            names[element.id] = element.navn;
        })
        console.log(names);
        
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
        console.log(Data);
        chartData.datasets[0].data = FinalData;
        this.chartData = chartData;
        // this.chartData.datasets.data = FinalData;
    },
  }
}
</script>