<?php

namespace Database\Factories;

use Illuminate\Database\Eloquent\Factories\Factory;
use App\Models\firma;
use App\Models\ekspedient;


/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\ordre>
 */
class OrdreFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition()
    {
        $firma_id = $this->faker->randomElement(firma::pluck('firma_id'));
        $ekspedient_id = $this->faker->randomElement(ekspedient::pluck('ekspedient_id'));

        return [
            'firma_id' => $firma_id,
            'ekspedient_id' => $ekspedient_id,
            'pris' => $this->faker->numberBetween('10', '1000'),
            'date' => $this->faker->dateTimeThisDecade(),
            
        ];
    }
}
