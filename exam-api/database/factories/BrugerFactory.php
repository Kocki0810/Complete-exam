<?php

namespace Database\Factories;

use App\Models\firma;
use App\Models\ekspedient;
use Illuminate\Database\Eloquent\Factories\Factory;
use Illuminate\Support\Facades\DB;

/**
 * @extends \Illuminate\Database\Eloquent\Factories\Factory<\App\Models\bruger>
 */
class BrugerFactory extends Factory
{
    /**
     * Define the model's default state.
     *
     * @return array<string, mixed>
     */
    public function definition()
    {
        $ekspedientID = $this->faker->randomElement(ekspedient::pluck('id'));

        return [
            // 'firmaID' => $this->faker->randomElement(firma::pluck('firmaID')),
            'ekspedient_id' => $ekspedientID,
            'navn' => DB::table('ekspedienter')->where('id', $ekspedientID)->value('navn'),
            'username' => $this->faker->userName,
            'password' => $this->faker->password,
            'last_login' => $this->faker->dateTimeThisDecade(),
            'last_IP' => $this->faker->ipv4,
            'adgangsgruppe' => $this->faker->numberBetween(1, 5),
            'email' => $this->faker->email,
            'telefon' => $this->faker->phoneNumber,
        ];
    }
}
