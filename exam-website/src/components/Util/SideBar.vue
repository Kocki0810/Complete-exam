<template>
    <div>
        <div class="">
            <p @click="this.$emit('Update-Page', 'StartPage')">Start</p>
            <p @click="this.$emit('Update-Page', 'ProduktgruppePage')">Produktgrupper</p>
            <p @click="this.$emit('Update-Page', 'EkspedientPage')">Ekspedienter</p>
        </div>
    </div>
</template>