<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use App\Models\firma;


class FirmaSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        Firma::factory()
        ->count(5)
        ->hasProduktgrupper(5)
        ->hasProdukter(10)
        ->hasEkspedienter(5)
        ->hasBrugere(2)
        ->create();
        
        
    }
}
